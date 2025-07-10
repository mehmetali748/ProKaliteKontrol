#!/usr/bin/env python
# -*- coding: utf-8 -*-

import sys, os, argparse, json, traceback, datetime, hashlib
import cv2, numpy as np, albumentations as A
import tensorflow as tf
from tensorflow.keras import layers, Model, optimizers
from tensorflow.keras.applications import ResNet50, ResNet101, ResNet152
from tensorflow.keras.preprocessing.image import ImageDataGenerator
from tensorflow.keras.metrics import Precision, Recall
import tf2onnx
import subprocess
from sklearn.metrics import classification_report, confusion_matrix, precision_recall_fscore_support
from sklearn.utils.class_weight import compute_class_weight
import matplotlib.pyplot as plt
import seaborn as sns
import pandas as pd
from pathlib import Path
import glob

# Character encoding fix
if sys.stdout.encoding != 'utf-8':
    sys.stdout.reconfigure(encoding='utf-8')
if sys.stderr.encoding != 'utf-8':
    sys.stderr.reconfigure(encoding='utf-8')

def global_exception_handler(exctype, value, tb):
    print(''.join(traceback.format_exception(exctype, value, tb)), flush=True)

sys.excepthook = global_exception_handler

print(f"[PYTHON PATH] {sys.executable}", flush=True)
print(f"[PYTHON VERSION] {sys.version}", flush=True)

def to_serializable(val):
    """TensorFlow tensor'larını ve numpy array'lerini JSON serileştirilebilir hale getir"""
    try:
        # TensorFlow tensor kontrolü
        if hasattr(val, 'numpy'):
            numpy_val = val.numpy()
            if hasattr(numpy_val, 'shape') and numpy_val.shape == ():
                return float(numpy_val.item())
            else:
                return numpy_val.tolist()
        
        # Numpy array kontrolü
        if isinstance(val, (np.generic, np.ndarray)):
            if hasattr(val, 'shape') and val.shape == ():
                return float(val.item())
            else:
                return val.tolist()
        
        # TensorFlow EagerTensor kontrolü
        if str(type(val)).find('EagerTensor') != -1:
            try:
                numpy_val = val.numpy()
                if hasattr(numpy_val, 'shape') and numpy_val.shape == ():
                    return float(numpy_val.item())
                else:
                    return numpy_val.tolist()
            except:
                return str(val)
        
        # Diğer türler için
        return val
    except Exception as e:
        # Hata durumunda string'e çevir
        return str(val)

class IndustrialTrainingCallback(tf.keras.callbacks.Callback):
    def __init__(self, log_dir):
        super().__init__()
        self.log_dir = log_dir
        self.epoch_times = []
        self.start_time = None
        
    def on_epoch_begin(self, epoch, logs=None):
        self.start_time = datetime.datetime.now()
        
    def on_epoch_end(self, epoch, logs=None):
        logs = logs or {}
        epoch_time = (datetime.datetime.now() - self.start_time).total_seconds()
        self.epoch_times.append(epoch_time)
        
        msg = f"[PROGRESS] Epoch {epoch+1} - Time: {epoch_time:.2f}s"
        for k, v in logs.items():
            msg += f" - {k}: {v:.4f}"
        log(msg)
        
        # Save epoch metrics
        self.save_epoch_metrics(epoch, logs, epoch_time)
        
    def save_epoch_metrics(self, epoch, logs, epoch_time):
        try:
            # Metrics'i güvenli şekilde hazırla
            safe_logs = {}
            for k, v in logs.items():
                try:
                    safe_logs[k] = to_serializable(v)
                except Exception as e:
                    safe_logs[k] = str(v)  # Hata durumunda string'e çevir
                    log(f"[WARNING] Metric {k} serileştirilemedi: {e}")
            
            metrics = {
                'epoch': epoch + 1,
                'timestamp': datetime.datetime.now().isoformat(),
                'epoch_time': epoch_time,
                **safe_logs
            }
            
            metrics_file = os.path.join(self.log_dir, 'epoch_metrics.json')
            
            # Mevcut metrics'leri yükle
            all_metrics = []
            if os.path.exists(metrics_file):
                try:
                    with open(metrics_file, 'r') as f:
                        all_metrics = json.load(f)
                except Exception as e:
                    log(f"[WARNING] Mevcut metrics dosyası okunamadı: {e}")
                    all_metrics = []
            
            all_metrics.append(metrics)
            
            # Yeni metrics'leri kaydet
            with open(metrics_file, 'w') as f:
                json.dump(all_metrics, f, indent=2, default=to_serializable)
                
        except Exception as e:
            log(f"[WARNING] Epoch metrics kaydedilemedi: {e}")
            # Hata detaylarını logla
            import traceback
            log(f"[DEBUG] Metrics kaydetme hatası detayı: {traceback.format_exc()}")

def log(msg):
    print(msg, flush=True)
    try:
        if 'args' in globals() and hasattr(args, 'classes'):
            log_path = os.path.join(os.path.abspath(args.classes), f'{getattr(args, "model_prefix", "model")}_train_log.txt')
            with open(log_path, 'a', encoding='utf-8') as f:
                f.write(f"{datetime.datetime.now().isoformat()} - {msg}\n")
    except Exception as e:
        print(f'[LOGGING ERROR] {e}', flush=True)

def get_git_info():
    """Git repository bilgilerini al (Git olmadan çalışır)"""
    try:
        return {
            'commit_hash': 'no_git_available',
            'branch': 'no_git_available',
            'is_dirty': False
        }
    except:
        return {'commit_hash': 'unknown', 'branch': 'unknown', 'is_dirty': False}

def check_requirements():
    """Gerekli kütüphanelerin kontrolü"""
    log("=== GEREKLİ KÜTÜPHANELER KONTROL EDİLİYOR ===")
    required_packages = [
        'tensorflow', 'opencv-python', 'numpy', 'matplotlib', 
        'seaborn', 'scikit-learn', 'albumentations', 'tf2onnx'
    ]
    
    missing_packages = []
    for package in required_packages:
        try:
            __import__(package.replace('-', '_'))
            log(f"✓ {package}")
        except ImportError:
            missing_packages.append(package)
            log(f"✗ {package} - EKSİK")
    
    if missing_packages:
        log(f"[WARNING] Eksik paketler: {missing_packages}")
        log("[INFO] Eksik paketler yüklenmeye çalışılacak...")
    else:
        log("✓ Tüm gerekli kütüphaneler mevcut")

def check_directories(classes_dir, used_classes=None):
    """Dizin yapısını kontrol et"""
    log("=== DİZİN YAPISI KONTROL EDİLİYOR ===")
    
    if not os.path.exists(classes_dir):
        raise FileNotFoundError(f"Classes dizini bulunamadı: {classes_dir}")
    
    class_dirs = [d for d in os.listdir(classes_dir) if os.path.isdir(os.path.join(classes_dir, d))]
    
    if used_classes is not None:
        missing_classes = [cls for cls in used_classes if cls not in class_dirs]
        if missing_classes:
            raise FileNotFoundError(f"Belirtilen sınıflar bulunamadı: {missing_classes}")
        class_dirs = [d for d in class_dirs if d in used_classes]
    
    if not class_dirs:
        raise ValueError(f"Hiç sınıf dizini bulunamadı: {classes_dir}")
    
    log(f"Bulunan sınıflar: {class_dirs}")
    
    # Her sınıf için dosya kontrolü
    for class_dir in class_dirs:
        class_path = os.path.join(classes_dir, class_dir)
        files = [f for f in os.listdir(class_path) if f.lower().endswith(('.png', '.jpg', '.jpeg'))]
        log(f"  {class_dir}: {len(files)} dosya")
        
        if len(files) == 0:
            log(f"[WARNING] {class_dir} sınıfında hiç resim dosyası yok!")

def check_data_quality(classes_dir, used_classes=None):
    """Veri kalitesi kontrolü"""
    log("=== VERİ KALİTESİ KONTROLÜ ===")
    
    class_dirs = [d for d in os.listdir(classes_dir) if os.path.isdir(os.path.join(classes_dir, d))]
    if used_classes is not None:
        class_dirs = [d for d in class_dirs if d in used_classes]
    
    class_counts = {}
    total_images = 0
    corrupted_files = []
    
    for class_dir in class_dirs:
        class_path = os.path.join(classes_dir, class_dir)
        files = [f for f in os.listdir(class_path) if f.lower().endswith(('.png', '.jpg', '.jpeg'))]
        
        valid_files = 0
        for file in files:
            try:
                img_path = os.path.join(class_path, file)
                img = cv2.imread(img_path)
                if img is not None and img.size > 0:
                    valid_files += 1
                else:
                    corrupted_files.append(img_path)
            except Exception as e:
                corrupted_files.append(img_path)
                log(f"[WARNING] Corrupted file: {img_path} - {e}")
        
        class_counts[class_dir] = valid_files
        total_images += valid_files
    
    # Class imbalance check
    min_samples = min(class_counts.values()) if class_counts else 0
    max_samples = max(class_counts.values()) if class_counts else 0
    imbalance_ratio = max_samples / min_samples if min_samples > 0 else float('inf')
    
    log(f"Class distribution: {class_counts}")
    log(f"Total valid images: {total_images}")
    log(f"Corrupted files: {len(corrupted_files)}")
    log(f"Class imbalance ratio: {imbalance_ratio:.2f}")
    
    if imbalance_ratio > 5:
        log("[WARNING] Significant class imbalance detected!")
    
    if len(corrupted_files) > 0:
        log(f"[WARNING] {len(corrupted_files)} corrupted files found")
    
    return class_counts, corrupted_files, imbalance_ratio

def create_resnet_model(num_classes, img_size, resnet_variant='resnet50', fine_tuning=True):
    """ResNet tabanlı gelişmiş model mimarisi"""
    try:
        log(f"ResNet {resnet_variant.upper()} modeli oluşturuluyor...")
        
        # ResNet variant seçimi
        if resnet_variant.lower() == 'resnet50':
            base = ResNet50(
                include_top=False,
                input_shape=(img_size, img_size, 3),
                weights='imagenet'
            )
        elif resnet_variant.lower() == 'resnet101':
            base = ResNet101(
                include_top=False,
                input_shape=(img_size, img_size, 3),
                weights='imagenet'
            )
        elif resnet_variant.lower() == 'resnet152':
            base = ResNet152(
                include_top=False,
                input_shape=(img_size, img_size, 3),
                weights='imagenet'
            )
        else:
            raise ValueError(f"Desteklenmeyen ResNet variant: {resnet_variant}")
        
        # Enhanced fine-tuning ayarları
        if fine_tuning:
            # Daha fazla katmanı eğitilebilir yap (overfitting'i azaltmak için)
            trainable_layers = 50  # 30'dan 50'ye artırıldı
            for layer in base.layers[-trainable_layers:]:
                layer.trainable = True
            log(f"Enhanced fine-tuning aktif: Son {trainable_layers} katman eğitilebilir")
            
            # İlk katmanları daha düşük learning rate ile eğit
            for layer in base.layers[:-trainable_layers]:
                layer.trainable = False
        else:
            # Tüm base model katmanlarını dondur
            for layer in base.layers:
                layer.trainable = False
            log("Transfer learning: Base model donduruldu")
        
        # Enhanced endüstriyel kalite kontrol için özelleştirilmiş head
        x = layers.GlobalAveragePooling2D()(base.output)
        x = layers.BatchNormalization()(x)
        x = layers.Dropout(0.6)(x)  # 0.5'ten 0.6'ya artırıldı
        
        # Daha derin ve güçlü head (endüstriyel uygulamalar için)
        x = layers.Dense(1024, activation='relu')(x)  # 512'den 1024'e artırıldı
        x = layers.BatchNormalization()(x)
        x = layers.Dropout(0.5)(x)  # 0.4'ten 0.5'e artırıldı
        
        x = layers.Dense(512, activation='relu')(x)
        x = layers.BatchNormalization()(x)
        x = layers.Dropout(0.4)(x)  # 0.3'ten 0.4'e artırıldı
        
        x = layers.Dense(256, activation='relu')(x)
        x = layers.BatchNormalization()(x)
        x = layers.Dropout(0.3)(x)  # 0.2'den 0.3'e artırıldı
        
        x = layers.Dense(128, activation='relu')(x)
        x = layers.BatchNormalization()(x)
        x = layers.Dropout(0.2)(x)
        
        # Çıkış katmanı
        outputs = layers.Dense(num_classes, activation='softmax')(x)
        
        model = Model(base.input, outputs)
        
        # Enhanced optimizer - ResNet için optimize edilmiş
        if fine_tuning:
            # Fine-tuning için daha düşük learning rate
            optimizer = optimizers.Adam(
                learning_rate=args.learning_rate * 0.05,  # 0.1'den 0.05'e azaltıldı
                beta_1=0.9,
                beta_2=0.999,
                epsilon=1e-8,  # 1e-7'den 1e-8'e azaltıldı
                weight_decay=1e-4  # Weight decay eklendi
            )
        else:
            # Transfer learning için normal learning rate
            optimizer = optimizers.Adam(
                learning_rate=args.learning_rate,
                beta_1=0.9,
                beta_2=0.999,
                epsilon=1e-8,  # 1e-7'den 1e-8'e azaltıldı
                weight_decay=1e-4  # Weight decay eklendi
            )
        
        model.compile(
            optimizer=optimizer,
            loss='categorical_crossentropy',
            metrics=['accuracy', Precision(), Recall()]
        )
        
        log(f"ResNet {resnet_variant.upper()} modeli başarıyla oluşturuldu")
        log(f"Toplam parametre sayısı: {model.count_params():,}")
        log(f"Eğitilebilir parametre sayısı: {sum([tf.keras.backend.count_params(w) for w in model.trainable_weights]):,}")
        
        return model
    except Exception as e:
        log(f"[ERR] ResNet model oluşturma hatası: {e}")
        raise

def evaluate_model_performance(model, val_gen, classes_dir, prefix):
    """Model performans değerlendirmesi"""
    log("=== MODEL PERFORMANS DEĞERLENDİRMESİ ===")
    
    try:
        # Predictions
        val_gen.reset()
        y_pred = model.predict(val_gen, verbose=0)
        y_pred_classes = np.argmax(y_pred, axis=1)
        y_true = val_gen.classes
        
        # Metrics with error handling
        try:
            precision, recall, f1, support = precision_recall_fscore_support(
                y_true, y_pred_classes, average='weighted', zero_division=0
            )
            
            # Ensure all values are valid
            precision = float(precision) if precision is not None else 0.0
            recall = float(recall) if recall is not None else 0.0
            f1 = float(f1) if f1 is not None else 0.0
            support = int(support) if support is not None else len(y_true)
            
        except Exception as e:
            log(f"[WARNING] Metrics calculation failed: {e}")
            precision, recall, f1, support = 0.0, 0.0, 0.0, len(y_true)
        
        # Confusion Matrix
        try:
            cm = confusion_matrix(y_true, y_pred_classes)
        except Exception as e:
            log(f"[WARNING] Confusion matrix failed: {e}")
            cm = np.zeros((len(val_gen.class_indices), len(val_gen.class_indices)))
        
        # Save detailed report
        try:
            report = classification_report(
                y_true, y_pred_classes, 
                target_names=val_gen.class_indices.keys(),
                output_dict=True,
                zero_division=0
            )
        except Exception as e:
            log(f"[WARNING] Classification report failed: {e}")
            report = {}
        
        # Save results
        results = {
            'precision': precision,
            'recall': recall,
            'f1_score': f1,
            'support': support,
            'confusion_matrix': cm.tolist(),
            'classification_report': report,
            'class_indices': val_gen.class_indices
        }
    
        results_file = os.path.join(classes_dir, f'{prefix}_performance.json')
        with open(results_file, 'w') as f:
            json.dump(results, f, indent=2, default=to_serializable)
        
        # Plot confusion matrix
        try:
            plt.figure(figsize=(12, 10))
            sns.heatmap(cm, annot=True, fmt='d', cmap='Blues',
                        xticklabels=val_gen.class_indices.keys(),
                        yticklabels=val_gen.class_indices.keys())
            plt.title('ResNet Confusion Matrix')
            plt.ylabel('True Label')
            plt.xlabel('Predicted Label')
            plt.tight_layout()
            plt.savefig(os.path.join(classes_dir, f'{prefix}_confusion_matrix.png'))
            plt.close()
        except Exception as e:
            log(f"[WARNING] Confusion matrix plot failed: {e}")
        
        log(f"Precision: {precision:.4f}")
        log(f"Recall: {recall:.4f}")
        log(f"F1-Score: {f1:.4f}")
        log(f"Performance results saved to {results_file}")
        
        return results
        
    except Exception as e:
        log(f"[ERROR] Model performance evaluation failed: {e}")
        # Return default results
        return {
            'precision': 0.0,
            'recall': 0.0,
            'f1_score': 0.0,
            'support': 0,
            'confusion_matrix': [],
            'classification_report': {},
            'class_indices': val_gen.class_indices if hasattr(val_gen, 'class_indices') else {}
        }

def save_model_with_metadata(model, classes_dir, prefix, performance_metrics):
    """Model ve metadata kaydetme"""
    try:
        # Model metadata
        metadata = {
            'model_name': prefix,
            'model_type': 'ResNet',
            'resnet_variant': args.resnet_variant,
            'created_at': datetime.datetime.now().isoformat(),
            'git_info': get_git_info(),
            'training_params': {
                'epochs': args.epochs,
                'batch_size': args.batch_size,
                'img_size': args.img_size,
                'learning_rate': args.learning_rate,
                'used_classes': args.used_classes,
                'fine_tuning': args.enable_fine_tuning
            },
            'performance_metrics': performance_metrics,
            'model_hash': hashlib.md5(str(model.get_weights()).encode()).hexdigest()
        }
        
        # Save metadata
        metadata_file = os.path.join(classes_dir, f'{prefix}_metadata.json')
        with open(metadata_file, 'w') as f:
            json.dump(metadata, f, indent=2, default=to_serializable)
        
        # Save ONNX model
        out_onnx = os.path.join(classes_dir, f'{prefix}.onnx')
        spec = (tf.TensorSpec((None, args.img_size, args.img_size, 3), tf.float32, name='input'),)
        tf2onnx.convert.from_keras(model, input_signature=spec, output_path=out_onnx)
        
        # Save TensorFlow model for further analysis
        tf_model_path = os.path.join(classes_dir, f'{prefix}_tf')
        model.save(tf_model_path)
        
        log(f"ResNet model saved: {out_onnx}")
        log(f"TensorFlow model saved: {tf_model_path}")
        log(f"Metadata saved: {metadata_file}")
        
        return True
    except Exception as e:
        log(f"[ERR] Model kaydetme hatası: {e}")
        return False

def filter_empty_classes(classes_dir, used_classes=None):
    """Boş sınıfları filtrele ve geçerli sınıfları döndür"""
    try:
        if used_classes:
            available_classes = used_classes
        else:
            available_classes = [d for d in os.listdir(classes_dir) 
                               if os.path.isdir(os.path.join(classes_dir, d))]
        
        valid_classes = []
        for class_name in available_classes:
            class_path = os.path.join(classes_dir, class_name)
            if os.path.exists(class_path):
                # Sınıf klasöründeki geçerli resim dosyalarını say
                valid_images = 0
                for ext in ['.jpg', '.jpeg', '.png', '.bmp', '.tiff']:
                    valid_images += len(glob.glob(os.path.join(class_path, f'*{ext}')))
                    valid_images += len(glob.glob(os.path.join(class_path, f'*{ext.upper()}')))
                
                if valid_images > 0:
                    valid_classes.append(class_name)
                    log(f"[INFO] Sınıf '{class_name}': {valid_images} geçerli resim")
                else:
                    log(f"[WARNING] Sınıf '{class_name}' boş - filtrelendi")
        
        if len(valid_classes) < 2:
            raise ValueError(f"En az 2 sınıf gerekli! Sadece {len(valid_classes)} geçerli sınıf bulundu.")
        
        log(f"[INFO] {len(valid_classes)} geçerli sınıf kullanılacak: {', '.join(valid_classes)}")
        return valid_classes
        
    except Exception as e:
        log(f"[ERR] Sınıf filtreleme hatası: {e}")
        raise

def main():
    global args
    try:
        log('=== RESNET ENDÜSTRİYEL KALİTE KONTROL EĞİTİMİ BAŞLADI ===')
        
        # Parse arguments
        parser = argparse.ArgumentParser(description="ResNet Industrial Quality Control Training")
        parser.add_argument('--classes', required=True, help='path to live_dataset folder')
        parser.add_argument('--epochs', type=int, default=50)  # ResNet için daha fazla epoch
        parser.add_argument('--batch_size', type=int, default=16)
        parser.add_argument('--img_size', type=int, default=224)
        parser.add_argument('--model_prefix', type=str, default='resnet_model')
        parser.add_argument('--used_classes', type=str, default=None)
        parser.add_argument('--learning_rate', type=float, default=0.0001)
        parser.add_argument('--scale_min', type=float, default=0.8)
        parser.add_argument('--scale_max', type=float, default=1.2)
        parser.add_argument('--enable_fine_tuning', action='store_true', help='Enable fine-tuning')
        parser.add_argument('--resnet_variant', type=str, default='resnet50', 
                          choices=['resnet50', 'resnet101', 'resnet152'],
                          help='ResNet variant to use')
        args = parser.parse_args()
        
        # Setup
        classes_dir = os.path.abspath(args.classes)
        used_classes = None
        if args.used_classes:
            used_classes = [x.strip() for x in args.used_classes.split(',') if x.strip()]
        
        # Create log directory
        log_dir = os.path.join(classes_dir, 'logs')
        os.makedirs(log_dir, exist_ok=True)
        
        # Check requirements and data quality
        check_requirements()
        check_directories(classes_dir, used_classes)
        class_counts, corrupted_files, imbalance_ratio = check_data_quality(classes_dir, used_classes)
        
        # Filter out empty classes
        valid_classes = filter_empty_classes(classes_dir, used_classes)
        
        # Enhanced data augmentation with industrial considerations
        datagen = ImageDataGenerator(
            preprocessing_function=lambda x: x.astype('float32')/255.0,
            validation_split=0.2,
            rotation_range=30,  # 20'den 30'a artırıldı
            width_shift_range=0.2,  # 0.15'ten 0.2'ye artırıldı
            height_shift_range=0.2,  # 0.15'ten 0.2'ye artırıldı
            shear_range=0.2,  # 0.15'ten 0.2'ye artırıldı
            zoom_range=[0.7, 1.3],  # 0.8-1.2'den 0.7-1.3'e genişletildi
            horizontal_flip=True,
            vertical_flip=False,  # Industrial images usually shouldn't be flipped vertically
            brightness_range=[0.6, 1.4],  # 0.8-1.2'den 0.6-1.4'e genişletildi
            fill_mode='nearest'
        )
        
        # Data generators
        train_gen = datagen.flow_from_directory(
            classes_dir,
            target_size=(args.img_size, args.img_size),
            batch_size=args.batch_size,
            subset='training',
            class_mode='categorical',
            classes=valid_classes
        )
        
        val_gen = datagen.flow_from_directory(
            classes_dir,
            target_size=(args.img_size, args.img_size),
            batch_size=args.batch_size,
            subset='validation',
            class_mode='categorical',
            classes=valid_classes
        )
        
        # Class weights for imbalanced data
        class_weights = None
        if imbalance_ratio > 3:
            log("[INFO] Applying class weights for imbalanced data")
            class_weights = compute_class_weight(
                'balanced',
                classes=np.unique(train_gen.classes),
                y=train_gen.classes
            )
            class_weights = dict(zip(range(len(class_weights)), class_weights))
        
        # Create and train ResNet model
        model = create_resnet_model(
            train_gen.num_classes, 
            args.img_size, 
            args.resnet_variant,
            args.enable_fine_tuning
        )
        
        # Enhanced callbacks for better training
        callbacks = [
            IndustrialTrainingCallback(log_dir),
            tf.keras.callbacks.EarlyStopping(
                monitor='val_loss',
                patience=15,  # 10'dan 15'e artırıldı
                restore_best_weights=True,
                verbose=1
            ),
            tf.keras.callbacks.ReduceLROnPlateau(
                monitor='val_loss',
                factor=0.3,  # 0.5'ten 0.3'e değiştirildi (daha agresif)
                patience=3,  # 5'ten 3'e azaltıldı (daha hızlı)
                min_lr=1e-8,  # 1e-7'den 1e-8'e azaltıldı
                verbose=1
            ),
            tf.keras.callbacks.ModelCheckpoint(
                filepath=os.path.join(log_dir, 'best_resnet_model.h5'),
                monitor='val_loss',
                save_best_only=True,
                verbose=1
            ),
            # Yeni callback: Learning rate monitoring
            tf.keras.callbacks.LambdaCallback(
                on_epoch_end=lambda epoch, logs: log(f"Epoch {epoch+1} - LR: {tf.keras.backend.get_value(model.optimizer.learning_rate):.2e}")
            )
        ]
        
        # Training
        log("=== RESNET EĞİTİMİ BAŞLIYOR ===")
        history = model.fit(
            train_gen,
            validation_data=val_gen,
            epochs=args.epochs,
            callbacks=callbacks,
            class_weight=class_weights,
            verbose=1
        )
        
        # Evaluate performance
        performance_metrics = evaluate_model_performance(model, val_gen, classes_dir, args.model_prefix)
        
        # Save model with metadata
        if save_model_with_metadata(model, classes_dir, args.model_prefix, performance_metrics):
            log("=== RESNET ENDÜSTRİYEL EĞİTİM TAMAMLANDI ===")
            sys.exit(0)
        else:
            log("[FATAL] Model kaydedilemedi!")
            sys.exit(1)
            
    except Exception as e:
        log(f'[FATAL] {e}')
        traceback.print_exc()
        exit(1)

if __name__ == "__main__":
    main() 