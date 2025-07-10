#!/usr/bin/env python
# -*- coding: utf-8 -*-

import sys, os, argparse, json, traceback
import cv2, numpy as np, albumentations as A
import tensorflow as tf
from tensorflow.keras import layers, Model, optimizers
from tensorflow.keras.applications import EfficientNetB0
from tensorflow.keras.preprocessing.image import ImageDataGenerator
import tf2onnx
import subprocess

# Character encoding fix
if sys.stdout.encoding != 'utf-8':
    sys.stdout.reconfigure(encoding='utf-8')
if sys.stderr.encoding != 'utf-8':
    sys.stderr.reconfigure(encoding='utf-8')

# Custom Callback for live logging
class TrainingLogCallback(tf.keras.callbacks.Callback):
    def on_epoch_end(self, epoch, logs=None):
        logs = logs or {}
        msg = f"[PROGRESS] Epoch {epoch+1}"
        for k, v in logs.items():
            msg += f" - {k}: {v:.4f}"
        log(msg)

def log(msg):
    print(msg, flush=True)
    # Log dosyasını her zaman veri klasörüne yaz
    try:
        if 'args' in globals() and hasattr(args, 'classes'):
            log_path = os.path.join(os.path.abspath(args.classes), f'{getattr(args, "model_prefix", "model")}_train_log.txt')
            with open(log_path, 'a', encoding='utf-8') as f:
                f.write(msg + '\n')
    except Exception as e:
        print(f'[LOGGING ERROR] {e}', flush=True)

def print_header(title):
    print("\n" + "="*20)
    print(title)
    print("="*20)

def check_requirements():
    print_header("GEREKSİNİMLER KONTROL EDİLİYOR")
    try:
        print(f"Python: {sys.version}")
        print(f"TensorFlow: {tf.__version__}")
        print(f"OpenCV: {cv2.__version__}")
        print(f"NumPy: {np.__version__}")
        
        # Print all installed packages
        print("\n--- Yüklü Paketler ---")
        reqs = subprocess.check_output([sys.executable, '-m', 'pip', 'freeze'])
        installed_packages = [r.decode() for r in reqs.split()]
        for pkg in installed_packages:
            print(pkg)
        print("------------------------\n")

    except Exception as e:
        log(f"[ERR] Gereksinim kontrolü hatası: {e}")
        raise
    log("=== GEREKSİNİMLER TAMAM ===")

def check_directories(classes_dir, used_classes=None):
    try:
        log("=== KLASÖRLER KONTROL EDİLİYOR ===")
        if not os.path.exists(classes_dir):
            raise Exception(f"Klasör bulunamadı: {classes_dir}")
        
        class_dirs = [d for d in os.listdir(classes_dir) if os.path.isdir(os.path.join(classes_dir, d))]
        if used_classes is not None:
            class_dirs = [d for d in class_dirs if d in used_classes]
        log(f"Kullanılan sınıf klasörleri: {class_dirs}")
        if not class_dirs:
            raise Exception("Hiç sınıf klasörü bulunamadı!")
        
        total_files = 0
        for class_dir in class_dirs:
            files = [f for f in os.listdir(os.path.join(classes_dir, class_dir)) if f.endswith('.png')]
            log(f"{class_dir}: {len(files)} dosya")
            total_files += len(files)
        
        if total_files == 0:
            raise Exception("Hiç resim dosyası bulunamadı!")
        
        log(f"Toplam {len(class_dirs)} sınıf, {total_files} dosya bulundu")
        log("=== KLASÖR KONTROLÜ TAMAM ===")
    except Exception as e:
        log(f"[ERR] Klasör kontrolü hatası: {e}")
        raise

def preprocess_image(img):
    try:
        # img burada zaten numpy array!
        img = cv2.resize(img, (args.img_size, args.img_size))
        return img.astype('float32')/255.0
    except Exception as e:
        log(f"[ERR] Ön işlem hatası: {e}")
        raise

def create_model(num_classes):
    try:
        base = EfficientNetB0(
            include_top=False,
            input_shape=(args.img_size, args.img_size, 3),
            weights='imagenet'
        )
        base.trainable = False
        
        x = layers.GlobalAveragePooling2D()(base.output)
        x = layers.Dropout(0.3)(x)
        x = layers.Dense(128, activation='relu')(x)
        outputs = layers.Dense(num_classes, activation='softmax')(x)
        
        model = Model(base.input, outputs)
        model.compile(
            optimizer=optimizers.Adam(args.learning_rate),
            loss='categorical_crossentropy',
            metrics=['accuracy']
        )
        return model
    except Exception as e:
        log(f"[ERR] Model oluşturma hatası: {e}")
        raise

def save_model(model, classes_dir, prefix):
    try:
        out_onnx = os.path.join(classes_dir, f'{prefix}.onnx')
        
        # Sadece ONNX formatında kaydet
        spec = (tf.TensorSpec((None, args.img_size, args.img_size, 3), tf.float32, name='input'),)
        tf2onnx.convert.from_keras(model, input_signature=spec, output_path=out_onnx)
        log(f"ONNX saved: {out_onnx}")
        
        return True
    except Exception as e:
        log(f"[ERR] Model kaydetme hatası: {e}")
        return False

def main():
    global args
    try:
        log('=== PYTHON TRAINING SCRIPT BAŞLADI ===')
        
        # 1) Argümanları al
        parser = argparse.ArgumentParser(description="Advanced quality control training")
        parser.add_argument('--classes', required=True, help='path to live_dataset folder')
        parser.add_argument('--epochs', type=int, default=20)
        parser.add_argument('--batch_size', type=int, default=16)
        parser.add_argument('--img_size', type=int, default=224)
        parser.add_argument('--model_prefix', type=str, default='model')
        parser.add_argument('--used_classes', type=str, default=None)
        parser.add_argument('--learning_rate', type=float, default=0.0001)
        parser.add_argument('--scale_min', type=float, default=0.8)
        parser.add_argument('--scale_max', type=float, default=1.2)
        args = parser.parse_args()
        prefix = args.model_prefix
        used_classes = None
        if args.used_classes:
            used_classes = [x.strip() for x in args.used_classes.split(',') if x.strip()]
        log(f"used_classes: {used_classes}")
        log(f"learning_rate: {args.learning_rate}")
        log(f"scale_min: {args.scale_min}, scale_max: {args.scale_max}")

        # 2) Gereksinimleri kontrol et
        check_requirements()

        # 3) Klasörleri kontrol et
        classes_dir = os.path.abspath(args.classes)
        check_directories(classes_dir, used_classes)

        # 4) Gelen scale parametrelerini kontrol et, geçersizse varsayılan ata
        zoom_min = args.scale_min
        zoom_max = args.scale_max
        if zoom_min <= 0 or zoom_max <= 0 or zoom_min >= zoom_max:
            log(f"[WARNING] Invalid scale values received (min: {zoom_min}, max: {zoom_max}). Using defaults [0.9, 1.1].")
            zoom_min = 0.9
            zoom_max = 1.1

        # 5) Data Generator'ları hazırla
        log("=== GENERATOR'LAR HAZIRLANIYOR ===")
        datagen = ImageDataGenerator(
            preprocessing_function=preprocess_image,
            validation_split=0.2,
            # Augmentation options
            rotation_range=15,
            width_shift_range=0.1,
            height_shift_range=0.1,
            shear_range=0.1,
            zoom_range=[zoom_min, zoom_max],
            horizontal_flip=True,
            fill_mode='nearest'
        )
        
        train_gen = datagen.flow_from_directory(
            classes_dir,
            target_size=(args.img_size, args.img_size),
            batch_size=args.batch_size,
            subset='training',
            class_mode='categorical',
            classes=used_classes if used_classes else None
        )
        
        val_gen = datagen.flow_from_directory(
            classes_dir,
            target_size=(args.img_size, args.img_size),
            batch_size=args.batch_size,
            subset='validation',
            class_mode='categorical',
            classes=used_classes if used_classes else None
        )
        
        log(f"Classes: {train_gen.num_classes}, Train samples: {train_gen.samples}, Val samples: {val_gen.samples}")
        
        if train_gen.samples == 0 or train_gen.num_classes == 0:
            raise Exception("Hiç eğitim verisi bulunamadı!")

        # 6) Model oluştur
        log("=== MODEL OLUŞTURULUYOR ===")
        model = create_model(train_gen.num_classes)
        model.summary(print_fn=log)
        log(f"Start training: epochs={args.epochs}, batch_size={args.batch_size}, lr={args.learning_rate}")

        # 7) Eğitim
        log("=== EĞİTİM BAŞLIYOR ===")
        history = model.fit(
            train_gen,
            validation_data=val_gen,
            epochs=args.epochs,
            callbacks=[
                TrainingLogCallback(),
                tf.keras.callbacks.EarlyStopping(
                    monitor='val_loss',
                    patience=5,
                    restore_best_weights=True
                )
            ],
            verbose=0
        )
        log("=== EĞİTİM TAMAMLANDI ===")

        # 8) History'i kaydet
        log("=== HISTORY KAYDEDİLİYOR ===")
        try:
            hist_data = {
                k: [float(val) for val in v] 
                for k, v in history.history.items()
            }
            with open(os.path.join(classes_dir, f'{prefix}_history.json'), 'w') as f:
                json.dump(hist_data, f)
            log(f"History saved -> {prefix}_history.json")
        except Exception as e:
            log(f"[ERR] history.json kaydedilemedi: {e}")

        # 9) Model'i kaydet (Sadece ONNX)
        log("=== MODEL KAYDEDİLİYOR ===")
        if save_model(model, classes_dir, prefix):
            log("=== İŞLEM TAMAMLANDI ===")
            sys.exit(0)
        else:
            log("[FATAL] Model dosyaları oluşturulamadı!")
            sys.exit(1)
            
    except Exception as e:
        tb = traceback.format_exc()
        log(f"[FATAL] Hata oluştu: {e}\n{tb}")
        sys.exit(1)

if __name__ == "__main__":
    main()
