namespace QualityControl.UI
{
    partial class IndustrialTrainingForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            
            // Ana Tab Control
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabTraining = new System.Windows.Forms.TabPage();
            this.tabAdvanced = new System.Windows.Forms.TabPage();
            this.tabMonitoring = new System.Windows.Forms.TabPage();
            
            // Training Tab Controls
            this.grpDataset = new System.Windows.Forms.GroupBox();
            this.txtDatasetPath = new System.Windows.Forms.TextBox();
            this.btnBrowseDataset = new System.Windows.Forms.Button();
            this.lblDatasetStatus = new System.Windows.Forms.Label();
            this.txtClassInfo = new System.Windows.Forms.TextBox();
            this.pbDatasetStatus = new System.Windows.Forms.PictureBox();
            
            this.grpTrainingParams = new System.Windows.Forms.GroupBox();
            this.tableLayoutParams = new System.Windows.Forms.TableLayoutPanel();
            this.lblEpochs = new System.Windows.Forms.Label();
            this.nudEpochs = new System.Windows.Forms.NumericUpDown();
            this.lblBatchSize = new System.Windows.Forms.Label();
            this.nudBatchSize = new System.Windows.Forms.NumericUpDown();
            this.lblImageSize = new System.Windows.Forms.Label();
            this.nudImageSize = new System.Windows.Forms.NumericUpDown();
            this.lblLearningRate = new System.Windows.Forms.Label();
            this.nudLearningRate = new System.Windows.Forms.NumericUpDown();
            this.lblModelPrefix = new System.Windows.Forms.Label();
            this.txtModelPrefix = new System.Windows.Forms.TextBox();
            
            this.grpAugmentation = new System.Windows.Forms.GroupBox();
            this.lblScaleMin = new System.Windows.Forms.Label();
            this.nudScaleMin = new System.Windows.Forms.NumericUpDown();
            this.lblScaleMax = new System.Windows.Forms.Label();
            this.nudScaleMax = new System.Windows.Forms.NumericUpDown();
            
            this.grpTraining = new System.Windows.Forms.GroupBox();
            this.btnStartTraining = new System.Windows.Forms.Button();
            this.btnStopTraining = new System.Windows.Forms.Button();
            this.progressBarTraining = new System.Windows.Forms.ProgressBar();
            this.lblPythonPath = new System.Windows.Forms.Label();
            this.pbPythonStatus = new System.Windows.Forms.PictureBox();
            this.lblTrainingStatus = new System.Windows.Forms.Label();
            
            // Advanced Tab Controls
            this.grpModelOptions = new System.Windows.Forms.GroupBox();
            this.chkEnableFineTuning = new System.Windows.Forms.CheckBox();
            this.chkEnableClassWeights = new System.Windows.Forms.CheckBox();
            this.chkEnableDataQuality = new System.Windows.Forms.CheckBox();
            this.chkEnablePerformanceMetrics = new System.Windows.Forms.CheckBox();
            this.chkEnableModelVersioning = new System.Windows.Forms.CheckBox();
            
            this.grpOptimization = new System.Windows.Forms.GroupBox();
            this.lblOptimizer = new System.Windows.Forms.Label();
            this.cmbOptimizer = new System.Windows.Forms.ComboBox();
            this.lblLossFunction = new System.Windows.Forms.Label();
            this.cmbLossFunction = new System.Windows.Forms.ComboBox();
            this.lblEarlyStopping = new System.Windows.Forms.Label();
            this.cmbEarlyStopping = new System.Windows.Forms.ComboBox();
            this.lblReduceLR = new System.Windows.Forms.Label();
            this.cmbReduceLR = new System.Windows.Forms.ComboBox();
            
            this.grpConfig = new System.Windows.Forms.GroupBox();
            this.btnSaveConfig = new System.Windows.Forms.Button();
            this.btnLoadConfig = new System.Windows.Forms.Button();
            
            // Monitoring Tab Controls
            this.grpLogging = new System.Windows.Forms.GroupBox();
            this.txtTrainingLog = new System.Windows.Forms.TextBox();
            this.btnClearLog = new System.Windows.Forms.Button();
            this.btnSaveLog = new System.Windows.Forms.Button();
            this.lblLogCount = new System.Windows.Forms.Label();
            
            this.grpResults = new System.Windows.Forms.GroupBox();
            this.btnOpenResults = new System.Windows.Forms.Button();
            this.lblResultsInfo = new System.Windows.Forms.Label();
            this.pbResultsStatus = new System.Windows.Forms.PictureBox();
            
            // Status Bar
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.pbStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblTime = new System.Windows.Forms.ToolStripStatusLabel();
            
            this.SuspendLayout();
            
            // TabControl
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Font = new System.Drawing.Font("Segoe UI", 9F);
            
            // Training Tab
            this.tabTraining.Text = "🎯 Eğitim";
            this.tabTraining.Padding = new System.Windows.Forms.Padding(15);
            this.tabTraining.BackColor = System.Drawing.Color.FromArgb(248, 248, 248);
            
            // Dataset Group
            this.grpDataset.Text = "📁 Veri Seti";
            this.grpDataset.Location = new System.Drawing.Point(15, 15);
            this.grpDataset.Size = new System.Drawing.Size(580, 140);
            this.grpDataset.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.grpDataset.BackColor = System.Drawing.Color.White;
            
            this.txtDatasetPath.Location = new System.Drawing.Point(15, 30);
            this.txtDatasetPath.Size = new System.Drawing.Size(420, 25);
            this.txtDatasetPath.ReadOnly = true;
            this.txtDatasetPath.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtDatasetPath.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            
            this.btnBrowseDataset.Text = "📂 Gözat";
            this.btnBrowseDataset.Location = new System.Drawing.Point(445, 30);
            this.btnBrowseDataset.Size = new System.Drawing.Size(100, 25);
            this.btnBrowseDataset.BackColor = System.Drawing.Color.FromArgb(33, 150, 243);
            this.btnBrowseDataset.ForeColor = System.Drawing.Color.White;
            this.btnBrowseDataset.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnBrowseDataset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            
            this.lblDatasetStatus.Text = "❌ Veri seti: Seçilmedi";
            this.lblDatasetStatus.Location = new System.Drawing.Point(15, 65);
            this.lblDatasetStatus.Size = new System.Drawing.Size(200, 20);
            this.lblDatasetStatus.ForeColor = System.Drawing.Color.Red;
            this.lblDatasetStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
            
            this.pbDatasetStatus.Location = new System.Drawing.Point(220, 65);
            this.pbDatasetStatus.Size = new System.Drawing.Size(16, 16);
            this.pbDatasetStatus.BackColor = System.Drawing.Color.Red;
            
            this.txtClassInfo.Location = new System.Drawing.Point(15, 90);
            this.txtClassInfo.Size = new System.Drawing.Size(550, 40);
            this.txtClassInfo.Multiline = true;
            this.txtClassInfo.ReadOnly = true;
            this.txtClassInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtClassInfo.Font = new System.Drawing.Font("Consolas", 8F);
            this.txtClassInfo.BackColor = System.Drawing.Color.FromArgb(30, 30, 30);
            this.txtClassInfo.ForeColor = System.Drawing.Color.FromArgb(0, 255, 0);
            
            // Training Parameters Group
            this.grpTrainingParams.Text = "⚙️ Eğitim Parametreleri";
            this.grpTrainingParams.Location = new System.Drawing.Point(15, 165);
            this.grpTrainingParams.Size = new System.Drawing.Size(580, 180);
            this.grpTrainingParams.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.grpTrainingParams.BackColor = System.Drawing.Color.White;
            
            this.tableLayoutParams.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutParams.ColumnCount = 4;
            this.tableLayoutParams.RowCount = 4;
            this.tableLayoutParams.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutParams.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutParams.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutParams.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutParams.Padding = new System.Windows.Forms.Padding(10);
            
            this.lblEpochs.Text = "Epochs:";
            this.nudEpochs.Minimum = 1;
            this.nudEpochs.Maximum = 1000;
            this.nudEpochs.Value = 30;
            this.nudEpochs.Size = new System.Drawing.Size(100, 25);
            
            this.lblBatchSize.Text = "Batch Size:";
            this.nudBatchSize.Minimum = 1;
            this.nudBatchSize.Maximum = 128;
            this.nudBatchSize.Value = 16;
            this.nudBatchSize.Size = new System.Drawing.Size(100, 25);
            
            this.lblImageSize.Text = "Image Size:";
            this.nudImageSize.Minimum = 32;
            this.nudImageSize.Maximum = 512;
            this.nudImageSize.Value = 224;
            this.nudImageSize.Increment = 32;
            this.nudImageSize.Size = new System.Drawing.Size(100, 25);
            
            this.lblLearningRate.Text = "Learning Rate:";
            this.nudLearningRate.DecimalPlaces = 4;
            this.nudLearningRate.Minimum = 0.0001m;
            this.nudLearningRate.Maximum = 1.0m;
            this.nudLearningRate.Value = 0.0001m;
            this.nudLearningRate.Increment = 0.0001m;
            this.nudLearningRate.Size = new System.Drawing.Size(100, 25);
            
            this.lblModelPrefix.Text = "Model Prefix:";
            this.txtModelPrefix.Text = "industrial_model";
            this.txtModelPrefix.Size = new System.Drawing.Size(150, 25);
            
            // Augmentation Group
            this.grpAugmentation.Text = "🔄 Data Augmentation";
            this.grpAugmentation.Location = new System.Drawing.Point(15, 355);
            this.grpAugmentation.Size = new System.Drawing.Size(580, 100);
            this.grpAugmentation.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.grpAugmentation.BackColor = System.Drawing.Color.White;
            
            this.lblScaleMin.Text = "Scale Min:";
            this.lblScaleMin.Location = new System.Drawing.Point(15, 30);
            this.lblScaleMin.AutoSize = true;
            
            this.nudScaleMin.Location = new System.Drawing.Point(120, 28);
            this.nudScaleMin.Size = new System.Drawing.Size(100, 25);
            this.nudScaleMin.DecimalPlaces = 1;
            this.nudScaleMin.Minimum = 0.1m;
            this.nudScaleMin.Maximum = 2.0m;
            this.nudScaleMin.Value = 0.8m;
            this.nudScaleMin.Increment = 0.1m;
            
            this.lblScaleMax.Text = "Scale Max:";
            this.lblScaleMax.Location = new System.Drawing.Point(240, 30);
            this.lblScaleMax.AutoSize = true;
            
            this.nudScaleMax.Location = new System.Drawing.Point(345, 28);
            this.nudScaleMax.Size = new System.Drawing.Size(100, 25);
            this.nudScaleMax.DecimalPlaces = 1;
            this.nudScaleMax.Minimum = 0.1m;
            this.nudScaleMax.Maximum = 2.0m;
            this.nudScaleMax.Value = 1.2m;
            this.nudScaleMax.Increment = 0.1m;
            
            // Training Group
            this.grpTraining.Text = "🚀 Eğitim Kontrolü";
            this.grpTraining.Location = new System.Drawing.Point(15, 465);
            this.grpTraining.Size = new System.Drawing.Size(580, 120);
            this.grpTraining.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.grpTraining.BackColor = System.Drawing.Color.White;
            
            this.btnStartTraining.Text = "▶️ Eğitimi Başlat";
            this.btnStartTraining.Location = new System.Drawing.Point(15, 30);
            this.btnStartTraining.Size = new System.Drawing.Size(140, 40);
            this.btnStartTraining.BackColor = System.Drawing.Color.FromArgb(76, 175, 80);
            this.btnStartTraining.ForeColor = System.Drawing.Color.White;
            this.btnStartTraining.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnStartTraining.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            
            this.btnStopTraining.Text = "⏹️ Eğitimi Durdur";
            this.btnStopTraining.Location = new System.Drawing.Point(165, 30);
            this.btnStopTraining.Size = new System.Drawing.Size(140, 40);
            this.btnStopTraining.BackColor = System.Drawing.Color.FromArgb(244, 67, 54);
            this.btnStopTraining.ForeColor = System.Drawing.Color.White;
            this.btnStopTraining.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnStopTraining.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStopTraining.Enabled = false;
            
            this.progressBarTraining.Location = new System.Drawing.Point(15, 80);
            this.progressBarTraining.Size = new System.Drawing.Size(550, 30);
            this.progressBarTraining.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            
            this.lblPythonPath.Text = "🐍 Python: Kontrol ediliyor...";
            this.lblPythonPath.Location = new System.Drawing.Point(320, 40);
            this.lblPythonPath.Size = new System.Drawing.Size(200, 20);
            this.lblPythonPath.Font = new System.Drawing.Font("Segoe UI", 9F);
            
            this.pbPythonStatus.Location = new System.Drawing.Point(300, 40);
            this.pbPythonStatus.Size = new System.Drawing.Size(16, 16);
            this.pbPythonStatus.BackColor = System.Drawing.Color.Yellow;
            
            this.lblTrainingStatus.Text = "⏸️ Eğitim Bekliyor";
            this.lblTrainingStatus.Location = new System.Drawing.Point(15, 115);
            this.lblTrainingStatus.Size = new System.Drawing.Size(200, 20);
            this.lblTrainingStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTrainingStatus.ForeColor = System.Drawing.Color.Gray;
            
            // Advanced Tab
            this.tabAdvanced.Text = "🔧 Gelişmiş";
            this.tabAdvanced.Padding = new System.Windows.Forms.Padding(15);
            this.tabAdvanced.BackColor = System.Drawing.Color.FromArgb(248, 248, 248);
            
            // Model Options Group
            this.grpModelOptions.Text = "🎛️ Model Seçenekleri";
            this.grpModelOptions.Location = new System.Drawing.Point(15, 15);
            this.grpModelOptions.Size = new System.Drawing.Size(580, 180);
            this.grpModelOptions.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.grpModelOptions.BackColor = System.Drawing.Color.White;
            
            this.chkEnableFineTuning.Text = "🔧 Fine-tuning Etkinleştir";
            this.chkEnableFineTuning.Location = new System.Drawing.Point(15, 30);
            this.chkEnableFineTuning.Size = new System.Drawing.Size(250, 25);
            this.chkEnableFineTuning.Checked = true;
            this.chkEnableFineTuning.Font = new System.Drawing.Font("Segoe UI", 9F);
            
            this.chkEnableClassWeights.Text = "⚖️ Class Weights Etkinleştir";
            this.chkEnableClassWeights.Location = new System.Drawing.Point(15, 60);
            this.chkEnableClassWeights.Size = new System.Drawing.Size(250, 25);
            this.chkEnableClassWeights.Checked = true;
            this.chkEnableClassWeights.Font = new System.Drawing.Font("Segoe UI", 9F);
            
            this.chkEnableDataQuality.Text = "🔍 Veri Kalitesi Kontrolü";
            this.chkEnableDataQuality.Location = new System.Drawing.Point(15, 90);
            this.chkEnableDataQuality.Size = new System.Drawing.Size(250, 25);
            this.chkEnableDataQuality.Checked = true;
            this.chkEnableDataQuality.Font = new System.Drawing.Font("Segoe UI", 9F);
            
            this.chkEnablePerformanceMetrics.Text = "📊 Performans Metrikleri";
            this.chkEnablePerformanceMetrics.Location = new System.Drawing.Point(15, 120);
            this.chkEnablePerformanceMetrics.Size = new System.Drawing.Size(250, 25);
            this.chkEnablePerformanceMetrics.Checked = true;
            this.chkEnablePerformanceMetrics.Font = new System.Drawing.Font("Segoe UI", 9F);
            
            this.chkEnableModelVersioning.Text = "📦 Model Versiyonlama";
            this.chkEnableModelVersioning.Location = new System.Drawing.Point(15, 150);
            this.chkEnableModelVersioning.Size = new System.Drawing.Size(250, 25);
            this.chkEnableModelVersioning.Checked = true;
            this.chkEnableModelVersioning.Font = new System.Drawing.Font("Segoe UI", 9F);
            
            // Optimization Group
            this.grpOptimization.Text = "⚡ Optimizasyon";
            this.grpOptimization.Location = new System.Drawing.Point(15, 205);
            this.grpOptimization.Size = new System.Drawing.Size(580, 200);
            this.grpOptimization.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.grpOptimization.BackColor = System.Drawing.Color.White;
            
            this.lblOptimizer.Text = "Optimizer:";
            this.lblOptimizer.Location = new System.Drawing.Point(15, 30);
            this.lblOptimizer.AutoSize = true;
            this.lblOptimizer.Font = new System.Drawing.Font("Segoe UI", 9F);
            
            this.cmbOptimizer.Location = new System.Drawing.Point(120, 28);
            this.cmbOptimizer.Size = new System.Drawing.Size(150, 25);
            this.cmbOptimizer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOptimizer.Items.AddRange(new object[] { "Adam", "SGD", "RMSprop", "AdamW" });
            this.cmbOptimizer.SelectedIndex = 0;
            this.cmbOptimizer.Font = new System.Drawing.Font("Segoe UI", 9F);
            
            this.lblLossFunction.Text = "Loss Function:";
            this.lblLossFunction.Location = new System.Drawing.Point(15, 65);
            this.lblLossFunction.AutoSize = true;
            this.lblLossFunction.Font = new System.Drawing.Font("Segoe UI", 9F);
            
            this.cmbLossFunction.Location = new System.Drawing.Point(120, 63);
            this.cmbLossFunction.Size = new System.Drawing.Size(200, 25);
            this.cmbLossFunction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLossFunction.Items.AddRange(new object[] { "Categorical Crossentropy", "Sparse Categorical Crossentropy", "Binary Crossentropy" });
            this.cmbLossFunction.SelectedIndex = 0;
            this.cmbLossFunction.Font = new System.Drawing.Font("Segoe UI", 9F);
            
            this.lblEarlyStopping.Text = "Early Stopping:";
            this.lblEarlyStopping.Location = new System.Drawing.Point(15, 100);
            this.lblEarlyStopping.AutoSize = true;
            this.lblEarlyStopping.Font = new System.Drawing.Font("Segoe UI", 9F);
            
            this.cmbEarlyStopping.Location = new System.Drawing.Point(120, 98);
            this.cmbEarlyStopping.Size = new System.Drawing.Size(100, 25);
            this.cmbEarlyStopping.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEarlyStopping.Items.AddRange(new object[] { "5", "8", "10", "15", "20" });
            this.cmbEarlyStopping.SelectedIndex = 1;
            this.cmbEarlyStopping.Font = new System.Drawing.Font("Segoe UI", 9F);
            
            this.lblReduceLR.Text = "Reduce LR:";
            this.lblReduceLR.Location = new System.Drawing.Point(15, 135);
            this.lblReduceLR.AutoSize = true;
            this.lblReduceLR.Font = new System.Drawing.Font("Segoe UI", 9F);
            
            this.cmbReduceLR.Location = new System.Drawing.Point(120, 133);
            this.cmbReduceLR.Size = new System.Drawing.Size(100, 25);
            this.cmbReduceLR.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbReduceLR.Items.AddRange(new object[] { "2", "3", "5", "8" });
            this.cmbReduceLR.SelectedIndex = 1;
            this.cmbReduceLR.Font = new System.Drawing.Font("Segoe UI", 9F);
            
            // Config Group
            this.grpConfig.Text = "💾 Konfigürasyon";
            this.grpConfig.Location = new System.Drawing.Point(15, 415);
            this.grpConfig.Size = new System.Drawing.Size(580, 100);
            this.grpConfig.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.grpConfig.BackColor = System.Drawing.Color.White;
            
            this.btnSaveConfig.Text = "💾 Konfigürasyonu Kaydet";
            this.btnSaveConfig.Location = new System.Drawing.Point(15, 30);
            this.btnSaveConfig.Size = new System.Drawing.Size(180, 35);
            this.btnSaveConfig.BackColor = System.Drawing.Color.FromArgb(33, 150, 243);
            this.btnSaveConfig.ForeColor = System.Drawing.Color.White;
            this.btnSaveConfig.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSaveConfig.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            
            this.btnLoadConfig.Text = "📂 Konfigürasyonu Yükle";
            this.btnLoadConfig.Location = new System.Drawing.Point(205, 30);
            this.btnLoadConfig.Size = new System.Drawing.Size(180, 35);
            this.btnLoadConfig.BackColor = System.Drawing.Color.FromArgb(156, 39, 176);
            this.btnLoadConfig.ForeColor = System.Drawing.Color.White;
            this.btnLoadConfig.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnLoadConfig.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            
            // Monitoring Tab
            this.tabMonitoring.Text = "📊 İzleme";
            this.tabMonitoring.Padding = new System.Windows.Forms.Padding(15);
            this.tabMonitoring.BackColor = System.Drawing.Color.FromArgb(248, 248, 248);
            
            // Logging Group
            this.grpLogging.Text = "📋 Eğitim Logları";
            this.grpLogging.Location = new System.Drawing.Point(15, 15);
            this.grpLogging.Size = new System.Drawing.Size(580, 450);
            this.grpLogging.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.grpLogging.BackColor = System.Drawing.Color.White;
            
            this.txtTrainingLog.Location = new System.Drawing.Point(15, 30);
            this.txtTrainingLog.Size = new System.Drawing.Size(550, 350);
            this.txtTrainingLog.Multiline = true;
            this.txtTrainingLog.ReadOnly = true;
            this.txtTrainingLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtTrainingLog.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtTrainingLog.BackColor = System.Drawing.Color.FromArgb(30, 30, 30);
            this.txtTrainingLog.ForeColor = System.Drawing.Color.FromArgb(0, 255, 0);
            
            this.btnClearLog.Text = "🗑️ Logları Temizle";
            this.btnClearLog.Location = new System.Drawing.Point(15, 390);
            this.btnClearLog.Size = new System.Drawing.Size(120, 35);
            this.btnClearLog.BackColor = System.Drawing.Color.FromArgb(158, 158, 158);
            this.btnClearLog.ForeColor = System.Drawing.Color.White;
            this.btnClearLog.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnClearLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            
            this.btnSaveLog.Text = "💾 Logları Kaydet";
            this.btnSaveLog.Location = new System.Drawing.Point(145, 390);
            this.btnSaveLog.Size = new System.Drawing.Size(120, 35);
            this.btnSaveLog.BackColor = System.Drawing.Color.FromArgb(76, 175, 80);
            this.btnSaveLog.ForeColor = System.Drawing.Color.White;
            this.btnSaveLog.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSaveLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            
            this.lblLogCount.Text = "📊 Log Sayısı: 0";
            this.lblLogCount.Location = new System.Drawing.Point(280, 400);
            this.lblLogCount.AutoSize = true;
            this.lblLogCount.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblLogCount.ForeColor = System.Drawing.Color.Gray;
            
            // Results Group
            this.grpResults.Text = "📁 Sonuçlar";
            this.grpResults.Location = new System.Drawing.Point(15, 475);
            this.grpResults.Size = new System.Drawing.Size(580, 100);
            this.grpResults.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.grpResults.BackColor = System.Drawing.Color.White;
            
            this.btnOpenResults.Text = "📂 Sonuç Klasörünü Aç";
            this.btnOpenResults.Location = new System.Drawing.Point(15, 30);
            this.btnOpenResults.Size = new System.Drawing.Size(180, 35);
            this.btnOpenResults.BackColor = System.Drawing.Color.FromArgb(255, 152, 0);
            this.btnOpenResults.ForeColor = System.Drawing.Color.White;
            this.btnOpenResults.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnOpenResults.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            
            this.lblResultsInfo.Text = "📊 Eğitim tamamlandıktan sonra sonuçlar burada görüntülenecek.";
            this.lblResultsInfo.Location = new System.Drawing.Point(210, 40);
            this.lblResultsInfo.Size = new System.Drawing.Size(350, 20);
            this.lblResultsInfo.ForeColor = System.Drawing.Color.Gray;
            this.lblResultsInfo.Font = new System.Drawing.Font("Segoe UI", 9F);
            
            this.pbResultsStatus.Location = new System.Drawing.Point(190, 40);
            this.pbResultsStatus.Size = new System.Drawing.Size(16, 16);
            this.pbResultsStatus.BackColor = System.Drawing.Color.Gray;
            
            // Status Bar
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.lblStatus,
                this.pbStatus,
                this.lblTime
            });
            
            this.lblStatus.Text = "Sistem Hazır";
            this.pbStatus.Text = "●";
            this.pbStatus.ForeColor = System.Drawing.Color.Green;
            this.lblTime.Text = DateTime.Now.ToString("HH:mm:ss");
            
            // Form
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 800);
            this.MinimumSize = new System.Drawing.Size(1000, 700);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "🏭 Endüstriyel Kalite Kontrol - Gelişmiş Eğitim Modülü";
            this.Icon = System.Drawing.SystemIcons.Application;
            
            // Controls
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.statusStrip);
            
            this.tabControl.Controls.Add(this.tabTraining);
            this.tabControl.Controls.Add(this.tabAdvanced);
            this.tabControl.Controls.Add(this.tabMonitoring);
            
            // Training Tab Controls
            this.tabTraining.Controls.Add(this.grpTraining);
            this.tabTraining.Controls.Add(this.grpAugmentation);
            this.tabTraining.Controls.Add(this.grpTrainingParams);
            this.tabTraining.Controls.Add(this.grpDataset);
            
            this.grpDataset.Controls.Add(this.txtDatasetPath);
            this.grpDataset.Controls.Add(this.btnBrowseDataset);
            this.grpDataset.Controls.Add(this.lblDatasetStatus);
            this.grpDataset.Controls.Add(this.pbDatasetStatus);
            this.grpDataset.Controls.Add(this.txtClassInfo);
            
            this.grpTrainingParams.Controls.Add(this.tableLayoutParams);
            this.tableLayoutParams.Controls.Add(this.lblEpochs, 0, 0);
            this.tableLayoutParams.Controls.Add(this.nudEpochs, 1, 0);
            this.tableLayoutParams.Controls.Add(this.lblBatchSize, 2, 0);
            this.tableLayoutParams.Controls.Add(this.nudBatchSize, 3, 0);
            this.tableLayoutParams.Controls.Add(this.lblImageSize, 0, 1);
            this.tableLayoutParams.Controls.Add(this.nudImageSize, 1, 1);
            this.tableLayoutParams.Controls.Add(this.lblLearningRate, 2, 1);
            this.tableLayoutParams.Controls.Add(this.nudLearningRate, 3, 1);
            this.tableLayoutParams.Controls.Add(this.lblModelPrefix, 0, 2);
            this.tableLayoutParams.Controls.Add(this.txtModelPrefix, 1, 2);
            
            this.grpAugmentation.Controls.Add(this.lblScaleMin);
            this.grpAugmentation.Controls.Add(this.nudScaleMin);
            this.grpAugmentation.Controls.Add(this.lblScaleMax);
            this.grpAugmentation.Controls.Add(this.nudScaleMax);
            
            this.grpTraining.Controls.Add(this.btnStartTraining);
            this.grpTraining.Controls.Add(this.btnStopTraining);
            this.grpTraining.Controls.Add(this.progressBarTraining);
            this.grpTraining.Controls.Add(this.lblPythonPath);
            this.grpTraining.Controls.Add(this.pbPythonStatus);
            this.grpTraining.Controls.Add(this.lblTrainingStatus);
            
            // Advanced Tab Controls
            this.tabAdvanced.Controls.Add(this.grpConfig);
            this.tabAdvanced.Controls.Add(this.grpOptimization);
            this.tabAdvanced.Controls.Add(this.grpModelOptions);
            
            this.grpModelOptions.Controls.Add(this.chkEnableFineTuning);
            this.grpModelOptions.Controls.Add(this.chkEnableClassWeights);
            this.grpModelOptions.Controls.Add(this.chkEnableDataQuality);
            this.grpModelOptions.Controls.Add(this.chkEnablePerformanceMetrics);
            this.grpModelOptions.Controls.Add(this.chkEnableModelVersioning);
            
            this.grpOptimization.Controls.Add(this.lblOptimizer);
            this.grpOptimization.Controls.Add(this.cmbOptimizer);
            this.grpOptimization.Controls.Add(this.lblLossFunction);
            this.grpOptimization.Controls.Add(this.cmbLossFunction);
            this.grpOptimization.Controls.Add(this.lblEarlyStopping);
            this.grpOptimization.Controls.Add(this.cmbEarlyStopping);
            this.grpOptimization.Controls.Add(this.lblReduceLR);
            this.grpOptimization.Controls.Add(this.cmbReduceLR);
            
            this.grpConfig.Controls.Add(this.btnSaveConfig);
            this.grpConfig.Controls.Add(this.btnLoadConfig);
            
            // Monitoring Tab Controls
            this.tabMonitoring.Controls.Add(this.grpResults);
            this.tabMonitoring.Controls.Add(this.grpLogging);
            
            this.grpLogging.Controls.Add(this.txtTrainingLog);
            this.grpLogging.Controls.Add(this.btnClearLog);
            this.grpLogging.Controls.Add(this.btnSaveLog);
            this.grpLogging.Controls.Add(this.lblLogCount);
            
            this.grpResults.Controls.Add(this.btnOpenResults);
            this.grpResults.Controls.Add(this.lblResultsInfo);
            this.grpResults.Controls.Add(this.pbResultsStatus);
            
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #region Windows Form Designer generated code
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabTraining;
        private System.Windows.Forms.TabPage tabAdvanced;
        private System.Windows.Forms.TabPage tabMonitoring;
        
        // Training Tab Controls
        private System.Windows.Forms.GroupBox grpDataset;
        private System.Windows.Forms.TextBox txtDatasetPath;
        private System.Windows.Forms.Button btnBrowseDataset;
        private System.Windows.Forms.Label lblDatasetStatus;
        private System.Windows.Forms.TextBox txtClassInfo;
        private System.Windows.Forms.PictureBox pbDatasetStatus;
        
        private System.Windows.Forms.GroupBox grpTrainingParams;
        private System.Windows.Forms.TableLayoutPanel tableLayoutParams;
        private System.Windows.Forms.Label lblEpochs;
        private System.Windows.Forms.NumericUpDown nudEpochs;
        private System.Windows.Forms.Label lblBatchSize;
        private System.Windows.Forms.NumericUpDown nudBatchSize;
        private System.Windows.Forms.Label lblImageSize;
        private System.Windows.Forms.NumericUpDown nudImageSize;
        private System.Windows.Forms.Label lblLearningRate;
        private System.Windows.Forms.NumericUpDown nudLearningRate;
        private System.Windows.Forms.Label lblModelPrefix;
        private System.Windows.Forms.TextBox txtModelPrefix;
        
        private System.Windows.Forms.GroupBox grpAugmentation;
        private System.Windows.Forms.Label lblScaleMin;
        private System.Windows.Forms.NumericUpDown nudScaleMin;
        private System.Windows.Forms.Label lblScaleMax;
        private System.Windows.Forms.NumericUpDown nudScaleMax;
        
        private System.Windows.Forms.GroupBox grpTraining;
        private System.Windows.Forms.Button btnStartTraining;
        private System.Windows.Forms.Button btnStopTraining;
        private System.Windows.Forms.ProgressBar progressBarTraining;
        private System.Windows.Forms.Label lblPythonPath;
        private System.Windows.Forms.PictureBox pbPythonStatus;
        private System.Windows.Forms.Label lblTrainingStatus;
        
        // Advanced Tab Controls
        private System.Windows.Forms.GroupBox grpModelOptions;
        private System.Windows.Forms.CheckBox chkEnableFineTuning;
        private System.Windows.Forms.CheckBox chkEnableClassWeights;
        private System.Windows.Forms.CheckBox chkEnableDataQuality;
        private System.Windows.Forms.CheckBox chkEnablePerformanceMetrics;
        private System.Windows.Forms.CheckBox chkEnableModelVersioning;
        
        private System.Windows.Forms.GroupBox grpOptimization;
        private System.Windows.Forms.Label lblOptimizer;
        private System.Windows.Forms.ComboBox cmbOptimizer;
        private System.Windows.Forms.Label lblLossFunction;
        private System.Windows.Forms.ComboBox cmbLossFunction;
        private System.Windows.Forms.Label lblEarlyStopping;
        private System.Windows.Forms.ComboBox cmbEarlyStopping;
        private System.Windows.Forms.Label lblReduceLR;
        private System.Windows.Forms.ComboBox cmbReduceLR;
        
        private System.Windows.Forms.GroupBox grpConfig;
        private System.Windows.Forms.Button btnSaveConfig;
        private System.Windows.Forms.Button btnLoadConfig;
        
        // Monitoring Tab Controls
        private System.Windows.Forms.GroupBox grpLogging;
        private System.Windows.Forms.TextBox txtTrainingLog;
        private System.Windows.Forms.Button btnClearLog;
        private System.Windows.Forms.Button btnSaveLog;
        private System.Windows.Forms.Label lblLogCount;
        
        private System.Windows.Forms.GroupBox grpResults;
        private System.Windows.Forms.Button btnOpenResults;
        private System.Windows.Forms.Label lblResultsInfo;
        private System.Windows.Forms.PictureBox pbResultsStatus;
        
        // Status Bar
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.ToolStripStatusLabel pbStatus;
        private System.Windows.Forms.ToolStripStatusLabel lblTime;
        #endregion
    }
} 