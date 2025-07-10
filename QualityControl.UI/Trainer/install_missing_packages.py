#!/usr/bin/env python
# -*- coding: utf-8 -*-

import subprocess
import sys
import os

def install_package(package_name):
    """Paket yükleme fonksiyonu"""
    try:
        print(f"Yükleniyor: {package_name}")
        subprocess.check_call([sys.executable, "-m", "pip", "install", package_name])
        print(f"✓ {package_name} başarıyla yüklendi")
        return True
    except subprocess.CalledProcessError as e:
        print(f"✗ {package_name} yüklenemedi: {e}")
        return False

def main():
    print("=== EKSİK PAKETLER YÜKLENİYOR ===")
    
    # Eksik paketler
    missing_packages = [
        'opencv-python',
        'scikit-learn'
    ]
    
    success_count = 0
    for package in missing_packages:
        if install_package(package):
            success_count += 1
    
    print(f"\n=== YÜKLEME TAMAMLANDI ===")
    print(f"Başarılı: {success_count}/{len(missing_packages)}")
    
    if success_count == len(missing_packages):
        print("✓ Tüm paketler başarıyla yüklendi!")
        return 0
    else:
        print("⚠ Bazı paketler yüklenemedi!")
        return 1

if __name__ == "__main__":
    exit(main()) 