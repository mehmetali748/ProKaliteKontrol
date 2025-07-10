using Newtonsoft.Json;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

using OpenCvSharp.Extensions;


namespace QualityControl.UI
{
    public partial class MaİnForm : Form
    {
        private VideoCapture _capture;
        
        private List<Mat>[] _samples;
        private Bitmap _currentFrameBmp;
        private bool _isCapturing = false;
        private string _baseDir;
        private string _pythonExePath;
        private ToolTip toolTip1 = new ToolTip();
        private PictureBox pbStatusCamera = new PictureBox();
        private PictureBox pbStatusTrain = new PictureBox();
        private List<Button> _sampleButtons = new List<Button>();
        private List<TextBox> _classTextBoxes = new List<TextBox>();
        private List<Label> _countLabels = new List<Label>();

        // ROI seçimi için alanlar
        private System.Drawing.Point _roiStartPoint;
        private System.Drawing.Rectangle _roiRect = System.Drawing.Rectangle.Empty;
        private bool _isSelectingRoi = false;

        public MaİnForm()
        {
            InitializeComponent();
            AppendLog("[INFO] Çalışma dizini: " + AppDomain.CurrentDomain.BaseDirectory);
            InitializeDirectories();
            FindPythonExecutable();
            SetupClassControls();

            // ROI mouse event handler'ları
            pictureBoxPreview.MouseDown += pictureBoxPreview_MouseDown;
            pictureBoxPreview.MouseMove += pictureBoxPreview_MouseMove;
            pictureBoxPreview.MouseUp += pictureBoxPreview_MouseUp;
            pictureBoxPreview.Paint += pictureBoxPreview_Paint;
        }

        private void InitializeDirectories()
        {
            try
            {
                // Belgelerim altında QualityControlData/Trainer/live_dataset
                string documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                _baseDir = Path.Combine(documents, "QualityControlData", "Trainer", "live_dataset");
                AppendLog("[INFO] Kayıt dizini: " + _baseDir);
                if (!Directory.Exists(_baseDir))
                {
                    Directory.CreateDirectory(_baseDir);
                    AppendLog($"[INFO] Ana klasör oluşturuldu: {_baseDir}");
                }
            }
            catch (Exception ex)
            {
                AppendLog($"[ERR] Klasör oluşturma hatası: {ex.Message}");
                MessageBox.Show("Klasör oluşturma hatası!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FindPythonExecutable()
        {
            try
            {
                // 1. Önce proje içindeki venv'i kontrol et
                var projectVenv = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "python_env", "venv310", "Scripts", "python.exe");
                if (File.Exists(projectVenv))
                {
                    _pythonExePath = Path.GetFullPath(projectVenv);
                    AppendLog($"[INFO] Proje venv bulundu: {_pythonExePath}");
                    return;
                }

                // 2. Sistem PATH'teki python'u kontrol et
                var systemPython = FindPythonInPath();
                if (!string.IsNullOrEmpty(systemPython))
                {
                    _pythonExePath = systemPython;
                    AppendLog($"[INFO] Sistem Python bulundu: {_pythonExePath}");
                    return;
                }

                // 3. Yaygın Python kurulum yerlerini kontrol et
                var commonPaths = new[]
                {
                    @"C:\Python310\python.exe",
                    @"C:\Python39\python.exe",
                    @"C:\Python38\python.exe",
                    @"C:\Users\" + Environment.UserName + @"\AppData\Local\Programs\Python\Python310\python.exe",
                    @"C:\Users\" + Environment.UserName + @"\AppData\Local\Programs\Python\Python39\python.exe"
                };

                foreach (var path in commonPaths)
                {
                    if (File.Exists(path))
                    {
                        _pythonExePath = path;
                        AppendLog($"[INFO] Python bulundu: {_pythonExePath}");
                        return;
                    }
                }

                throw new Exception("Python executable bulunamadı! Lütfen Python'u yükleyin veya PATH'e ekleyin.");
            }
            catch (Exception ex)
            {
                AppendLog($"[ERR] Python yolu bulma hatası: {ex.Message}");
                MessageBox.Show($"Python bulunamadı!\n\nHata: {ex.Message}\n\nLütfen Python'u yükleyin veya PATH'e ekleyin.", 
                    "Python Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string FindPythonInPath()
        {
            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = "where",
                    Arguments = "python",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                };

                using (var proc = Process.Start(psi))
                {
                    if (proc != null)
                    {
                        var output = proc.StandardOutput.ReadToEnd();
                        proc.WaitForExit();
                        
                        if (proc.ExitCode == 0 && !string.IsNullOrWhiteSpace(output))
                        {
                            var lines = output.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                            return lines.FirstOrDefault()?.Trim();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                AppendLog($"[WARN] PATH'te Python arama hatası: {ex.Message}");
            }
            return null;
        }

        private void SetupClassControls()
        {
            // 2'den 10'a kadar sınıf seçenekleri
            cmbClassCount.Items.AddRange(new object[] { "2", "3", "4", "5", "6", "7", "8", "9", "10" });
            cmbClassCount.SelectedIndex = 0;
            cmbClassCount.SelectedIndexChanged += (s, e) =>
            {
                GenerateSampleControls(int.Parse(cmbClassCount.SelectedItem.ToString()));
            };
            GenerateSampleControls(2);

            // Set default values for augmentation controls
            nudMin.Value = 0.9m;
            nudMax.Value = 1.1m;
            nudLr.Value = 0.0001m;

            btnStartCam.Click += btnStartCam_Click;
            btnStopCam.Click += btnStopCam_Click;
            btnTrain.Click += btnTrain_Click;
            btnIndustrialTraining.Click += btnIndustrialTraining_Click;
        }

        private void GenerateSampleControls(int classCount)
        {
            try
            {
                panelSampleControls.Controls.Clear();
                panelSampleControls.RowStyles.Clear();
                panelSampleControls.RowCount = classCount;
                _samples = new List<Mat>[classCount];

                for (int i = 0; i < classCount; i++)
                {
                    _samples[i] = new List<Mat>();

                    // Sınıf numarası etiketi
                    var lblClassNum = new Label
                    {
                        Name = $"lblClassNum_{i}",
                        Text = $"#{i + 1}",
                        Anchor = AnchorStyles.Left,
                        AutoSize = true,
                        Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                        ForeColor = Color.DarkBlue,
                        TextAlign = ContentAlignment.MiddleLeft,
                        Margin = new Padding(2, 6, 2, 2)
                    };

                    var txt = new TextBox
                    {
                        Name = $"txtClassName_{i}",
                        Text = $"Sınıf{i + 1}",
                        Anchor = AnchorStyles.Left | AnchorStyles.Right,
                        Width = 120,
                        Font = new Font("Segoe UI", 9F),
                        Margin = new Padding(2, 3, 2, 2)
                    };

                    var btn = new Button
                    {
                        Name = $"btnCapture_{i}",
                        Text = "Örnek Yakala",
                        Anchor = AnchorStyles.Left,
                        Tag = i,
                        Width = 100,
                        Height = 28,
                        Font = new Font("Segoe UI", 8F),
                        Margin = new Padding(2, 2, 2, 2)
                    };
                    btn.Click += btnCapture_i_Click;

                    var lbl = new Label
                    {
                        Name = $"lblCount_{i}",
                        Text = "Toplanan: 0",
                        Anchor = AnchorStyles.Left,
                        AutoSize = true,
                        Font = new Font("Segoe UI", 9F),
                        Margin = new Padding(2, 6, 2, 2)
                    };

                    panelSampleControls.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
                    panelSampleControls.Controls.Add(lblClassNum, 0, i);
                    panelSampleControls.Controls.Add(txt, 1, i);
                    panelSampleControls.Controls.Add(btn, 2, i);
                    panelSampleControls.Controls.Add(lbl, 3, i);
                }

                AppendLog($"[INFO] {classCount} sınıf için kontroller oluşturuldu");
            }
            catch (Exception ex)
            {
                AppendLog($"[ERR] Kontrol oluşturma hatası: {ex.Message}");
            }
        }
        
        private void MaİnForm_Load(object sender, EventArgs e)
        {
            AppendLog("[INFO] Arayüz yüklendi.");
            // We don't need to call SetupClassControls from here anymore,
            // cmbClassCount's event handler will do it.
        }

        private void btnStartCam_Click(object sender, EventArgs e)
        {
            try
            {
                if (_capture != null)
                {
                    _capture.Release();
                    _capture = null;
                }

                _capture = new VideoCapture();
                if (!_capture.Open(0))
                {
                    MessageBox.Show("Kamera açılamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                FrameTimer = new Timer { Interval = 33 };
                FrameTimer.Tick += FrameTimer_Tick;
                FrameTimer.Start();
                _isCapturing = true;
                AppendLog("[INFO] Kamera başlatıldı");
            }
            catch (Exception ex)
            {
                AppendLog($"[ERR] Kamera başlatma hatası: {ex.Message}");
                MessageBox.Show("Kamera başlatma hatası!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrameTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (!_isCapturing || _capture == null) return;
                
                using (var frameMat = new Mat())
                {
                    _capture.Read(frameMat);
                    if (frameMat.Empty()) return;
                    
                    if (_currentFrameBmp != null)
                    {
                        _currentFrameBmp.Dispose();
                    }
                    _currentFrameBmp = frameMat.ToBitmap();
                    pictureBoxPreview.Image = _currentFrameBmp;
                }
            }
            catch (Exception ex)
            {
                AppendLog($"[ERR] Frame işleme hatası: {ex.Message}");
            }
        }

        private void btnCapture_i_Click(object sender, EventArgs e)
        {
            try
            {
                if (_currentFrameBmp == null) return;

                int classIndex = (int)((Button)sender).Tag;

                Bitmap bmpToSave = null;
                if (_roiRect != System.Drawing.Rectangle.Empty && _roiRect.Width > 5 && _roiRect.Height > 5)
                {
                    // ROI seçiliyse sadece o bölgeyi kırp
                    System.Drawing.Rectangle bmpRect = new System.Drawing.Rectangle(0, 0, _currentFrameBmp.Width, _currentFrameBmp.Height);
                    System.Drawing.Rectangle roi = System.Drawing.Rectangle.Intersect(_roiRect, bmpRect);
                    if (roi.Width > 0 && roi.Height > 0)
                    {
                        bmpToSave = _currentFrameBmp.Clone(roi, _currentFrameBmp.PixelFormat);
                    }
                }
                if (bmpToSave == null)
                {
                    // ROI yoksa tüm görüntüyü kaydet
                    bmpToSave = (Bitmap)_currentFrameBmp.Clone();
                }

                using (Mat mat = bmpToSave.ToMat())
                {
                    _samples[classIndex].Add(mat.Clone());
                    
                    // HEMEN DISK'E KAYDET
                    SaveSingleSampleToDisk(classIndex, mat.Clone());
                }
                bmpToSave.Dispose();

                var lblCount = panelSampleControls.Controls.Find($"lblCount_{classIndex}", true).FirstOrDefault() as Label;
                if (lblCount != null)
                {
                    lblCount.Text = $"Toplanan: {_samples[classIndex].Count}";
                }

                // ROI'yi sıfırla (her yakalamadan sonra yeni seçim için)
                _roiRect = System.Drawing.Rectangle.Empty;
                pictureBoxPreview.Invalidate();
            }
            catch (Exception ex)
            {
                AppendLog($"[ERR] Örnek yakalama hatası: {ex.Message}");
            }
        }

        private void SaveSingleSampleToDisk(int classIndex, Mat sample)
        {
            try
            {
                // Sınıf ismini al
                var txt = panelSampleControls.Controls
                    .OfType<TextBox>()
                    .First(t => t.Name == $"txtClassName_{classIndex}");
                string className = txt.Text.Trim();
                if (string.IsNullOrWhiteSpace(className))
                    className = $"class_{classIndex}";

                // Klasör oluştur
                var classDir = Path.Combine(_baseDir, className);
                if (!Directory.Exists(classDir))
                {
                    Directory.CreateDirectory(classDir);
                    AppendLog($"[INFO] Sınıf klasörü oluşturuldu: {classDir}");
                }

                // Dosya adı oluştur (mevcut sayı + 1)
                int fileIndex = _samples[classIndex].Count - 1; // Yeni eklenen
                var filePath = Path.Combine(classDir, $"{fileIndex:0000}.png");
                
                // Kaydet
                sample.SaveImage(filePath);
                AppendLog($"[INFO] Anlık kaydedildi: {filePath}");
            }
            catch (Exception ex)
            {
                AppendLog($"[ERR] Anlık kaydetme hatası: {ex.Message}");
            }
        }

        private void btnStopCam_Click(object sender, EventArgs e)
        {
            try
            {
                _isCapturing = false;
                if (FrameTimer != null)
                {
                    FrameTimer.Stop();
                    FrameTimer.Dispose();
                }
                if (_capture != null)
                {
                    _capture.Release();
                }
                pictureBoxPreview.Image = null;
                AppendLog("[INFO] Kamera durduruldu");
            }
            catch (Exception ex)
            {
                AppendLog($"[ERR] Kamera durdurma hatası: {ex.Message}");
            }
        }
        
        private void SaveLiveSamplesToDisk()
        {
             try
             {
                 // Ensure _baseDir is set to the user's Documents folder
                 string documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                 _baseDir = Path.Combine(documents, "QualityControlData", "Trainer", "live_dataset");
                 AppendLog($"[INFO] Kayıt dizini: {_baseDir}");
 
                 if (!Directory.Exists(_baseDir))
                 {
                     Directory.CreateDirectory(_baseDir);
                     AppendLog($"[INFO] Ana klasör oluşturuldu: {_baseDir}");
                 }
 
                 // Örnekler zaten anlık olarak kaydedildiği için sadece kontrol yap
                 int totalFiles = 0;
                 for (int i = 0; i < _samples.Length; i++)
                 {
                     if (_samples[i].Count == 0) continue;
 
                     // Sınıf ismini textbox'tan al
                     var txt = panelSampleControls.Controls
                         .OfType<TextBox>()
                         .First(t => t.Name == $"txtClassName_{i}");
                     string className = txt.Text.Trim();
                     if (string.IsNullOrWhiteSpace(className))
                         className = $"class_{i}"; // Yedek olarak
                     
                     var classDir = Path.Combine(_baseDir, className);
                     
                     // Dosya sayısını kontrol et
                     if (Directory.Exists(classDir))
                     {
                         var files = Directory.GetFiles(classDir, "*.png");
                         totalFiles += files.Length;
                         AppendLog($"[INFO] Sınıf {className}: {files.Length} dosya mevcut");
                     }
                     else
                     {
                         AppendLog($"[WARN] Sınıf klasörü bulunamadı: {classDir}");
                     }
                 }
 
                 if (totalFiles == 0)
                 {
                     throw new Exception("Hiç örnek dosyası bulunamadı!");
                 }
 
                 AppendLog($"[INFO] Toplam {totalFiles} örnek dosyası hazır");
             }
             catch (Exception ex)
             {
                 AppendLog($"[ERR] Kayıt kontrolü hatası: {ex.Message}");
                 throw;
             }
        }
        
        private async void btnTrain_Click(object sender, EventArgs e)
        {
            Process proc = null;
            try
            {
                progressBarTrain.Value = 0;
                progressBarTrain.Visible = true;
                pbStatus.Image = SystemIcons.Information.ToBitmap();

                if (string.IsNullOrEmpty(_pythonExePath))
                {
                    MessageBox.Show("Python yolu bulunamadı! Lütfen Python'u yükleyin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                SaveLiveSamplesToDisk();

                var scriptPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Trainer", "train_model.py");
                if (!File.Exists(scriptPath))
                {
                    throw new Exception($"train_model.py bulunamadı: {scriptPath}");
                }

                var classNames = panelSampleControls.Controls
                    .OfType<TextBox>()
                    .Select(t => t.Text.Trim())
                    .Where(t => !string.IsNullOrWhiteSpace(t))
                    .ToArray();

                if (classNames.Length == 0)
                {
                    throw new Exception("Hiç sınıf ismi girilmemiş!");
                }
                
                string usedClasses = string.Join(",", classNames);

                var args = string.Join(" ",
                    $"--classes \"{_baseDir}\"",
                    $"--epochs {nudEpoch.Value}",
                    $"--batch_size {nudBatchSize.Value}",
                    $"--img_size 224",
                    $"--model_prefix \"{string.Join("_", classNames)}\"",
                    $"--used_classes \"{usedClasses}\"",
                    $"--learning_rate {nudLr.Value.ToString(CultureInfo.InvariantCulture)}",
                    $"--scale_min {nudMin.Value.ToString(CultureInfo.InvariantCulture)}",
                    $"--scale_max {nudMax.Value.ToString(CultureInfo.InvariantCulture)}"
                );

                AppendLog($"[DEBUG] Python args: {args}\n");

                var psi = new ProcessStartInfo
                {
                    FileName = _pythonExePath,
                    Arguments = $"\"{scriptPath}\" {args}",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    StandardOutputEncoding = System.Text.Encoding.UTF8,
                    StandardErrorEncoding = System.Text.Encoding.UTF8,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    WorkingDirectory = Path.GetDirectoryName(scriptPath)
                };

                AppendLog($"[INFO] Using Python: {_pythonExePath}\n");
                AppendLog($"[INFO] Using Script: {scriptPath}\n");
                
                progressBarTrain.Style = ProgressBarStyle.Marquee;
                btnTrain.Enabled = false;
                lstLog.Items.Clear();
                lstLog.Items.Add(new ListViewItem("[INFO] Eğitim başlatılıyor..."));

                using (proc = new Process { StartInfo = psi })
                {
                    proc.OutputDataReceived += (s, ev) =>
                    {
                        if (ev.Data != null)
                        {
                            lstLog.Invoke(new Action(() =>
                            {
                                lstLog.Items.Add(new ListViewItem(ev.Data));

                                if (ev.Data.StartsWith("[PROGRESS]"))
                                {
                                    var match = System.Text.RegularExpressions.Regex.Match(ev.Data, @"Epoch (\d+)");
                                    if (match.Success)
                                    {
                                        int epoch = int.Parse(match.Groups[1].Value);
                                        int totalEpochs = (int)nudEpoch.Value;
                                        progressBarTrain.Value = Math.Min((int)(100.0 * epoch / totalEpochs), 100);
                                    }
                                }

                                if (ev.Data.Contains("EĞİTİM TAMAMLANDI"))
                                {
                                    progressBarTrain.Value = 100;
                                    pbStatus.Image = SystemIcons.Shield.ToBitmap();
                                }
                            }));
                        }
                    };
                    proc.ErrorDataReceived += (s, ev) => { if (ev.Data != null) lstLog.Invoke(new Action(() => lstLog.Items.Add(new ListViewItem("[ERR] " + ev.Data)))); };

                    proc.Start();
                    proc.BeginOutputReadLine();
                    proc.BeginErrorReadLine();
                    
                    await Task.Run(() => proc.WaitForExit());

                    if (proc.ExitCode == 0)
                    {
                        MessageBox.Show("Eğitim tamamlandı!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        throw new Exception($"Python scripti hata ile sonlandı (ExitCode: {proc.ExitCode})");
                    }
                }
            }
            catch (Exception ex)
            {
                lstLog.Items.Add(new ListViewItem($"[ERR] Eğitim hatası: {ex.Message}"));
                MessageBox.Show($"Eğitim sırasında bir hata oluştu. Detaylar log ekranında.\n\nHata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                progressBarTrain.Style = ProgressBarStyle.Blocks;
                progressBarTrain.Visible = false;
                btnTrain.Enabled = true;
                pbStatus.Image = SystemIcons.Information.ToBitmap();
            }
        }
        
        private void AppendLog(string text)
        {
            if (lstLog.InvokeRequired)
            {
                lstLog.Invoke(new Action(() => AppendLog(text)));
            }
            else
            {
                var item = new ListViewItem(text);
                // Renk kodlama: Hata (ERR) kırmızı, Uyarı (WARN) turuncu, Bilgi (INFO) yeşil
                if (text.Contains("[ERR]"))
                    item.ForeColor = Color.Red;
                else if (text.Contains("[WARN]"))
                    item.ForeColor = Color.Orange;
                else if (text.Contains("[INFO]"))
                    item.ForeColor = Color.Green;
                else if (text.Contains("[FATAL]"))
                    item.ForeColor = Color.DarkRed;
                else if (text.Contains("[PROGRESS]"))
                    item.ForeColor = Color.Blue;
                lstLog.Items.Add(item);
                lstLog.EnsureVisible(lstLog.Items.Count - 1);
            }
        }

        private void btnIndustrialTraining_Click(object sender, EventArgs e)
        {
            try
            {
                AppendLog("[INFO] Endüstriyel eğitim formu açılıyor...");
                
                // Endüstriyel eğitim formunu aç
                using (var industrialForm = new IndustrialTrainingForm())
                {
                    industrialForm.ShowDialog();
                }
                
                AppendLog("[INFO] Endüstriyel eğitim formu kapatıldı.");
            }
            catch (Exception ex)
            {
                AppendLog($"[ERR] Endüstriyel eğitim formu açma hatası: {ex.Message}");
                MessageBox.Show($"Endüstriyel eğitim formu açılamadı!\n\nHata: {ex.Message}", 
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pictureBoxPreview_MouseDown(object sender, MouseEventArgs e)
        {
            if (_currentFrameBmp == null || e.Button != MouseButtons.Left) return;
            _isSelectingRoi = true;
            _roiStartPoint = e.Location;
            _roiRect = new System.Drawing.Rectangle(e.Location, new System.Drawing.Size(0, 0));
            pictureBoxPreview.Invalidate();
        }

        private void pictureBoxPreview_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isSelectingRoi && _currentFrameBmp != null)
            {
                int x = Math.Min(_roiStartPoint.X, e.X);
                int y = Math.Min(_roiStartPoint.Y, e.Y);
                int w = Math.Abs(_roiStartPoint.X - e.X);
                int h = Math.Abs(_roiStartPoint.Y - e.Y);
                _roiRect = new System.Drawing.Rectangle(x, y, w, h);
                pictureBoxPreview.Invalidate();
            }
        }

        private void pictureBoxPreview_MouseUp(object sender, MouseEventArgs e)
        {
            if (_isSelectingRoi)
            {
                _isSelectingRoi = false;
                pictureBoxPreview.Invalidate();
            }
        }

        private void pictureBoxPreview_Paint(object sender, PaintEventArgs e)
        {
            if (_roiRect != System.Drawing.Rectangle.Empty && _roiRect.Width > 5 && _roiRect.Height > 5)
            {
                using (var pen = new Pen(Color.Red, 2))
                {
                    e.Graphics.DrawRectangle(pen, _roiRect);
                }
            }
        }

        private void MaİnForm_Load_1(object sender, EventArgs e)
        {

        }
    }
}

