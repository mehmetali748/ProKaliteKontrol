#!/usr/bin/env python
# -*- coding: utf-8 -*-

import tensorflow as tf
from tensorflow.keras import layers, Model, optimizers, regularizers
from tensorflow.keras.applications import ResNet50, EfficientNetB0
from tensorflow.keras.callbacks import EarlyStopping, ReduceLROnPlateau, ModelCheckpoint
import numpy as np
import matplotlib.pyplot as plt
import seaborn as sns

class AntiOverfittingStrategies:
    """Overfitting önleme stratejileri sınıfı"""
    
    def __init__(self, num_classes, img_size=224):
        self.num_classes = num_classes
        self.img_size = img_size
        
    def create_regularized_model(self, base_model_type='resnet50', regularization_strength=0.01):
        """Regularizasyon ile model oluştur"""
        
        # Base model seçimi
        if base_model_type == 'resnet50':
            base = ResNet50(
                include_top=False,
                input_shape=(self.img_size, self.img_size, 3),
                weights='imagenet'
            )
        elif base_model_type == 'efficientnet':
            base = EfficientNetB0(
                include_top=False,
                input_shape=(self.img_size, self.img_size, 3),
                weights='imagenet'
            )
        
        # Base model katmanlarını dondur (transfer learning)
        for layer in base.layers:
            layer.trainable = False
        
        # Regularizasyon ile head katmanları
        x = layers.GlobalAveragePooling2D()(base.output)
        
        # L2 Regularizasyon ile Dense katmanları
        x = layers.Dense(
            512, 
            activation='relu',
            kernel_regularizer=regularizers.l2(regularization_strength),
            activity_regularizer=regularizers.l1(regularization_strength * 0.1)
        )(x)
        x = layers.BatchNormalization()(x)
        x = layers.Dropout(0.6)(x)  # Daha yüksek dropout
        
        x = layers.Dense(
            256, 
            activation='relu',
            kernel_regularizer=regularizers.l2(regularization_strength),
            activity_regularizer=regularizers.l1(regularization_strength * 0.1)
        )(x)
        x = layers.BatchNormalization()(x)
        x = layers.Dropout(0.5)(x)
        
        x = layers.Dense(
            128, 
            activation='relu',
            kernel_regularizer=regularizers.l2(regularization_strength),
            activity_regularizer=regularizers.l1(regularization_strength * 0.1)
        )(x)
        x = layers.BatchNormalization()(x)
        x = layers.Dropout(0.4)(x)
        
        # Çıkış katmanı
        outputs = layers.Dense(self.num_classes, activation='softmax')(x)
        
        model = Model(base.input, outputs)
        
        return model
    
    def create_lightweight_model(self, base_model_type='efficientnet'):
        """Hafif model oluştur (daha az parametre)"""
        
        if base_model_type == 'efficientnet':
            base = EfficientNetB0(
                include_top=False,
                input_shape=(self.img_size, self.img_size, 3),
                weights='imagenet'
            )
        else:
            # MobilNet gibi daha hafif model
            base = tf.keras.applications.MobileNetV2(
                include_top=False,
                input_shape=(self.img_size, self.img_size, 3),
                weights='imagenet'
            )
        
        # Minimal head
        x = layers.GlobalAveragePooling2D()(base.output)
        x = layers.Dropout(0.5)(x)
        x = layers.Dense(128, activation='relu')(x)
        x = layers.Dropout(0.3)(x)
        outputs = layers.Dense(self.num_classes, activation='softmax')(x)
        
        model = Model(base.input, outputs)
        return model
    
    def create_ensemble_model(self, num_models=3):
        """Ensemble model oluştur"""
        models = []
        
        for i in range(num_models):
            # Farklı base modeller
            base_models = ['efficientnet', 'resnet50', 'mobilenet']
            base_type = base_models[i % len(base_models)]
            
            model = self.create_regularized_model(base_type)
            models.append(model)
        
        return models
    
    def get_advanced_callbacks(self, patience_epochs=15, min_lr=1e-7):
        """Gelişmiş callback'ler"""
        
        callbacks = [
            # Early stopping - daha uzun patience
            EarlyStopping(
                monitor='val_loss',
                patience=patience_epochs,
                restore_best_weights=True,
                verbose=1
            ),
            
            # Learning rate reduction
            ReduceLROnPlateau(
                monitor='val_loss',
                factor=0.3,  # Daha agresif azaltma
                patience=patience_epochs // 2,
                min_lr=min_lr,
                verbose=1
            ),
            
            # Model checkpoint
            ModelCheckpoint(
                filepath='best_model_anti_overfit.h5',
                monitor='val_loss',
                save_best_only=True,
                verbose=1
            ),
            
            # Custom callback - overfitting tespiti
            OverfittingDetectionCallback()
        ]
        
        return callbacks
    
    def get_data_augmentation_strategy(self, strong_augmentation=True):
        """Güçlü data augmentation stratejisi"""
        
        if strong_augmentation:
            # Güçlü augmentation - daha fazla çeşitlilik
            augmentation = tf.keras.Sequential([
                layers.RandomRotation(0.3),  # 30 derece rotasyon
                layers.RandomZoom(0.2),      # %20 zoom
                layers.RandomTranslation(0.1, 0.1),  # %10 translation
                layers.RandomFlip("horizontal"),
                layers.RandomContrast(0.2),  # Kontrast değişimi
                layers.RandomBrightness(0.2),  # Parlaklık değişimi
                layers.GaussianNoise(0.01),  # Gürültü ekleme
            ])
        else:
            # Standart augmentation
            augmentation = tf.keras.Sequential([
                layers.RandomRotation(0.1),
                layers.RandomZoom(0.1),
                layers.RandomFlip("horizontal"),
            ])
        
        return augmentation

class OverfittingDetectionCallback(tf.keras.callbacks.Callback):
    """Overfitting tespiti için custom callback"""
    
    def __init__(self, threshold=0.1):
        super().__init__()
        self.threshold = threshold
        self.train_losses = []
        self.val_losses = []
        
    def on_epoch_end(self, epoch, logs=None):
        logs = logs or {}
        
        self.train_losses.append(logs.get('loss', 0))
        self.val_losses.append(logs.get('val_loss', 0))
        
        if len(self.train_losses) > 5:
            # Son 5 epoch'ta overfitting kontrolü
            train_trend = np.mean(self.train_losses[-5:])
            val_trend = np.mean(self.val_losses[-5:])
            
            # Overfitting tespiti
            if train_trend < val_trend and (val_trend - train_trend) > self.threshold:
                print(f"\n[WARNING] Overfitting tespit edildi! Epoch {epoch+1}")
                print(f"Train loss: {train_trend:.4f}, Val loss: {val_trend:.4f}")
                print(f"Fark: {val_trend - train_trend:.4f}")
                
                # Learning rate'i daha da azalt
                current_lr = self.model.optimizer.learning_rate
                new_lr = current_lr * 0.5
                self.model.optimizer.learning_rate = new_lr
                print(f"Learning rate azaltıldı: {current_lr:.6f} -> {new_lr:.6f}")

def calculate_model_complexity(model):
    """Model karmaşıklığını hesapla"""
    
    total_params = model.count_params()
    trainable_params = sum([tf.keras.backend.count_params(w) for w in model.trainable_weights])
    non_trainable_params = total_params - trainable_params
    
    # Model boyutu (MB)
    model_size_mb = total_params * 4 / (1024 * 1024)  # float32 = 4 bytes
    
    complexity_metrics = {
        'total_parameters': total_params,
        'trainable_parameters': trainable_params,
        'non_trainable_parameters': non_trainable_params,
        'model_size_mb': model_size_mb,
        'complexity_ratio': trainable_params / total_params if total_params > 0 else 0
    }
    
    return complexity_metrics

def plot_training_history(history, save_path='training_history.png'):
    """Eğitim geçmişini görselleştir"""
    
    fig, axes = plt.subplots(2, 2, figsize=(15, 10))
    
    # Loss
    axes[0, 0].plot(history.history['loss'], label='Train Loss')
    axes[0, 0].plot(history.history['val_loss'], label='Validation Loss')
    axes[0, 0].set_title('Model Loss')
    axes[0, 0].set_xlabel('Epoch')
    axes[0, 0].set_ylabel('Loss')
    axes[0, 0].legend()
    axes[0, 0].grid(True)
    
    # Accuracy
    axes[0, 1].plot(history.history['accuracy'], label='Train Accuracy')
    axes[0, 1].plot(history.history['val_accuracy'], label='Validation Accuracy')
    axes[0, 1].set_title('Model Accuracy')
    axes[0, 1].set_xlabel('Epoch')
    axes[0, 1].set_ylabel('Accuracy')
    axes[0, 1].legend()
    axes[0, 1].grid(True)
    
    # Learning Rate
    if 'lr' in history.history:
        axes[1, 0].plot(history.history['lr'])
        axes[1, 0].set_title('Learning Rate')
        axes[1, 0].set_xlabel('Epoch')
        axes[1, 0].set_ylabel('Learning Rate')
        axes[1, 0].set_yscale('log')
        axes[1, 0].grid(True)
    
    # Overfitting Detection
    train_loss = history.history['loss']
    val_loss = history.history['val_loss']
    epochs = range(1, len(train_loss) + 1)
    
    axes[1, 1].plot(epochs, train_loss, 'b-', label='Train Loss')
    axes[1, 1].plot(epochs, val_loss, 'r-', label='Validation Loss')
    axes[1, 1].fill_between(epochs, train_loss, val_loss, 
                           where=(np.array(val_loss) > np.array(train_loss)),
                           alpha=0.3, color='red', label='Overfitting Region')
    axes[1, 1].set_title('Overfitting Detection')
    axes[1, 1].set_xlabel('Epoch')
    axes[1, 1].set_ylabel('Loss')
    axes[1, 1].legend()
    axes[1, 1].grid(True)
    
    plt.tight_layout()
    plt.savefig(save_path, dpi=300, bbox_inches='tight')
    plt.close()

def get_optimal_hyperparameters(num_classes, dataset_size):
    """Veri seti boyutuna göre optimal hiperparametreler"""
    
    if dataset_size < 1000:
        # Küçük veri seti
        return {
            'model_type': 'efficientnet',
            'regularization_strength': 0.02,
            'dropout_rate': 0.7,
            'batch_size': 8,
            'learning_rate': 0.0001,
            'epochs': 30,
            'patience': 10
        }
    elif dataset_size < 5000:
        # Orta veri seti
        return {
            'model_type': 'resnet50',
            'regularization_strength': 0.01,
            'dropout_rate': 0.5,
            'batch_size': 16,
            'learning_rate': 0.0001,
            'epochs': 50,
            'patience': 15
        }
    else:
        # Büyük veri seti
        return {
            'model_type': 'resnet101',
            'regularization_strength': 0.005,
            'dropout_rate': 0.3,
            'batch_size': 32,
            'learning_rate': 0.0001,
            'epochs': 80,
            'patience': 20
        }

# Kullanım örneği
if __name__ == "__main__":
    # Anti-overfitting stratejileri
    strategies = AntiOverfittingStrategies(num_classes=10, img_size=224)
    
    # Optimal hiperparametreler
    dataset_size = 2000  # Örnek veri seti boyutu
    optimal_params = get_optimal_hyperparameters(10, dataset_size)
    
    print("=== OPTİMAL HİPERPARAMETRELER ===")
    for key, value in optimal_params.items():
        print(f"{key}: {value}")
    
    # Regularizasyon ile model oluştur
    model = strategies.create_regularized_model(
        base_model_type=optimal_params['model_type'],
        regularization_strength=optimal_params['regularization_strength']
    )
    
    # Model karmaşıklığını hesapla
    complexity = calculate_model_complexity(model)
    print(f"\n=== MODEL KARMAŞIKLIĞI ===")
    print(f"Toplam parametre: {complexity['total_parameters']:,}")
    print(f"Eğitilebilir parametre: {complexity['trainable_parameters']:,}")
    print(f"Model boyutu: {complexity['model_size_mb']:.2f} MB")
    
    # Callback'ler
    callbacks = strategies.get_advanced_callbacks(
        patience_epochs=optimal_params['patience']
    )
    
    print(f"\n=== OVERFITTING ÖNLEME STRATEJİLERİ ===")
    print("✓ L2/L1 Regularizasyon")
    print("✓ Yüksek Dropout (0.4-0.7)")
    print("✓ Early Stopping")
    print("✓ Learning Rate Reduction")
    print("✓ Güçlü Data Augmentation")
    print("✓ Overfitting Detection Callback") 