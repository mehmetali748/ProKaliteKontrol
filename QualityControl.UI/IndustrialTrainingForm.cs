using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Globalization;

namespace QualityControl.UI
{
    public partial class IndustrialTrainingForm : Form
    {
        private string _pythonExePath;
        private Process _trainingProcess;
        private bool _isTraining = false;
        private Dictionary<string, object> _trainingConfig;
        private List<string> _logMessages = new List<string>();
        private Timer _logUpdateTimer;
        private string _selectedTrainingScript = "train_model_enhanced.py";

        public IndustrialTrainingForm()
        {
            InitializeComponent();
            InitializeForm();
            LoadDefaultConfig();
        }

        private void InitializeForm()
        {
            // Form başlığı ve boyutu
            this.Text = "Endüstriyel Kalite Kontrol - Gelişmiş Eğitim Modülü";
            this.Size = new Size(1200, 900);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Python yolu bulma
            FindPythonExecutable();
            
            // Log timer
            _logUpdateTimer = new Timer();
            _logUpdateTimer.Interval = 1000;
            _logUpdateTimer.Tick += LogUpdateTimer_Tick;

            // Event handler'ları bağla
            btnBrowseDataset.Click += btnBrowseDataset_Click;
            btnStartTraining.Click += btnStartTraining_Click;
            btnStopTraining.Click += btnStopTraining_Click;
            btnOpenResults.Click += btnOpenResults_Click;
            btnSaveConfig.Click += btnSaveConfig_Click;
            btnLoadConfig.Click += btnLoadConfig_Click;
            btnClearLog.Click += btnClearLog_Click;
            btnSaveLog.Click += btnSaveLog_Click;
            cmbScript.SelectedIndexChanged += cmbScript_SelectedIndexChanged;
            cmbResNetVariant.SelectedIndexChanged += cmbResNetVariant_SelectedIndexChanged;
        }

        private void LoadDefaultConfig()
        {
            // Varsayılan değerleri yükle
            nudEpochs.Value = 30;
            nudBatchSize.Value = 16;
            nudImageSize.Value = 224;
            nudLearningRate.Value = 0.0001m;
            nudScaleMin.Value = 0.8m;
            nudScaleMax.Value = 1.2m;
            txtModelPrefix.Text = "industrial_model";
            
            // Checkbox varsayılan değerleri
            chkEnableFineTuning.Checked = true;
            chkEnableClassWeights.Checked = true;
            chkEnableDataQuality.Checked = true;
            chkEnablePerformanceMetrics.Checked = true;
            chkEnableModelVersioning.Checked = true;
            
            // ComboBox varsayılan değerleri
            cmbOptimizer.SelectedIndex = 0; // Adam
            cmbLossFunction.SelectedIndex = 0; // Categorical Crossentropy
            cmbEarlyStopping.SelectedIndex = 1; // 8 epochs
            cmbReduceLR.SelectedIndex = 1; // 3 epochs
        }

        private void FindPythonExecutable()
        {
            try
            {
                // Sabit ve kesin python yolu
                var projectVenv = @"C:\\Users\\malib\\source\\repos\\YETTTERRRR\\python_env\\venv310\\Scripts\\python.exe";
                AppendLog($"[DEBUG] Kontrol edilen python yolu: {projectVenv}");
                if (File.Exists(projectVenv))
                {
                    _pythonExePath = projectVenv;
                    lblPythonPath.Text = $"Python: {Path.GetFileName(_pythonExePath)}";
                    lblPythonPath.ForeColor = Color.Green;
                    AppendLog($"[INFO] Proje venv bulundu: {_pythonExePath}");
                    return;
                }
                throw new Exception($"Proje içi sanal ortam bulunamadı! Kontrol edilen yol: {projectVenv}");
            }
            catch (Exception ex)
            {
                lblPythonPath.Text = "Python: Hata!";
                lblPythonPath.ForeColor = Color.Red;
                AppendLog($"[ERR] Python yolu bulma hatası: {ex.Message}");
                MessageBox.Show($"Python bulunamadı!\n\nHata: {ex.Message}\n\nLütfen python_env/venv310 ortamını oluşturun.", "Python Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBrowseDataset_Click(object sender, EventArgs e)
        {
            using (var folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "Veri seti klasörünü seçin (live_dataset)";
                folderDialog.ShowNewFolderButton = false;
                
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    txtDatasetPath.Text = folderDialog.SelectedPath;
                    ValidateDatasetPath();
                }
            }
        }

        private void ValidateDatasetPath()
        {
            if (string.IsNullOrEmpty(txtDatasetPath.Text))
            {
                lblDatasetStatus.Text = "Veri seti: Seçilmedi";
                lblDatasetStatus.ForeColor = Color.Red;
                return;
            }

            if (!Directory.Exists(txtDatasetPath.Text))
            {
                lblDatasetStatus.Text = "Veri seti: Klasör bulunamadı";
                lblDatasetStatus.ForeColor = Color.Red;
                return;
            }

            // Alt klasörleri kontrol et
            var subdirs = Directory.GetDirectories(txtDatasetPath.Text);
            if (subdirs.Length == 0)
            {
                lblDatasetStatus.Text = "Veri seti: Alt klasör bulunamadı";
                lblDatasetStatus.ForeColor = Color.Orange;
                return;
            }

            var totalImages = 0;
            var classInfo = new List<string>();
            
            foreach (var subdir in subdirs)
            {
                var className = Path.GetFileName(subdir);
                var images = Directory.GetFiles(subdir, "*.png").Length + 
                           Directory.GetFiles(subdir, "*.jpg").Length +
                           Directory.GetFiles(subdir, "*.jpeg").Length;
                totalImages += images;
                classInfo.Add($"{className}: {images} resim");
            }

            lblDatasetStatus.Text = $"Veri seti: {subdirs.Length} sınıf, {totalImages} resim";
            lblDatasetStatus.ForeColor = Color.Green;
            
            // Sınıf bilgilerini göster
            txtClassInfo.Text = string.Join(Environment.NewLine, classInfo);
        }

        private void btnStartTraining_Click(object sender, EventArgs e)
        {
            if (_isTraining)
            {
                MessageBox.Show("Eğitim zaten devam ediyor!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateTrainingConfig())
            {
                return;
            }

            StartTraining();
        }

        private bool ValidateTrainingConfig()
        {
            if (string.IsNullOrEmpty(_pythonExePath))
            {
                MessageBox.Show("Python yolu bulunamadı! Lütfen Python'u yükleyin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (string.IsNullOrEmpty(txtDatasetPath.Text) || !Directory.Exists(txtDatasetPath.Text))
            {
                MessageBox.Show("Geçerli bir veri seti klasörü seçin!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (nudScaleMin.Value >= nudScaleMax.Value)
            {
                MessageBox.Show("Scale Min değeri Scale Max değerinden küçük olmalıdır!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (_selectedTrainingScript == "train_model_resnet.py" && cmbResNetVariant.SelectedItem.ToString() == "ResNet152" && nudBatchSize.Value > 8)
            {
                var result = MessageBox.Show(
                    "ResNet152 için batch size 8'den büyük olmamalıdır. Otomatik olarak 8'e ayarlansın mı?",
                    "Batch Size Uyarısı",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    nudBatchSize.Value = 8;
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        private void StartTraining()
        {
            try
            {
                _isTraining = true;
                btnStartTraining.Enabled = false;
                btnStopTraining.Enabled = true;
                progressBarTraining.Style = ProgressBarStyle.Marquee;
                
                // Log dosyasını temizle
                txtTrainingLog.Clear();
                _logMessages.Clear();

                // Training konfigürasyonunu oluştur
                var config = BuildTrainingConfig();
                
                // Python script'ini çalıştır
                var scriptPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Trainer", _selectedTrainingScript);
                
                if (!File.Exists(scriptPath))
                {
                    MessageBox.Show("train_model_enhanced.py dosyası bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var arguments = BuildPythonArguments(config);
                
                AppendLog($"[INFO] Eğitim başlatılıyor...");
                AppendLog($"[INFO] Script: {scriptPath}");
                AppendLog($"[INFO] Arguments: {arguments}");

                _trainingProcess = new Process();
                _trainingProcess.StartInfo.FileName = _pythonExePath;
                _trainingProcess.StartInfo.Arguments = $"\"{scriptPath}\" {arguments}";
                _trainingProcess.StartInfo.UseShellExecute = false;
                _trainingProcess.StartInfo.RedirectStandardOutput = true;
                _trainingProcess.StartInfo.RedirectStandardError = true;
                _trainingProcess.StartInfo.CreateNoWindow = true;
                _trainingProcess.StartInfo.WorkingDirectory = Path.GetDirectoryName(scriptPath);
                _trainingProcess.StartInfo.StandardOutputEncoding = Encoding.UTF8;
                _trainingProcess.StartInfo.StandardErrorEncoding = Encoding.UTF8;

                _trainingProcess.OutputDataReceived += (s, e) =>
                {
                    if (!string.IsNullOrEmpty(e.Data))
                    {
                        _logMessages.Add(e.Data);
                    }
                };

                _trainingProcess.ErrorDataReceived += (s, e) =>
                {
                    if (!string.IsNullOrEmpty(e.Data))
                    {
                        _logMessages.Add($"[ERROR] {e.Data}");
                    }
                };

                _trainingProcess.EnableRaisingEvents = true;
                _trainingProcess.Exited += TrainingProcess_Exited;

                _trainingProcess.Start();
                _trainingProcess.BeginOutputReadLine();
                _trainingProcess.BeginErrorReadLine();

                _logUpdateTimer.Start();
                
                AppendLog("[INFO] Eğitim süreci başlatıldı");
            }
            catch (Exception ex)
            {
                AppendLog($"[FATAL] Eğitim başlatma hatası: {ex.Message}");
                MessageBox.Show($"Eğitim başlatılamadı!\n\nHata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                StopTraining();
            }
        }

        private Dictionary<string, object> BuildTrainingConfig()
        {
            var config = new Dictionary<string, object>
            {
                ["classes"] = txtDatasetPath.Text,
                ["epochs"] = (int)nudEpochs.Value,
                ["batch_size"] = (int)nudBatchSize.Value,
                ["img_size"] = (int)nudImageSize.Value,
                ["model_prefix"] = txtModelPrefix.Text,
                ["learning_rate"] = (double)nudLearningRate.Value,
                ["scale_min"] = (double)nudScaleMin.Value,
                ["scale_max"] = (double)nudScaleMax.Value,
                ["enable_fine_tuning"] = chkEnableFineTuning.Checked,
                ["enable_class_weights"] = chkEnableClassWeights.Checked,
                ["enable_data_quality"] = chkEnableDataQuality.Checked,
                ["enable_performance_metrics"] = chkEnablePerformanceMetrics.Checked,
                ["enable_model_versioning"] = chkEnableModelVersioning.Checked,
                ["optimizer"] = cmbOptimizer.SelectedItem.ToString(),
                ["loss_function"] = cmbLossFunction.SelectedItem.ToString(),
                ["early_stopping_patience"] = int.Parse(cmbEarlyStopping.SelectedItem.ToString()),
                ["reduce_lr_patience"] = int.Parse(cmbReduceLR.SelectedItem.ToString())
            };

            _trainingConfig = config;
            return config;
        }

        private string BuildPythonArguments(Dictionary<string, object> config)
        {
            var args = new List<string>
            {
                $"--classes \"{config["classes"]}\"",
                $"--epochs {config["epochs"]}",
                $"--batch_size {config["batch_size"]}",
                $"--img_size {config["img_size"]}",
                $"--model_prefix \"{config["model_prefix"]}\"",
                $"--learning_rate {((double)config["learning_rate"]).ToString(CultureInfo.InvariantCulture)}",
                $"--scale_min {((double)config["scale_min"]).ToString(CultureInfo.InvariantCulture)}",
                $"--scale_max {((double)config["scale_max"]).ToString(CultureInfo.InvariantCulture)}"
            };

            if ((bool)config["enable_fine_tuning"])
            {
                args.Add("--enable_fine_tuning");
            }

            if (_selectedTrainingScript == "train_model_resnet.py")
            {
                args.Add($"--resnet_variant {cmbResNetVariant.SelectedItem.ToString().ToLower()}");
            }

            return string.Join(" ", args);
        }

        private void TrainingProcess_Exited(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => TrainingProcess_Exited(sender, e)));
                return;
            }

            _logUpdateTimer.Stop();
            _isTraining = false;
            btnStartTraining.Enabled = true;
            btnStopTraining.Enabled = false;
            progressBarTraining.Style = ProgressBarStyle.Continuous;
            progressBarTraining.Value = 0;

            if (_trainingProcess.ExitCode == 0)
            {
                AppendLog("[SUCCESS] Eğitim başarıyla tamamlandı!");
                progressBarTraining.Value = 100;
                MessageBox.Show("Eğitim başarıyla tamamlandı!", "Başarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                AppendLog($"[ERROR] Eğitim hatayla sonlandı! Exit Code: {_trainingProcess.ExitCode}");
                MessageBox.Show($"Eğitim hatayla sonlandı!\nExit Code: {_trainingProcess.ExitCode}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnStopTraining_Click(object sender, EventArgs e)
        {
            StopTraining();
        }

        private void StopTraining()
        {
            if (_trainingProcess != null && !_trainingProcess.HasExited)
            {
                try
                {
                    _trainingProcess.Kill();
                    AppendLog("[INFO] Eğitim süreci durduruldu");
                }
                catch (Exception ex)
                {
                    AppendLog($"[WARN] Süreç durdurma hatası: {ex.Message}");
                }
            }

            _logUpdateTimer.Stop();
            _isTraining = false;
            btnStartTraining.Enabled = true;
            btnStopTraining.Enabled = false;
            progressBarTraining.Style = ProgressBarStyle.Continuous;
            progressBarTraining.Value = 0;
        }

        private void LogUpdateTimer_Tick(object sender, EventArgs e)
        {
            if (_logMessages.Count > 0)
            {
                var newMessages = _logMessages.ToArray();
                _logMessages.Clear();
                
                foreach (var message in newMessages)
                {
                    AppendLog(message);
                }
            }
        }

        private void AppendLog(string message)
        {
            if (txtTrainingLog.InvokeRequired)
            {
                txtTrainingLog.Invoke(new Action<string>(AppendLog), message);
                return;
            }

            var timestamp = DateTime.Now.ToString("HH:mm:ss");
            var logMessage = $"[{timestamp}] {message}";
            
            txtTrainingLog.AppendText(logMessage + Environment.NewLine);
            txtTrainingLog.ScrollToCaret();
        }

        private void btnOpenResults_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtDatasetPath.Text) || !Directory.Exists(txtDatasetPath.Text))
            {
                MessageBox.Show("Önce geçerli bir veri seti seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Process.Start("explorer.exe", txtDatasetPath.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Klasör açılamadı: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSaveConfig_Click(object sender, EventArgs e)
        {
            using (var saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
                saveDialog.FileName = "training_config.json";
                
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var config = BuildTrainingConfig();
                        File.WriteAllText(saveDialog.FileName, JsonConvert.SerializeObject(_trainingConfig, Formatting.Indented));
                        MessageBox.Show("Konfigürasyon kaydedildi!", "Başarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Kaydetme hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnLoadConfig_Click(object sender, EventArgs e)
        {
            using (var openDialog = new OpenFileDialog())
            {
                openDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
                
                if (openDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var json = File.ReadAllText(openDialog.FileName);
                        var config = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
                        
                        // Konfigürasyonu form'a yükle
                        LoadConfigToForm(config);
                        MessageBox.Show("Konfigürasyon yüklendi!", "Başarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Yükleme hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void LoadConfigToForm(Dictionary<string, object> config)
        {
            if (config.ContainsKey("epochs")) nudEpochs.Value = Convert.ToDecimal(config["epochs"]);
            if (config.ContainsKey("batch_size")) nudBatchSize.Value = Convert.ToDecimal(config["batch_size"]);
            if (config.ContainsKey("img_size")) nudImageSize.Value = Convert.ToDecimal(config["img_size"]);
            if (config.ContainsKey("learning_rate")) nudLearningRate.Value = Convert.ToDecimal(config["learning_rate"]);
            if (config.ContainsKey("scale_min")) nudScaleMin.Value = Convert.ToDecimal(config["scale_min"]);
            if (config.ContainsKey("scale_max")) nudScaleMax.Value = Convert.ToDecimal(config["scale_max"]);
            if (config.ContainsKey("model_prefix")) txtModelPrefix.Text = config["model_prefix"].ToString();
            if (config.ContainsKey("enable_fine_tuning")) chkEnableFineTuning.Checked = Convert.ToBoolean(config["enable_fine_tuning"]);
            if (config.ContainsKey("enable_class_weights")) chkEnableClassWeights.Checked = Convert.ToBoolean(config["enable_class_weights"]);
            if (config.ContainsKey("enable_data_quality")) chkEnableDataQuality.Checked = Convert.ToBoolean(config["enable_data_quality"]);
            if (config.ContainsKey("enable_performance_metrics")) chkEnablePerformanceMetrics.Checked = Convert.ToBoolean(config["enable_performance_metrics"]);
            if (config.ContainsKey("enable_model_versioning")) chkEnableModelVersioning.Checked = Convert.ToBoolean(config["enable_model_versioning"]);
        }

        private void btnClearLog_Click(object sender, EventArgs e)
        {
            txtTrainingLog.Clear();
            AppendLog("[INFO] Loglar temizlendi");
        }

        private void btnSaveLog_Click(object sender, EventArgs e)
        {
            using (var saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                saveDialog.FileName = $"training_log_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
                
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        File.WriteAllText(saveDialog.FileName, txtTrainingLog.Text);
                        MessageBox.Show("Log dosyası kaydedildi!", "Başarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Kaydetme hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void cmbScript_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selectedTrainingScript = cmbScript.SelectedItem.ToString();
            AppendLog($"[INFO] Script seçildi: {_selectedTrainingScript}");
            // ResNet script seçildiyse ResNet variantını aktif et
            lblResNetVariant.Enabled = cmbResNetVariant.Enabled = (_selectedTrainingScript == "train_model_resnet.py");
        }

        private void cmbResNetVariant_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedVariant = cmbResNetVariant.SelectedItem.ToString();
            switch (selectedVariant)
            {
                case "ResNet50":
                    nudEpochs.Value = 50;
                    nudBatchSize.Value = 16;
                    nudLearningRate.Value = 0.0001m;
                    break;
                case "ResNet101":
                    nudEpochs.Value = 80;
                    nudBatchSize.Value = 12;
                    nudLearningRate.Value = 0.00005m;
                    break;
                case "ResNet152":
                    nudEpochs.Value = 100;
                    nudBatchSize.Value = 8;
                    nudLearningRate.Value = 0.00003m;
                    break;
            }
            txtModelPrefix.Text = $"{selectedVariant.ToLower()}_model";
            AppendLog($"[INFO] ResNet variant değiştirildi: {selectedVariant}");
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (_isTraining)
            {
                var result = MessageBox.Show("Eğitim devam ediyor. Çıkmak istediğinizden emin misiniz?", 
                    "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                
                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                    return;
                }
                
                StopTraining();
            }
            
            base.OnFormClosing(e);
        }
    }
} 