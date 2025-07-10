#!/usr/bin/env python
# -*- coding: utf-8 -*-

import onnxruntime as ort
import numpy as np
import cv2
import json
import os
import time
from pathlib import Path
import threading
from collections import deque

class LiveQualityControl:
    def __init__(self, model_path, class_mapping, confidence_threshold=0.7):
        """
        Canlı kalite kontrol sistemi
        
        Args:
            model_path: ONNX model dosyası yolu
            class_mapping: Sınıf indekslerini isimlere eşleyen dictionary
            confidence_threshold: Minimum güven eşiği
        """
        self.model_path = model_path
        self.class_mapping = class_mapping
        self.confidence_threshold = confidence_threshold
        self.num_classes = len(class_mapping)
        
        # ONNX runtime session
        self.session = ort.InferenceSession(model_path)
        self.input_name = self.session.get_inputs()[0].name
        self.output_name = self.session.get_outputs()[0].name
        
        # Sonuç geçmişi (son 10 tahmin)
        self.prediction_history = deque(maxlen=10)
        
        print(f"ONNX Model yüklendi: {model_path}")
        print(f"Sınıf sayısı: {self.num_classes}")
        print(f"Güven eşiği: {confidence_threshold}")
        print("Sınıf mapping:")
        for idx, name in class_mapping.items():
            print(f"  {idx}: {name}")

    def preprocess_image(self, image, target_size=(224, 224)):
        """Görüntüyü model için hazırla"""
        # BGR'den RGB'ye çevir
        if len(image.shape) == 3:
            image = cv2.cvtColor(image, cv2.COLOR_BGR2RGB)
        
        # Boyutu değiştir
        image = cv2.resize(image, target_size)
        
        # Normalize et (0-255'ten 0-1'e)
        image = image.astype(np.float32) / 255.0
        
        # Batch boyutu ekle
        image = np.expand_dims(image, axis=0)
        
        return image

    def predict_single_image(self, image):
        """Tek görüntü için tahmin yap"""
        try:
            # Görüntüyü hazırla
            input_data = self.preprocess_image(image)
            
            # Tahmin yap
            start_time = time.time()
            outputs = self.session.run([self.output_name], {self.input_name: input_data})
            inference_time = time.time() - start_time
            
            predictions = outputs[0]
            class_probs = predictions[0]  # İlk batch
            
            # En yüksek olasılıklı sınıfı bul
            predicted_class_idx = np.argmax(class_probs)
            confidence = class_probs[predicted_class_idx]
            
            # Sınıf ismini al
            predicted_class_name = self.class_mapping.get(predicted_class_idx, f"Unknown_{predicted_class_idx}")
            
            # Tüm sınıf olasılıklarını al
            all_probabilities = {}
            for idx, prob in enumerate(class_probs):
                class_name = self.class_mapping.get(idx, f"Unknown_{idx}")
                all_probabilities[class_name] = float(prob)
            
            result = {
                'class_idx': int(predicted_class_idx),
                'class_name': predicted_class_name,
                'confidence': float(confidence),
                'all_probabilities': all_probabilities,
                'inference_time': inference_time,
                'timestamp': time.time(),
                'is_defect': predicted_class_idx != 0,  # 0 = normal
                'above_threshold': confidence >= self.confidence_threshold
            }
            
            # Geçmişe ekle
            self.prediction_history.append(result)
            
            return result
            
        except Exception as e:
            print(f"Tahmin hatası: {e}")
            return None

    def predict_live_camera(self, camera_id=0, display=True):
        """Canlı kamera ile sürekli tahmin"""
        cap = cv2.VideoCapture(camera_id)
        
        if not cap.isOpened():
            print(f"Kamera {camera_id} açılamadı!")
            return
        
        print("Canlı kalite kontrol başladı. 'q' tuşu ile çıkın.")
        
        try:
            while True:
                ret, frame = cap.read()
                if not ret:
                    print("Frame okunamadı!")
                    break
                
                # Tahmin yap
                result = self.predict_single_image(frame)
                
                if result and display:
                    # Görüntüye sonuçları ekle
                    self.draw_results_on_frame(frame, result)
                    
                    # Görüntüyü göster
                    cv2.imshow('Live Quality Control', frame)
                
                # 'q' tuşu ile çık
                if cv2.waitKey(1) & 0xFF == ord('q'):
                    break
                    
        finally:
            cap.release()
            cv2.destroyAllWindows()

    def draw_results_on_frame(self, frame, result):
        """Sonuçları görüntüye çiz"""
        # Arka plan
        cv2.rectangle(frame, (10, 10), (400, 200), (0, 0, 0), -1)
        cv2.rectangle(frame, (10, 10), (400, 200), (255, 255, 255), 2)
        
        # Ana sonuç
        color = (0, 255, 0) if not result['is_defect'] else (0, 0, 255)
        status = "NORMAL" if not result['is_defect'] else "KUSUR"
        
        cv2.putText(frame, f"Durum: {status}", (20, 40), 
                   cv2.FONT_HERSHEY_SIMPLEX, 0.8, color, 2)
        
        cv2.putText(frame, f"Sınıf: {result['class_name']}", (20, 70), 
                   cv2.FONT_HERSHEY_SIMPLEX, 0.6, (255, 255, 255), 2)
        
        cv2.putText(frame, f"Güven: {result['confidence']:.3f}", (20, 100), 
                   cv2.FONT_HERSHEY_SIMPLEX, 0.6, (255, 255, 255), 2)
        
        cv2.putText(frame, f"Süre: {result['inference_time']*1000:.1f}ms", (20, 130), 
                   cv2.FONT_HERSHEY_SIMPLEX, 0.6, (255, 255, 255), 2)
        
        # Güven eşiği kontrolü
        if not result['above_threshold']:
            cv2.putText(frame, "DÜŞÜK GÜVEN!", (20, 160), 
                       cv2.FONT_HERSHEY_SIMPLEX, 0.6, (0, 255, 255), 2)

    def get_statistics(self):
        """Son tahminlerin istatistiklerini al"""
        if not self.prediction_history:
            return None
        
        total_predictions = len(self.prediction_history)
        defect_count = sum(1 for pred in self.prediction_history if pred['is_defect'])
        normal_count = total_predictions - defect_count
        
        avg_confidence = np.mean([pred['confidence'] for pred in self.prediction_history])
        avg_inference_time = np.mean([pred['inference_time'] for pred in self.prediction_history])
        
        # Sınıf dağılımı
        class_counts = {}
        for pred in self.prediction_history:
            class_name = pred['class_name']
            class_counts[class_name] = class_counts.get(class_name, 0) + 1
        
        return {
            'total_predictions': total_predictions,
            'defect_count': defect_count,
            'normal_count': normal_count,
            'defect_rate': defect_count / total_predictions if total_predictions > 0 else 0,
            'avg_confidence': avg_confidence,
            'avg_inference_time': avg_inference_time,
            'class_distribution': class_counts
        }

    def save_prediction_log(self, filename="prediction_log.json"):
        """Tahmin geçmişini dosyaya kaydet"""
        log_data = {
            'timestamp': time.time(),
            'model_path': self.model_path,
            'class_mapping': self.class_mapping,
            'confidence_threshold': self.confidence_threshold,
            'predictions': list(self.prediction_history)
        }
        
        with open(filename, 'w', encoding='utf-8') as f:
            json.dump(log_data, f, indent=2, ensure_ascii=False)
        
        print(f"Tahmin geçmişi kaydedildi: {filename}")

def main():
    # 10 sınıflı sistem için sınıf mapping
    class_mapping = {
        0: "normal",           # Normal ürün
        1: "scratch",          # Çizik
        2: "dent",             # Çökme
        3: "stain",            # Leke
        4: "crack",            # Çatlak
        5: "hole",             # Delik
        6: "color_defect",     # Renk hatası
        7: "size_defect",      # Boyut hatası
        8: "surface_defect",   # Yüzey hatası
        9: "assembly_defect"   # Montaj hatası
    }
    
    # Model yolu
    model_path = "model.onnx"  # ONNX model dosyası
    
    # Dosya kontrolü
    if not os.path.exists(model_path):
        print(f"Model dosyası bulunamadı: {model_path}")
        print("Lütfen önce model eğitimi yapın.")
        return
    
    # Canlı kalite kontrol sistemi oluştur
    qc_system = LiveQualityControl(
        model_path=model_path,
        class_mapping=class_mapping,
        confidence_threshold=0.7
    )
    
    print("\n=== 10 SINIFLI CANLI KALİTE KONTROL SİSTEMİ ===")
    print("1. Canlı kamera ile kontrol")
    print("2. Tek görüntü test et")
    print("3. İstatistikleri göster")
    print("4. Tahmin geçmişini kaydet")
    print("5. Çıkış")
    
    while True:
        choice = input("\nSeçiminiz (1-5): ").strip()
        
        if choice == "1":
            print("Canlı kamera başlatılıyor...")
            qc_system.predict_live_camera()
            
        elif choice == "2":
            image_path = input("Görüntü dosyası yolu: ").strip()
            if os.path.exists(image_path):
                image = cv2.imread(image_path)
                result = qc_system.predict_single_image(image)
                if result:
                    print(f"\nSonuç:")
                    print(f"  Sınıf: {result['class_name']}")
                    print(f"  Güven: {result['confidence']:.3f}")
                    print(f"  Kusur: {'Evet' if result['is_defect'] else 'Hayır'}")
                    print(f"  Süre: {result['inference_time']*1000:.1f}ms")
            else:
                print("Dosya bulunamadı!")
                
        elif choice == "3":
            stats = qc_system.get_statistics()
            if stats:
                print(f"\nİstatistikler:")
                print(f"  Toplam tahmin: {stats['total_predictions']}")
                print(f"  Kusur sayısı: {stats['defect_count']}")
                print(f"  Normal sayısı: {stats['normal_count']}")
                print(f"  Kusur oranı: {stats['defect_rate']:.2%}")
                print(f"  Ortalama güven: {stats['avg_confidence']:.3f}")
                print(f"  Ortalama süre: {stats['avg_inference_time']*1000:.1f}ms")
                print(f"  Sınıf dağılımı: {stats['class_distribution']}")
            else:
                print("Henüz tahmin yapılmadı!")
                
        elif choice == "4":
            qc_system.save_prediction_log()
            
        elif choice == "5":
            print("Sistem kapatılıyor...")
            break
            
        else:
            print("Geçersiz seçim!")

if __name__ == "__main__":
    main() 