#!/usr/bin/env python
# -*- coding: utf-8 -*-

import onnxruntime as ort
import numpy as np
import cv2
import json
import os
from pathlib import Path

def load_class_mapping(classes_dir):
    """Sınıf isimlerini ve indekslerini yükle"""
    class_dirs = [d for d in os.listdir(classes_dir) if os.path.isdir(os.path.join(classes_dir, d))]
    class_dirs.sort()  # Alfabetik sıralama
    
    # Keras'ın flow_from_directory ile oluşturduğu mapping
    class_indices = {class_name: idx for idx, class_name in enumerate(class_dirs)}
    return class_indices

def preprocess_image(image_path, target_size=(224, 224)):
    """Görüntüyü model için hazırla"""
    # Görüntüyü oku
    img = cv2.imread(image_path)
    if img is None:
        raise ValueError(f"Görüntü okunamadı: {image_path}")
    
    # BGR'den RGB'ye çevir
    img = cv2.cvtColor(img, cv2.COLOR_BGR2RGB)
    
    # Boyutu değiştir
    img = cv2.resize(img, target_size)
    
    # Normalize et (0-255'ten 0-1'e)
    img = img.astype(np.float32) / 255.0
    
    # Batch boyutu ekle
    img = np.expand_dims(img, axis=0)
    
    return img

def predict_with_onnx(model_path, image_path, class_mapping):
    """ONNX model ile tahmin yap"""
    
    # ONNX runtime session oluştur
    session = ort.InferenceSession(model_path)
    
    # Input ve output isimlerini al
    input_name = session.get_inputs()[0].name
    output_name = session.get_outputs()[0].name
    
    # Görüntüyü hazırla
    input_data = preprocess_image(image_path)
    
    # Tahmin yap
    outputs = session.run([output_name], {input_name: input_data})
    predictions = outputs[0]
    
    # Sonuçları işle
    class_probs = predictions[0]  # İlk batch
    predicted_class_idx = np.argmax(class_probs)
    confidence = class_probs[predicted_class_idx]
    
    # Sınıf ismini bul
    predicted_class_name = None
    for class_name, idx in class_mapping.items():
        if idx == predicted_class_idx:
            predicted_class_name = class_name
            break
    
    return {
        'predicted_class_idx': int(predicted_class_idx),
        'predicted_class_name': predicted_class_name,
        'confidence': float(confidence),
        'all_probabilities': class_probs.tolist(),
        'class_mapping': class_mapping
    }

def main():
    # Yolları ayarla
    classes_dir = "live_dataset"  # Sınıf klasörlerinin bulunduğu dizin
    model_path = "model.onnx"     # ONNX model dosyası
    
    print("=== ONNX Model Inference Örneği ===")
    
    # Sınıf mapping'ini yükle
    try:
        class_mapping = load_class_mapping(classes_dir)
        print(f"Sınıf mapping: {class_mapping}")
    except Exception as e:
        print(f"Sınıf mapping yüklenirken hata: {e}")
        return
    
    # Model dosyasını kontrol et
    if not os.path.exists(model_path):
        print(f"Model dosyası bulunamadı: {model_path}")
        return
    
    # Test görüntüsü seç (ilk sınıftan bir örnek)
    test_class = list(class_mapping.keys())[0]
    test_class_dir = os.path.join(classes_dir, test_class)
    
    if os.path.exists(test_class_dir):
        test_images = [f for f in os.listdir(test_class_dir) if f.lower().endswith(('.png', '.jpg', '.jpeg'))]
        if test_images:
            test_image_path = os.path.join(test_class_dir, test_images[0])
            print(f"Test görüntüsü: {test_image_path}")
            
            # Tahmin yap
            try:
                result = predict_with_onnx(model_path, test_image_path, class_mapping)
                
                print("\n=== Tahmin Sonuçları ===")
                print(f"Görüntü: {test_image_path}")
                print(f"Gerçek sınıf: {test_class}")
                print(f"Tahmin edilen sınıf indeksi: {result['predicted_class_idx']}")
                print(f"Tahmin edilen sınıf ismi: {result['predicted_class_name']}")
                print(f"Güven: {result['confidence']:.4f}")
                
                print("\nTüm sınıf olasılıkları:")
                for class_name, idx in class_mapping.items():
                    prob = result['all_probabilities'][idx]
                    print(f"  {class_name}: {prob:.4f}")
                
            except Exception as e:
                print(f"Tahmin sırasında hata: {e}")
        else:
            print(f"{test_class} klasöründe test görüntüsü bulunamadı")
    else:
        print(f"Test sınıf klasörü bulunamadı: {test_class_dir}")

if __name__ == "__main__":
    main() 