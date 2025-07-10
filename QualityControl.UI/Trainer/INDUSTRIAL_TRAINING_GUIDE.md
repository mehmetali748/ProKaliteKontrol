# Endüstriyel Kalite Kontrol Eğitim Modülü - Kullanım Kılavuzu

## 📋 Genel Bakış

Bu modül, endüstriyel kalite kontrol için gelişmiş makine öğrenmesi eğitimi sağlar. `train_model_enhanced.py` script'ini kullanarak profesyonel seviyede model eğitimi yapabilirsiniz.

## 🚀 Başlangıç

### 1. Ana Formdan Erişim
- Ana uygulamayı başlatın
- "Endüstriyel Eğitim" butonuna tıklayın
- Gelişmiş eğitim formu açılacaktır

### 2. Form Yapısı
Form 3 ana sekmeden oluşur:
- **Eğitim**: Temel eğitim parametreleri
- **Gelişmiş**: Model optimizasyon seçenekleri
- **İzleme**: Eğitim logları ve sonuçlar

## 📊 Eğitim Sekmesi

### Veri Seti Seçimi
1. **Gözat** butonuna tıklayın
2. `live_dataset` klasörünü seçin
3. Sistem otomatik olarak:
   - Sınıf sayısını tespit eder
   - Her sınıftaki resim sayısını sayar
   - Veri kalitesini kontrol eder

### Eğitim Parametreleri
- **Epochs**: Eğitim turu sayısı (varsayılan: 30)
- **Batch Size**: Toplu işlem boyutu (varsayılan: 16)
- **Image Size**: Resim boyutu (varsayılan: 224x224)
- **Learning Rate**: Öğrenme oranı (varsayılan: 0.0001)
- **Model Prefix**: Model dosya adı öneki

### Data Augmentation
- **Scale Min/Max**: Zoom aralığı (varsayılan: 0.8-1.2)
- Sistem otomatik olarak rotation, flip, brightness ayarları yapar

## ⚙️ Gelişmiş Sekmesi

### Model Seçenekleri
- ✅ **Fine-tuning**: Son katmanları eğitilebilir yapar
- ✅ **Class Weights**: Dengesiz veri setleri için ağırlık
- ✅ **Data Quality**: Veri kalitesi kontrolü
- ✅ **Performance Metrics**: Detaylı performans analizi
- ✅ **Model Versioning**: Git tracking ve metadata

### Optimizasyon
- **Optimizer**: Adam, SGD, RMSprop, AdamW
- **Loss Function**: Categorical Crossentropy, vb.
- **Early Stopping**: Overfitting'i önleme (5-20 epoch)
- **Reduce LR**: Öğrenme oranı azaltma (2-8 epoch)

### Konfigürasyon Yönetimi
- **Kaydet**: Mevcut ayarları JSON dosyasına kaydet
- **Yükle**: Önceden kaydedilmiş ayarları yükle

## 📈 İzleme Sekmesi

### Eğitim Logları
- **Gerçek zamanlı log**: Eğitim sürecini canlı izleme
- **Temizle**: Logları temizle
- **Kaydet**: Logları dosyaya kaydet

### Sonuçlar
- **Sonuç Klasörünü Aç**: Eğitim sonuçlarını görüntüle
- Otomatik olarak oluşturulan dosyalar:
  - `model.onnx`: ONNX formatında model
  - `model_metadata.json`: Model bilgileri
  - `model_performance.json`: Performans metrikleri
  - `confusion_matrix.png`: Karışıklık matrisi
  - `epoch_metrics.json`: Epoch bazlı metrikler

## 🔧 Eğitim Süreci

### Başlatma
1. Veri setini seçin
2. Parametreleri ayarlayın
3. "Eğitimi Başlat" butonuna tıklayın
4. Progress bar eğitim durumunu gösterir

### İzleme
- **Log sekmesi**: Gerçek zamanlı eğitim bilgileri
- **Progress bar**: Eğitim ilerlemesi
- **Durdur butonu**: Eğitimi güvenli şekilde durdur

### Tamamlama
- Başarılı eğitim sonrası tüm dosyalar otomatik kaydedilir
- Performans raporu otomatik oluşturulur
- Model ONNX formatında export edilir

## 📁 Çıktı Dosyaları

### Model Dosyaları
```
live_dataset/
├── industrial_model.onnx          # ONNX model
├── industrial_model_tf/           # TensorFlow model
├── industrial_model_metadata.json # Model metadata
├── industrial_model_performance.json # Performans metrikleri
├── industrial_model_confusion_matrix.png # Karışıklık matrisi
├── industrial_model_history.json # Eğitim geçmişi
└── logs/
    ├── epoch_metrics.json        # Epoch metrikleri
    └── best_model.h5            # En iyi model
```

### Metadata İçeriği
```json
{
  "model_name": "industrial_model",
  "created_at": "2024-01-01T12:00:00",
  "git_info": {
    "commit_hash": "abc123...",
    "branch": "main",
    "is_dirty": false
  },
  "training_params": {
    "epochs": 30,
    "batch_size": 16,
    "img_size": 224,
    "learning_rate": 0.0001
  },
  "performance_metrics": {
    "precision": 0.95,
    "recall": 0.93,
    "f1_score": 0.94
  }
}
```

## ⚠️ Önemli Notlar

### Sistem Gereksinimleri
- Python 3.8+ yüklü olmalı
- Gerekli paketler: tensorflow, opencv, scikit-learn, vb.
- Yeterli RAM (en az 8GB önerilir)
- GPU desteği (opsiyonel, hızlandırma için)

### Veri Kalitesi
- Her sınıfta en az 50 resim önerilir
- Resimler PNG, JPG, JPEG formatında olmalı
- Corrupted dosyalar otomatik tespit edilir
- Class imbalance uyarısı verilir

### Performans Optimizasyonu
- Batch size sistem RAM'ine göre ayarlayın
- Image size büyütmek daha iyi sonuç verir ama yavaşlatır
- Fine-tuning sadece yeterli veri varsa etkinleştirin
- Early stopping overfitting'i önler

## 🆘 Sorun Giderme

### Yaygın Hatalar
1. **Python bulunamadı**: Python PATH'e eklenmeli
2. **Veri seti hatası**: Klasör yapısını kontrol edin
3. **Memory hatası**: Batch size'ı azaltın
4. **CUDA hatası**: GPU sürücülerini güncelleyin

### Log Analizi
- Log dosyalarını kontrol edin
- Hata mesajlarını dikkatle okuyun
- Python script çıktısını inceleyin

## 📞 Destek

Sorun yaşarsanız:
1. Log dosyalarını kontrol edin
2. Sistem gereksinimlerini doğrulayın
3. Veri seti yapısını kontrol edin
4. Gerekli Python paketlerini yükleyin

---

**Not**: Bu modül endüstriyel kullanım için tasarlanmıştır. Üretim ortamında kullanmadan önce kapsamlı test yapın. 