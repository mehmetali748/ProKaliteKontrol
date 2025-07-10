# train_model_enhanced.py - Endüstriyel Kalite Kontrol Model Eğitimi Dokümantasyonu

## Genel Bakış

Bu dosya, endüstriyel kalite kontrol sistemleri için gelişmiş bir makine öğrenmesi modeli eğitim scriptidir. EfficientNetB0 tabanlı bir CNN modeli kullanarak görüntü sınıflandırma eğitimi gerçekleştirir.

## Kullanılan Kütüphaneler

### Ana Kütüphaneler
- **TensorFlow/Keras**: Derin öğrenme modeli oluşturma ve eğitimi
- **OpenCV (cv2)**: Görüntü işleme
- **NumPy**: Sayısal işlemler
- **Albumentations**: Gelişmiş veri artırma (data augmentation)
- **Scikit-learn**: Model değerlendirme metrikleri
- **Matplotlib/Seaborn**: Görselleştirme
- **tf2onnx**: Model formatı dönüştürme

### Yardımcı Kütüphaneler
- **argparse**: Komut satırı argümanları
- **json**: Metadata kaydetme
- **datetime**: Zaman damgaları
- **hashlib**: Model hash hesaplama
- **pathlib**: Dosya yolu işlemleri

## Sınıflar

### IndustrialTrainingCallback

**Amaç**: Eğitim sürecini izlemek ve metrikleri kaydetmek

**Özellikler**:
- Epoch başına geçen süreyi ölçer
- Eğitim metriklerini JSON formatında kaydeder
- Gerçek zamanlı ilerleme raporlaması yapar

**Metodlar**:
- `on_epoch_begin()`: Epoch başlangıcında zamanlayıcıyı başlatır
- `on_epoch_end()`: Epoch sonunda metrikleri kaydeder ve raporlar
- `save_epoch_metrics()`: Epoch metriklerini dosyaya kaydeder

## Fonksiyonlar

### Yardımcı Fonksiyonlar

#### `log(msg)`
**Amaç**: Mesajları hem konsola hem de log dosyasına yazdırır
**Parametreler**:
- `msg`: Yazdırılacak mesaj
**Özellikler**:
- UTF-8 encoding desteği
- Otomatik timestamp ekleme
- Hata durumunda graceful handling

#### `get_git_info()`
**Amaç**: Git repository bilgilerini alır
**Dönüş**: Git commit hash, branch ve dirty state bilgileri
**Not**: Git olmadan da çalışır, varsayılan değerler döner

#### `to_serializable(val)`
**Amaç**: TensorFlow/NumPy değerlerini JSON serileştirilebilir hale getirir
**Parametreler**:
- `val`: Dönüştürülecek değer
**Dönüş**: JSON uyumlu değer

### Kontrol Fonksiyonları

#### `check_requirements()`
**Amaç**: Gerekli Python kütüphanelerinin varlığını kontrol eder
**Kontrol edilen kütüphaneler**:
- tensorflow
- opencv-python
- numpy
- matplotlib
- seaborn
- scikit-learn
- albumentations
- tf2onnx

#### `check_directories(classes_dir, used_classes=None)`
**Amaç**: Veri dizin yapısını ve sınıf klasörlerini kontrol eder
**Parametreler**:
- `classes_dir`: Ana veri dizini
- `used_classes`: Kullanılacak sınıflar listesi (opsiyonel)
**Kontroller**:
- Dizin varlığı
- Sınıf klasörlerinin varlığı
- Her sınıftaki dosya sayısı

#### `check_data_quality(classes_dir, used_classes=None)`
**Amaç**: Veri kalitesi ve dağılımını analiz eder
**Parametreler**:
- `classes_dir`: Ana veri dizini
- `used_classes`: Kullanılacak sınıflar listesi (opsiyonel)
**Analizler**:
- Sınıf dağılımı
- Bozuk dosya tespiti
- Class imbalance oranı
- Toplam geçerli görüntü sayısı
**Dönüş**: `(class_counts, corrupted_files, imbalance_ratio)`

### Model Fonksiyonları

#### `create_advanced_model(num_classes, img_size)`
**Amaç**: Gelişmiş CNN modeli oluşturur
**Parametreler**:
- `num_classes`: Sınıf sayısı
- `img_size`: Görüntü boyutu
**Model Mimarisi**:
- **Base Model**: EfficientNetB0 (ImageNet ağırlıkları ile)
- **Fine-tuning**: Son 20 katman eğitilebilir
- **Head Layers**:
  - GlobalAveragePooling2D
  - BatchNormalization
  - Dropout (0.5)
  - Dense (256, ReLU)
  - BatchNormalization
  - Dropout (0.3)
  - Dense (128, ReLU)
  - Dense (num_classes, Softmax)
**Optimizer**: Adam (özelleştirilebilir learning rate)
**Metrikler**: Accuracy, Precision, Recall

#### `evaluate_model_performance(model, val_gen, classes_dir, prefix)`
**Amaç**: Model performansını kapsamlı şekilde değerlendirir
**Parametreler**:
- `model`: Eğitilmiş model
- `val_gen`: Validation data generator
- `classes_dir`: Sonuçların kaydedileceği dizin
- `prefix`: Dosya isimleri için önek
**Değerlendirmeler**:
- Precision, Recall, F1-Score
- Confusion Matrix
- Detaylı sınıf bazlı rapor
- Görsel confusion matrix
**Çıktılar**:
- JSON formatında performans metrikleri
- PNG formatında confusion matrix görseli

#### `save_model_with_metadata(model, classes_dir, prefix, performance_metrics)`
**Amaç**: Model ve metadata'yı kaydeder
**Parametreler**:
- `model`: Kaydedilecek model
- `classes_dir`: Kayıt dizini
- `prefix`: Dosya isimleri için önek
- `performance_metrics`: Performans metrikleri
**Kaydedilen Dosyalar**:
- **ONNX Model**: `{prefix}.onnx`
- **TensorFlow Model**: `{prefix}_tf/`
- **Metadata**: `{prefix}_metadata.json`
**Metadata İçeriği**:
- Model adı ve oluşturma tarihi
- Git bilgileri
- Eğitim parametreleri
- Performans metrikleri
- Model hash değeri

## Ana Fonksiyon: `main()`

### İş Akışı

1. **Argüman Parsing**
   - `--classes`: Veri dizini (zorunlu)
   - `--epochs`: Epoch sayısı (varsayılan: 30)
   - `--batch_size`: Batch boyutu (varsayılan: 16)
   - `--img_size`: Görüntü boyutu (varsayılan: 224)
   - `--model_prefix`: Model dosya öneki (varsayılan: "model")
   - `--used_classes`: Kullanılacak sınıflar (virgülle ayrılmış)
   - `--learning_rate`: Öğrenme oranı (varsayılan: 0.0001)
   - `--scale_min/max`: Zoom aralığı (varsayılan: 0.8-1.2)
   - `--enable_fine_tuning`: Fine-tuning aktifleştirme

2. **Ön Kontroller**
   - Gerekli kütüphanelerin kontrolü
   - Dizin yapısının kontrolü
   - Veri kalitesi analizi

3. **Veri Hazırlama**
   - ImageDataGenerator ile veri artırma
   - Train/validation split (%80/%20)
   - Class weights hesaplama (imbalanced data için)

4. **Model Eğitimi**
   - Model oluşturma
   - Callback'lerin ayarlanması:
     - IndustrialTrainingCallback
     - EarlyStopping (patience: 8)
     - ReduceLROnPlateau (patience: 3)
     - ModelCheckpoint
   - Eğitim gerçekleştirme

5. **Değerlendirme ve Kaydetme**
   - Model performans değerlendirmesi
   - ONNX ve TensorFlow formatlarında kaydetme
   - Metadata ve metriklerin kaydedilmesi

## Veri Artırma (Data Augmentation)

### Kullanılan Teknikler
- **Rotasyon**: ±20 derece
- **Shift**: Genişlik ve yükseklik için %15
- **Shear**: %15
- **Zoom**: 0.8-1.2 aralığında
- **Horizontal Flip**: Aktif
- **Vertical Flip**: Pasif (endüstriyel görüntüler için)
- **Brightness**: 0.8-1.2 aralığında
- **Fill Mode**: Nearest

### Endüstriyel Özellikler
- Dikey çevirme devre dışı (endüstriyel görüntüler için uygun değil)
- Aydınlatma değişimleri (fabrika ortamı için)
- Gerçekçi zoom aralıkları

## Hata Yönetimi

### Global Exception Handler
- Tüm hataların yakalanması
- Detaylı stack trace çıktısı
- UTF-8 encoding desteği

### Graceful Degradation
- Git olmadan çalışabilme
- Eksik kütüphaneler için uyarı
- Bozuk dosyalar için raporlama

## Çıktı Dosyaları

### Eğitim Sürecinde
- `{model_prefix}_train_log.txt`: Detaylı eğitim logu
- `logs/epoch_metrics.json`: Epoch bazlı metrikler
- `logs/best_model.h5`: En iyi model checkpoint

### Eğitim Sonrası
- `{model_prefix}.onnx`: ONNX formatında model
- `{model_prefix}_tf/`: TensorFlow formatında model
- `{model_prefix}_metadata.json`: Model metadata
- `{model_prefix}_performance.json`: Performans metrikleri
- `{model_prefix}_confusion_matrix.png`: Confusion matrix görseli

## Kullanım Örnekleri

### Temel Kullanım
```bash
python train_model_enhanced.py --classes /path/to/live_dataset
```

### Gelişmiş Kullanım
```bash
python train_model_enhanced.py \
    --classes /path/to/live_dataset \
    --epochs 50 \
    --batch_size 32 \
    --img_size 299 \
    --model_prefix "quality_control_v1" \
    --used_classes "defect,normal,minor_defect" \
    --learning_rate 0.0005 \
    --enable_fine_tuning
```

## Performans Optimizasyonları

### Model Seviyesinde
- EfficientNetB0 tabanlı transfer learning
- Batch normalization ile hızlı yakınsama
- Dropout ile overfitting önleme
- Adaptive learning rate

### Veri Seviyesinde
- Class weights ile imbalanced data handling
- Endüstriyel odaklı data augmentation
- Validation split ile overfitting kontrolü

### Sistem Seviyesinde
- Early stopping ile gereksiz eğitim önleme
- Model checkpoint ile en iyi modeli saklama
- ONNX formatı ile deployment optimizasyonu

## Güvenlik ve Güvenilirlik

### Veri Doğrulama
- Dosya formatı kontrolü
- Bozuk görüntü tespiti
- Sınıf dağılımı analizi

### Model Doğrulama
- Kapsamlı performans metrikleri
- Confusion matrix analizi
- Sınıf bazlı detaylı raporlama

### Metadata Tracking
- Git bilgileri
- Eğitim parametreleri
- Model hash değeri
- Zaman damgaları 