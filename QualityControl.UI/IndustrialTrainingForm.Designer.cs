using System;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IndustrialTrainingForm));
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabTraining = new System.Windows.Forms.TabPage();
            this.grpTraining = new System.Windows.Forms.GroupBox();
            this.btnStartTraining = new System.Windows.Forms.Button();
            this.btnStopTraining = new System.Windows.Forms.Button();
            this.progressBarTraining = new System.Windows.Forms.ProgressBar();
            this.lblPythonPath = new System.Windows.Forms.Label();
            this.pbPythonStatus = new System.Windows.Forms.PictureBox();
            this.lblTrainingStatus = new System.Windows.Forms.Label();
            this.grpAugmentation = new System.Windows.Forms.GroupBox();
            this.lblScaleMin = new System.Windows.Forms.Label();
            this.nudScaleMin = new System.Windows.Forms.NumericUpDown();
            this.lblScaleMax = new System.Windows.Forms.Label();
            this.nudScaleMax = new System.Windows.Forms.NumericUpDown();
            this.grpTrainingParams = new System.Windows.Forms.GroupBox();
            this.tableLayoutParams = new System.Windows.Forms.TableLayoutPanel();
            this.lblEpochs = new System.Windows.Forms.Label();
            this.lblBatchSize = new System.Windows.Forms.Label();
            this.nudBatchSize = new System.Windows.Forms.NumericUpDown();
            this.lblImageSize = new System.Windows.Forms.Label();
            this.nudImageSize = new System.Windows.Forms.NumericUpDown();
            this.lblLearningRate = new System.Windows.Forms.Label();
            this.nudLearningRate = new System.Windows.Forms.NumericUpDown();
            this.lblModelPrefix = new System.Windows.Forms.Label();
            this.nudEpochs = new System.Windows.Forms.NumericUpDown();
            this.txtModelPrefix = new System.Windows.Forms.TextBox();
            this.lblScript = new System.Windows.Forms.Label();
            this.cmbScript = new System.Windows.Forms.ComboBox();
            this.lblResNetVariant = new System.Windows.Forms.Label();
            this.cmbResNetVariant = new System.Windows.Forms.ComboBox();
            this.grpDataset = new System.Windows.Forms.GroupBox();
            this.txtDatasetPath = new System.Windows.Forms.TextBox();
            this.btnBrowseDataset = new System.Windows.Forms.Button();
            this.lblDatasetStatus = new System.Windows.Forms.Label();
            this.pbDatasetStatus = new System.Windows.Forms.PictureBox();
            this.txtClassInfo = new System.Windows.Forms.TextBox();
            this.tabAdvanced = new System.Windows.Forms.TabPage();
            this.grpConfig = new System.Windows.Forms.GroupBox();
            this.btnSaveConfig = new System.Windows.Forms.Button();
            this.btnLoadConfig = new System.Windows.Forms.Button();
            this.grpOptimization = new System.Windows.Forms.GroupBox();
            this.lblOptimizer = new System.Windows.Forms.Label();
            this.cmbOptimizer = new System.Windows.Forms.ComboBox();
            this.lblLossFunction = new System.Windows.Forms.Label();
            this.cmbLossFunction = new System.Windows.Forms.ComboBox();
            this.lblEarlyStopping = new System.Windows.Forms.Label();
            this.cmbEarlyStopping = new System.Windows.Forms.ComboBox();
            this.lblReduceLR = new System.Windows.Forms.Label();
            this.cmbReduceLR = new System.Windows.Forms.ComboBox();
            this.grpModelOptions = new System.Windows.Forms.GroupBox();
            this.chkEnableFineTuning = new System.Windows.Forms.CheckBox();
            this.chkEnableClassWeights = new System.Windows.Forms.CheckBox();
            this.chkEnableDataQuality = new System.Windows.Forms.CheckBox();
            this.chkEnablePerformanceMetrics = new System.Windows.Forms.CheckBox();
            this.chkEnableModelVersioning = new System.Windows.Forms.CheckBox();
            this.tabMonitoring = new System.Windows.Forms.TabPage();
            this.grpResults = new System.Windows.Forms.GroupBox();
            this.btnOpenResults = new System.Windows.Forms.Button();
            this.lblResultsInfo = new System.Windows.Forms.Label();
            this.pbResultsStatus = new System.Windows.Forms.PictureBox();
            this.grpLogging = new System.Windows.Forms.GroupBox();
            this.txtTrainingLog = new System.Windows.Forms.TextBox();
            this.btnClearLog = new System.Windows.Forms.Button();
            this.btnSaveLog = new System.Windows.Forms.Button();
            this.lblLogCount = new System.Windows.Forms.Label();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.pbStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabControl.SuspendLayout();
            this.tabTraining.SuspendLayout();
            this.grpTraining.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPythonStatus)).BeginInit();
            this.grpAugmentation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudScaleMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudScaleMax)).BeginInit();
            this.grpTrainingParams.SuspendLayout();
            this.tableLayoutParams.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBatchSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudImageSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLearningRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEpochs)).BeginInit();
            this.grpDataset.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbDatasetStatus)).BeginInit();
            this.tabAdvanced.SuspendLayout();
            this.grpConfig.SuspendLayout();
            this.grpOptimization.SuspendLayout();
            this.grpModelOptions.SuspendLayout();
            this.tabMonitoring.SuspendLayout();
            this.grpResults.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbResultsStatus)).BeginInit();
            this.grpLogging.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabTraining);
            this.tabControl.Controls.Add(this.tabAdvanced);
            this.tabControl.Controls.Add(this.tabMonitoring);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1029, 671);
            this.tabControl.TabIndex = 0;
            // 
            // tabTraining
            // 
            this.tabTraining.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this.tabTraining.Controls.Add(this.grpTraining);
            this.tabTraining.Controls.Add(this.grpAugmentation);
            this.tabTraining.Controls.Add(this.grpTrainingParams);
            this.tabTraining.Controls.Add(this.grpDataset);
            this.tabTraining.Location = new System.Drawing.Point(4, 24);
            this.tabTraining.Name = "tabTraining";
            this.tabTraining.Padding = new System.Windows.Forms.Padding(13);
            this.tabTraining.Size = new System.Drawing.Size(1021, 643);
            this.tabTraining.TabIndex = 0;
            this.tabTraining.Text = "🎯 Eğitim";
            // 
            // grpTraining
            // 
            this.grpTraining.BackColor = System.Drawing.Color.White;
            this.grpTraining.Controls.Add(this.btnStartTraining);
            this.grpTraining.Controls.Add(this.btnStopTraining);
            this.grpTraining.Controls.Add(this.progressBarTraining);
            this.grpTraining.Controls.Add(this.lblPythonPath);
            this.grpTraining.Controls.Add(this.pbPythonStatus);
            this.grpTraining.Controls.Add(this.lblTrainingStatus);
            this.grpTraining.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.grpTraining.Location = new System.Drawing.Point(13, 403);
            this.grpTraining.Name = "grpTraining";
            this.grpTraining.Size = new System.Drawing.Size(1005, 237);
            this.grpTraining.TabIndex = 0;
            this.grpTraining.TabStop = false;
            this.grpTraining.Text = "🚀 Eğitim Kontrolü";
            // 
            // btnStartTraining
            // 
            this.btnStartTraining.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnStartTraining.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStartTraining.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnStartTraining.ForeColor = System.Drawing.Color.White;
            this.btnStartTraining.Location = new System.Drawing.Point(13, 26);
            this.btnStartTraining.Name = "btnStartTraining";
            this.btnStartTraining.Size = new System.Drawing.Size(120, 35);
            this.btnStartTraining.TabIndex = 0;
            this.btnStartTraining.Text = "▶️ Eğitimi Başlat";
            this.btnStartTraining.UseVisualStyleBackColor = false;
            // 
            // btnStopTraining
            // 
            this.btnStopTraining.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.btnStopTraining.Enabled = false;
            this.btnStopTraining.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStopTraining.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnStopTraining.ForeColor = System.Drawing.Color.White;
            this.btnStopTraining.Location = new System.Drawing.Point(141, 26);
            this.btnStopTraining.Name = "btnStopTraining";
            this.btnStopTraining.Size = new System.Drawing.Size(120, 35);
            this.btnStopTraining.TabIndex = 1;
            this.btnStopTraining.Text = "⏹️ Eğitimi Durdur";
            this.btnStopTraining.UseVisualStyleBackColor = false;
            // 
            // progressBarTraining
            // 
            this.progressBarTraining.Location = new System.Drawing.Point(3, 67);
            this.progressBarTraining.Name = "progressBarTraining";
            this.progressBarTraining.Size = new System.Drawing.Size(471, 26);
            this.progressBarTraining.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBarTraining.TabIndex = 2;
            // 
            // lblPythonPath
            // 
            this.lblPythonPath.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblPythonPath.Location = new System.Drawing.Point(296, 37);
            this.lblPythonPath.Name = "lblPythonPath";
            this.lblPythonPath.Size = new System.Drawing.Size(171, 17);
            this.lblPythonPath.TabIndex = 3;
            this.lblPythonPath.Text = "🐍 Python: Kontrol ediliyor...";
            // 
            // pbPythonStatus
            // 
            this.pbPythonStatus.BackColor = System.Drawing.Color.Yellow;
            this.pbPythonStatus.Location = new System.Drawing.Point(276, 37);
            this.pbPythonStatus.Name = "pbPythonStatus";
            this.pbPythonStatus.Size = new System.Drawing.Size(14, 14);
            this.pbPythonStatus.TabIndex = 4;
            this.pbPythonStatus.TabStop = false;
            // 
            // lblTrainingStatus
            // 
            this.lblTrainingStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTrainingStatus.ForeColor = System.Drawing.Color.Gray;
            this.lblTrainingStatus.Location = new System.Drawing.Point(13, 100);
            this.lblTrainingStatus.Name = "lblTrainingStatus";
            this.lblTrainingStatus.Size = new System.Drawing.Size(171, 17);
            this.lblTrainingStatus.TabIndex = 5;
            this.lblTrainingStatus.Text = "⏸️ Eğitim Bekliyor";
            // 
            // grpAugmentation
            // 
            this.grpAugmentation.BackColor = System.Drawing.Color.White;
            this.grpAugmentation.Controls.Add(this.lblScaleMin);
            this.grpAugmentation.Controls.Add(this.nudScaleMin);
            this.grpAugmentation.Controls.Add(this.lblScaleMax);
            this.grpAugmentation.Controls.Add(this.nudScaleMax);
            this.grpAugmentation.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.grpAugmentation.Location = new System.Drawing.Point(13, 308);
            this.grpAugmentation.Name = "grpAugmentation";
            this.grpAugmentation.Size = new System.Drawing.Size(1005, 220);
            this.grpAugmentation.TabIndex = 1;
            this.grpAugmentation.TabStop = false;
            this.grpAugmentation.Text = "🔄 Data Augmentation";
            // 
            // lblScaleMin
            // 
            this.lblScaleMin.AutoSize = true;
            this.lblScaleMin.Location = new System.Drawing.Point(13, 26);
            this.lblScaleMin.Name = "lblScaleMin";
            this.lblScaleMin.Size = new System.Drawing.Size(63, 15);
            this.lblScaleMin.TabIndex = 0;
            this.lblScaleMin.Text = "Scale Min:";
            // 
            // nudScaleMin
            // 
            this.nudScaleMin.DecimalPlaces = 1;
            this.nudScaleMin.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudScaleMin.Location = new System.Drawing.Point(103, 24);
            this.nudScaleMin.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            65536});
            this.nudScaleMin.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudScaleMin.Name = "nudScaleMin";
            this.nudScaleMin.Size = new System.Drawing.Size(86, 23);
            this.nudScaleMin.TabIndex = 1;
            this.nudScaleMin.Value = new decimal(new int[] {
            8,
            0,
            0,
            65536});
            // 
            // lblScaleMax
            // 
            this.lblScaleMax.AutoSize = true;
            this.lblScaleMax.Location = new System.Drawing.Point(206, 26);
            this.lblScaleMax.Name = "lblScaleMax";
            this.lblScaleMax.Size = new System.Drawing.Size(66, 15);
            this.lblScaleMax.TabIndex = 2;
            this.lblScaleMax.Text = "Scale Max:";
            // 
            // nudScaleMax
            // 
            this.nudScaleMax.DecimalPlaces = 1;
            this.nudScaleMax.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudScaleMax.Location = new System.Drawing.Point(296, 24);
            this.nudScaleMax.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            65536});
            this.nudScaleMax.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudScaleMax.Name = "nudScaleMax";
            this.nudScaleMax.Size = new System.Drawing.Size(86, 23);
            this.nudScaleMax.TabIndex = 3;
            this.nudScaleMax.Value = new decimal(new int[] {
            12,
            0,
            0,
            65536});
            // 
            // grpTrainingParams
            // 
            this.grpTrainingParams.BackColor = System.Drawing.Color.White;
            this.grpTrainingParams.Controls.Add(this.tableLayoutParams);
            this.grpTrainingParams.Controls.Add(this.lblScript);
            this.grpTrainingParams.Controls.Add(this.cmbScript);
            this.grpTrainingParams.Controls.Add(this.lblResNetVariant);
            this.grpTrainingParams.Controls.Add(this.cmbResNetVariant);
            this.grpTrainingParams.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.grpTrainingParams.Location = new System.Drawing.Point(13, 143);
            this.grpTrainingParams.Name = "grpTrainingParams";
            this.grpTrainingParams.Size = new System.Drawing.Size(1005, 289);
            this.grpTrainingParams.TabIndex = 2;
            this.grpTrainingParams.TabStop = false;
            this.grpTrainingParams.Text = "⚙️ Eğitim Parametreleri";
            // 
            // tableLayoutParams
            // 
            this.tableLayoutParams.ColumnCount = 4;
            this.tableLayoutParams.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutParams.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutParams.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutParams.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutParams.Controls.Add(this.lblEpochs, 0, 0);
            this.tableLayoutParams.Controls.Add(this.lblBatchSize, 2, 0);
            this.tableLayoutParams.Controls.Add(this.nudBatchSize, 3, 0);
            this.tableLayoutParams.Controls.Add(this.lblImageSize, 0, 1);
            this.tableLayoutParams.Controls.Add(this.nudImageSize, 1, 1);
            this.tableLayoutParams.Controls.Add(this.lblLearningRate, 2, 1);
            this.tableLayoutParams.Controls.Add(this.nudLearningRate, 3, 1);
            this.tableLayoutParams.Controls.Add(this.lblModelPrefix, 0, 2);
            this.tableLayoutParams.Controls.Add(this.nudEpochs, 1, 0);
            this.tableLayoutParams.Controls.Add(this.txtModelPrefix, 1, 3);
            this.tableLayoutParams.Controls.Add(this.lblScript, 0, 4);
            this.tableLayoutParams.Controls.Add(this.cmbScript, 1, 4);
            this.tableLayoutParams.Controls.Add(this.lblResNetVariant, 2, 4);
            this.tableLayoutParams.Controls.Add(this.cmbResNetVariant, 3, 4);
            this.tableLayoutParams.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutParams.Location = new System.Drawing.Point(3, 19);
            this.tableLayoutParams.Name = "tableLayoutParams";
            this.tableLayoutParams.Padding = new System.Windows.Forms.Padding(9);
            this.tableLayoutParams.RowCount = 5;
            this.tableLayoutParams.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutParams.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutParams.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutParams.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutParams.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutParams.Size = new System.Drawing.Size(999, 267);
            this.tableLayoutParams.TabIndex = 0;
            // 
            // lblEpochs
            // 
            this.lblEpochs.Location = new System.Drawing.Point(12, 9);
            this.lblEpochs.Name = "lblEpochs";
            this.lblEpochs.Size = new System.Drawing.Size(86, 20);
            this.lblEpochs.TabIndex = 0;
            this.lblEpochs.Text = "Epochs:";
            // 
            // lblBatchSize
            // 
            this.lblBatchSize.Location = new System.Drawing.Point(502, 9);
            this.lblBatchSize.Name = "lblBatchSize";
            this.lblBatchSize.Size = new System.Drawing.Size(86, 20);
            this.lblBatchSize.TabIndex = 2;
            this.lblBatchSize.Text = "Batch Size:";
            // 
            // nudBatchSize
            // 
            this.nudBatchSize.Location = new System.Drawing.Point(747, 12);
            this.nudBatchSize.Maximum = new decimal(new int[] {
            128,
            0,
            0,
            0});
            this.nudBatchSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudBatchSize.Name = "nudBatchSize";
            this.nudBatchSize.Size = new System.Drawing.Size(86, 23);
            this.nudBatchSize.TabIndex = 3;
            this.nudBatchSize.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            // 
            // lblImageSize
            // 
            this.lblImageSize.Location = new System.Drawing.Point(12, 29);
            this.lblImageSize.Name = "lblImageSize";
            this.lblImageSize.Size = new System.Drawing.Size(86, 20);
            this.lblImageSize.TabIndex = 4;
            this.lblImageSize.Text = "Image Size:";
            // 
            // nudImageSize
            // 
            this.nudImageSize.Increment = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.nudImageSize.Location = new System.Drawing.Point(257, 32);
            this.nudImageSize.Maximum = new decimal(new int[] {
            512,
            0,
            0,
            0});
            this.nudImageSize.Minimum = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.nudImageSize.Name = "nudImageSize";
            this.nudImageSize.Size = new System.Drawing.Size(86, 23);
            this.nudImageSize.TabIndex = 5;
            this.nudImageSize.Value = new decimal(new int[] {
            224,
            0,
            0,
            0});
            // 
            // lblLearningRate
            // 
            this.lblLearningRate.Location = new System.Drawing.Point(502, 29);
            this.lblLearningRate.Name = "lblLearningRate";
            this.lblLearningRate.Size = new System.Drawing.Size(86, 20);
            this.lblLearningRate.TabIndex = 6;
            this.lblLearningRate.Text = "Learning Rate:";
            // 
            // nudLearningRate
            // 
            this.nudLearningRate.DecimalPlaces = 4;
            this.nudLearningRate.Increment = new decimal(new int[] {
            1,
            0,
            0,
            262144});
            this.nudLearningRate.Location = new System.Drawing.Point(747, 32);
            this.nudLearningRate.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.nudLearningRate.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            262144});
            this.nudLearningRate.Name = "nudLearningRate";
            this.nudLearningRate.Size = new System.Drawing.Size(86, 23);
            this.nudLearningRate.TabIndex = 7;
            this.nudLearningRate.Value = new decimal(new int[] {
            1,
            0,
            0,
            262144});
            // 
            // lblModelPrefix
            // 
            this.lblModelPrefix.Location = new System.Drawing.Point(12, 49);
            this.lblModelPrefix.Name = "lblModelPrefix";
            this.lblModelPrefix.Size = new System.Drawing.Size(86, 20);
            this.lblModelPrefix.TabIndex = 8;
            this.lblModelPrefix.Text = "Model Prefix:";
            // 
            // nudEpochs
            // 
            this.nudEpochs.Location = new System.Drawing.Point(257, 12);
            this.nudEpochs.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudEpochs.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudEpochs.Name = "nudEpochs";
            this.nudEpochs.Size = new System.Drawing.Size(86, 23);
            this.nudEpochs.TabIndex = 1;
            this.nudEpochs.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // txtModelPrefix
            // 
            this.txtModelPrefix.Location = new System.Drawing.Point(257, 72);
            this.txtModelPrefix.Name = "txtModelPrefix";
            this.txtModelPrefix.Size = new System.Drawing.Size(114, 23);
            this.txtModelPrefix.TabIndex = 9;
            this.txtModelPrefix.Text = "industrial_model";
            // 
            // lblScript
            // 
            this.lblScript.AutoSize = true;
            this.lblScript.Text = "Eğitim Scripti:";
            // 
            // cmbScript
            // 
            this.cmbScript.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbScript.Items.AddRange(new object[] { "train_model_enhanced.py", "train_model_resnet.py" });
            this.cmbScript.SelectedIndex = 0;
            // 
            // lblResNetVariant
            // 
            this.lblResNetVariant.AutoSize = true;
            this.lblResNetVariant.Text = "ResNet Variant:";
            // 
            // cmbResNetVariant
            // 
            this.cmbResNetVariant.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbResNetVariant.Items.AddRange(new object[] { "ResNet50", "ResNet101", "ResNet152" });
            this.cmbResNetVariant.SelectedIndex = 0;
            // 
            // grpDataset
            // 
            this.grpDataset.BackColor = System.Drawing.Color.White;
            this.grpDataset.Controls.Add(this.txtDatasetPath);
            this.grpDataset.Controls.Add(this.btnBrowseDataset);
            this.grpDataset.Controls.Add(this.lblDatasetStatus);
            this.grpDataset.Controls.Add(this.pbDatasetStatus);
            this.grpDataset.Controls.Add(this.txtClassInfo);
            this.grpDataset.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.grpDataset.Location = new System.Drawing.Point(13, 13);
            this.grpDataset.Name = "grpDataset";
            this.grpDataset.Size = new System.Drawing.Size(1005, 254);
            this.grpDataset.TabIndex = 3;
            this.grpDataset.TabStop = false;
            this.grpDataset.Text = "📁 Veri Seti";
            // 
            // txtDatasetPath
            // 
            this.txtDatasetPath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.txtDatasetPath.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtDatasetPath.Location = new System.Drawing.Point(13, 26);
            this.txtDatasetPath.Name = "txtDatasetPath";
            this.txtDatasetPath.ReadOnly = true;
            this.txtDatasetPath.Size = new System.Drawing.Size(361, 22);
            this.txtDatasetPath.TabIndex = 0;
            // 
            // btnBrowseDataset
            // 
            this.btnBrowseDataset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.btnBrowseDataset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseDataset.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnBrowseDataset.ForeColor = System.Drawing.Color.White;
            this.btnBrowseDataset.Location = new System.Drawing.Point(381, 26);
            this.btnBrowseDataset.Name = "btnBrowseDataset";
            this.btnBrowseDataset.Size = new System.Drawing.Size(86, 22);
            this.btnBrowseDataset.TabIndex = 1;
            this.btnBrowseDataset.Text = "📂 Gözat";
            this.btnBrowseDataset.UseVisualStyleBackColor = false;
            // 
            // lblDatasetStatus
            // 
            this.lblDatasetStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDatasetStatus.ForeColor = System.Drawing.Color.Red;
            this.lblDatasetStatus.Location = new System.Drawing.Point(13, 56);
            this.lblDatasetStatus.Name = "lblDatasetStatus";
            this.lblDatasetStatus.Size = new System.Drawing.Size(171, 17);
            this.lblDatasetStatus.TabIndex = 2;
            this.lblDatasetStatus.Text = "❌ Veri seti: Seçilmedi";
            // 
            // pbDatasetStatus
            // 
            this.pbDatasetStatus.BackColor = System.Drawing.Color.Red;
            this.pbDatasetStatus.Location = new System.Drawing.Point(189, 56);
            this.pbDatasetStatus.Name = "pbDatasetStatus";
            this.pbDatasetStatus.Size = new System.Drawing.Size(14, 14);
            this.pbDatasetStatus.TabIndex = 3;
            this.pbDatasetStatus.TabStop = false;
            // 
            // txtClassInfo
            // 
            this.txtClassInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.txtClassInfo.Font = new System.Drawing.Font("Consolas", 8F);
            this.txtClassInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            this.txtClassInfo.Location = new System.Drawing.Point(13, 78);
            this.txtClassInfo.Multiline = true;
            this.txtClassInfo.Name = "txtClassInfo";
            this.txtClassInfo.ReadOnly = true;
            this.txtClassInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtClassInfo.Size = new System.Drawing.Size(454, 35);
            this.txtClassInfo.TabIndex = 4;
            // 
            // tabAdvanced
            // 
            this.tabAdvanced.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this.tabAdvanced.Controls.Add(this.grpConfig);
            this.tabAdvanced.Controls.Add(this.grpOptimization);
            this.tabAdvanced.Controls.Add(this.grpModelOptions);
            this.tabAdvanced.Location = new System.Drawing.Point(4, 24);
            this.tabAdvanced.Name = "tabAdvanced";
            this.tabAdvanced.Padding = new System.Windows.Forms.Padding(13);
            this.tabAdvanced.Size = new System.Drawing.Size(1021, 643);
            this.tabAdvanced.TabIndex = 1;
            this.tabAdvanced.Text = "🔧 Gelişmiş";
            // 
            // grpConfig
            // 
            this.grpConfig.BackColor = System.Drawing.Color.White;
            this.grpConfig.Controls.Add(this.btnSaveConfig);
            this.grpConfig.Controls.Add(this.btnLoadConfig);
            this.grpConfig.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.grpConfig.Location = new System.Drawing.Point(13, 360);
            this.grpConfig.Name = "grpConfig";
            this.grpConfig.Size = new System.Drawing.Size(1000, 280);
            this.grpConfig.TabIndex = 0;
            this.grpConfig.TabStop = false;
            this.grpConfig.Text = "💾 Konfigürasyon";
            // 
            // btnSaveConfig
            // 
            this.btnSaveConfig.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.btnSaveConfig.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveConfig.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSaveConfig.ForeColor = System.Drawing.Color.White;
            this.btnSaveConfig.Location = new System.Drawing.Point(13, 26);
            this.btnSaveConfig.Name = "btnSaveConfig";
            this.btnSaveConfig.Size = new System.Drawing.Size(154, 30);
            this.btnSaveConfig.TabIndex = 0;
            this.btnSaveConfig.Text = "💾 Konfigürasyonu Kaydet";
            this.btnSaveConfig.UseVisualStyleBackColor = false;
            // 
            // btnLoadConfig
            // 
            this.btnLoadConfig.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(39)))), ((int)(((byte)(176)))));
            this.btnLoadConfig.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoadConfig.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnLoadConfig.ForeColor = System.Drawing.Color.White;
            this.btnLoadConfig.Location = new System.Drawing.Point(176, 26);
            this.btnLoadConfig.Name = "btnLoadConfig";
            this.btnLoadConfig.Size = new System.Drawing.Size(154, 30);
            this.btnLoadConfig.TabIndex = 1;
            this.btnLoadConfig.Text = "📂 Konfigürasyonu Yükle";
            this.btnLoadConfig.UseVisualStyleBackColor = false;
            // 
            // grpOptimization
            // 
            this.grpOptimization.BackColor = System.Drawing.Color.White;
            this.grpOptimization.Controls.Add(this.lblOptimizer);
            this.grpOptimization.Controls.Add(this.cmbOptimizer);
            this.grpOptimization.Controls.Add(this.lblLossFunction);
            this.grpOptimization.Controls.Add(this.cmbLossFunction);
            this.grpOptimization.Controls.Add(this.lblEarlyStopping);
            this.grpOptimization.Controls.Add(this.cmbEarlyStopping);
            this.grpOptimization.Controls.Add(this.lblReduceLR);
            this.grpOptimization.Controls.Add(this.cmbReduceLR);
            this.grpOptimization.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.grpOptimization.Location = new System.Drawing.Point(13, 178);
            this.grpOptimization.Name = "grpOptimization";
            this.grpOptimization.Size = new System.Drawing.Size(1000, 366);
            this.grpOptimization.TabIndex = 1;
            this.grpOptimization.TabStop = false;
            this.grpOptimization.Text = "⚡ Optimizasyon";
            // 
            // lblOptimizer
            // 
            this.lblOptimizer.AutoSize = true;
            this.lblOptimizer.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblOptimizer.Location = new System.Drawing.Point(13, 26);
            this.lblOptimizer.Name = "lblOptimizer";
            this.lblOptimizer.Size = new System.Drawing.Size(62, 15);
            this.lblOptimizer.TabIndex = 0;
            this.lblOptimizer.Text = "Optimizer:";
            // 
            // cmbOptimizer
            // 
            this.cmbOptimizer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOptimizer.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbOptimizer.Items.AddRange(new object[] {
            "Adam",
            "SGD",
            "RMSprop",
            "AdamW"});
            this.cmbOptimizer.Location = new System.Drawing.Point(103, 24);
            this.cmbOptimizer.Name = "cmbOptimizer";
            this.cmbOptimizer.Size = new System.Drawing.Size(129, 23);
            this.cmbOptimizer.TabIndex = 1;
            // 
            // lblLossFunction
            // 
            this.lblLossFunction.AutoSize = true;
            this.lblLossFunction.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblLossFunction.Location = new System.Drawing.Point(13, 56);
            this.lblLossFunction.Name = "lblLossFunction";
            this.lblLossFunction.Size = new System.Drawing.Size(83, 15);
            this.lblLossFunction.TabIndex = 2;
            this.lblLossFunction.Text = "Loss Function:";
            // 
            // cmbLossFunction
            // 
            this.cmbLossFunction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLossFunction.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbLossFunction.Items.AddRange(new object[] {
            "Categorical Crossentropy",
            "Sparse Categorical Crossentropy",
            "Binary Crossentropy"});
            this.cmbLossFunction.Location = new System.Drawing.Point(103, 55);
            this.cmbLossFunction.Name = "cmbLossFunction";
            this.cmbLossFunction.Size = new System.Drawing.Size(172, 23);
            this.cmbLossFunction.TabIndex = 3;
            // 
            // lblEarlyStopping
            // 
            this.lblEarlyStopping.AutoSize = true;
            this.lblEarlyStopping.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblEarlyStopping.Location = new System.Drawing.Point(13, 87);
            this.lblEarlyStopping.Name = "lblEarlyStopping";
            this.lblEarlyStopping.Size = new System.Drawing.Size(86, 15);
            this.lblEarlyStopping.TabIndex = 4;
            this.lblEarlyStopping.Text = "Early Stopping:";
            // 
            // cmbEarlyStopping
            // 
            this.cmbEarlyStopping.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEarlyStopping.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbEarlyStopping.Items.AddRange(new object[] {
            "5",
            "8",
            "10",
            "15",
            "20"});
            this.cmbEarlyStopping.Location = new System.Drawing.Point(103, 85);
            this.cmbEarlyStopping.Name = "cmbEarlyStopping";
            this.cmbEarlyStopping.Size = new System.Drawing.Size(86, 23);
            this.cmbEarlyStopping.TabIndex = 5;
            // 
            // lblReduceLR
            // 
            this.lblReduceLR.AutoSize = true;
            this.lblReduceLR.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblReduceLR.Location = new System.Drawing.Point(13, 117);
            this.lblReduceLR.Name = "lblReduceLR";
            this.lblReduceLR.Size = new System.Drawing.Size(65, 15);
            this.lblReduceLR.TabIndex = 6;
            this.lblReduceLR.Text = "Reduce LR:";
            // 
            // cmbReduceLR
            // 
            this.cmbReduceLR.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbReduceLR.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbReduceLR.Items.AddRange(new object[] {
            "2",
            "3",
            "5",
            "8"});
            this.cmbReduceLR.Location = new System.Drawing.Point(103, 115);
            this.cmbReduceLR.Name = "cmbReduceLR";
            this.cmbReduceLR.Size = new System.Drawing.Size(86, 23);
            this.cmbReduceLR.TabIndex = 7;
            // 
            // grpModelOptions
            // 
            this.grpModelOptions.BackColor = System.Drawing.Color.White;
            this.grpModelOptions.Controls.Add(this.chkEnableFineTuning);
            this.grpModelOptions.Controls.Add(this.chkEnableClassWeights);
            this.grpModelOptions.Controls.Add(this.chkEnableDataQuality);
            this.grpModelOptions.Controls.Add(this.chkEnablePerformanceMetrics);
            this.grpModelOptions.Controls.Add(this.chkEnableModelVersioning);
            this.grpModelOptions.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.grpModelOptions.Location = new System.Drawing.Point(13, 13);
            this.grpModelOptions.Name = "grpModelOptions";
            this.grpModelOptions.Size = new System.Drawing.Size(1000, 349);
            this.grpModelOptions.TabIndex = 2;
            this.grpModelOptions.TabStop = false;
            this.grpModelOptions.Text = "🎛️ Model Seçenekleri";
            // 
            // chkEnableFineTuning
            // 
            this.chkEnableFineTuning.Checked = true;
            this.chkEnableFineTuning.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEnableFineTuning.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkEnableFineTuning.Location = new System.Drawing.Point(13, 26);
            this.chkEnableFineTuning.Name = "chkEnableFineTuning";
            this.chkEnableFineTuning.Size = new System.Drawing.Size(214, 22);
            this.chkEnableFineTuning.TabIndex = 0;
            this.chkEnableFineTuning.Text = "🔧 Fine-tuning Etkinleştir";
            // 
            // chkEnableClassWeights
            // 
            this.chkEnableClassWeights.Checked = true;
            this.chkEnableClassWeights.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEnableClassWeights.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkEnableClassWeights.Location = new System.Drawing.Point(13, 52);
            this.chkEnableClassWeights.Name = "chkEnableClassWeights";
            this.chkEnableClassWeights.Size = new System.Drawing.Size(214, 22);
            this.chkEnableClassWeights.TabIndex = 1;
            this.chkEnableClassWeights.Text = "⚖️ Class Weights Etkinleştir";
            // 
            // chkEnableDataQuality
            // 
            this.chkEnableDataQuality.Checked = true;
            this.chkEnableDataQuality.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEnableDataQuality.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkEnableDataQuality.Location = new System.Drawing.Point(13, 78);
            this.chkEnableDataQuality.Name = "chkEnableDataQuality";
            this.chkEnableDataQuality.Size = new System.Drawing.Size(214, 22);
            this.chkEnableDataQuality.TabIndex = 2;
            this.chkEnableDataQuality.Text = "🔍 Veri Kalitesi Kontrolü";
            // 
            // chkEnablePerformanceMetrics
            // 
            this.chkEnablePerformanceMetrics.Checked = true;
            this.chkEnablePerformanceMetrics.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEnablePerformanceMetrics.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkEnablePerformanceMetrics.Location = new System.Drawing.Point(13, 104);
            this.chkEnablePerformanceMetrics.Name = "chkEnablePerformanceMetrics";
            this.chkEnablePerformanceMetrics.Size = new System.Drawing.Size(214, 22);
            this.chkEnablePerformanceMetrics.TabIndex = 3;
            this.chkEnablePerformanceMetrics.Text = "📊 Performans Metrikleri";
            // 
            // chkEnableModelVersioning
            // 
            this.chkEnableModelVersioning.Checked = true;
            this.chkEnableModelVersioning.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEnableModelVersioning.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkEnableModelVersioning.Location = new System.Drawing.Point(13, 130);
            this.chkEnableModelVersioning.Name = "chkEnableModelVersioning";
            this.chkEnableModelVersioning.Size = new System.Drawing.Size(214, 22);
            this.chkEnableModelVersioning.TabIndex = 4;
            this.chkEnableModelVersioning.Text = "📦 Model Versiyonlama";
            // 
            // tabMonitoring
            // 
            this.tabMonitoring.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this.tabMonitoring.Controls.Add(this.grpResults);
            this.tabMonitoring.Controls.Add(this.grpLogging);
            this.tabMonitoring.Location = new System.Drawing.Point(4, 24);
            this.tabMonitoring.Name = "tabMonitoring";
            this.tabMonitoring.Padding = new System.Windows.Forms.Padding(13);
            this.tabMonitoring.Size = new System.Drawing.Size(1021, 643);
            this.tabMonitoring.TabIndex = 2;
            this.tabMonitoring.Text = "📊 İzleme";
            // 
            // grpResults
            // 
            this.grpResults.BackColor = System.Drawing.Color.White;
            this.grpResults.Controls.Add(this.btnOpenResults);
            this.grpResults.Controls.Add(this.lblResultsInfo);
            this.grpResults.Controls.Add(this.pbResultsStatus);
            this.grpResults.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.grpResults.Location = new System.Drawing.Point(13, 412);
            this.grpResults.Name = "grpResults";
            this.grpResults.Size = new System.Drawing.Size(1005, 228);
            this.grpResults.TabIndex = 0;
            this.grpResults.TabStop = false;
            this.grpResults.Text = "📁 Sonuçlar";
            // 
            // btnOpenResults
            // 
            this.btnOpenResults.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.btnOpenResults.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenResults.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnOpenResults.ForeColor = System.Drawing.Color.White;
            this.btnOpenResults.Location = new System.Drawing.Point(13, 26);
            this.btnOpenResults.Name = "btnOpenResults";
            this.btnOpenResults.Size = new System.Drawing.Size(154, 30);
            this.btnOpenResults.TabIndex = 0;
            this.btnOpenResults.Text = "📂 Sonuç Klasörünü Aç";
            this.btnOpenResults.UseVisualStyleBackColor = false;
            // 
            // lblResultsInfo
            // 
            this.lblResultsInfo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblResultsInfo.ForeColor = System.Drawing.Color.Gray;
            this.lblResultsInfo.Location = new System.Drawing.Point(180, 35);
            this.lblResultsInfo.Name = "lblResultsInfo";
            this.lblResultsInfo.Size = new System.Drawing.Size(300, 17);
            this.lblResultsInfo.TabIndex = 1;
            this.lblResultsInfo.Text = "📊 Eğitim tamamlandıktan sonra sonuçlar burada görüntülenecek.";
            // 
            // pbResultsStatus
            // 
            this.pbResultsStatus.BackColor = System.Drawing.Color.Gray;
            this.pbResultsStatus.Location = new System.Drawing.Point(163, 35);
            this.pbResultsStatus.Name = "pbResultsStatus";
            this.pbResultsStatus.Size = new System.Drawing.Size(14, 14);
            this.pbResultsStatus.TabIndex = 2;
            this.pbResultsStatus.TabStop = false;
            // 
            // grpLogging
            // 
            this.grpLogging.BackColor = System.Drawing.Color.White;
            this.grpLogging.Controls.Add(this.txtTrainingLog);
            this.grpLogging.Controls.Add(this.btnClearLog);
            this.grpLogging.Controls.Add(this.btnSaveLog);
            this.grpLogging.Controls.Add(this.lblLogCount);
            this.grpLogging.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.grpLogging.Location = new System.Drawing.Point(13, 13);
            this.grpLogging.Name = "grpLogging";
            this.grpLogging.Size = new System.Drawing.Size(1005, 531);
            this.grpLogging.TabIndex = 1;
            this.grpLogging.TabStop = false;
            this.grpLogging.Text = "📋 Eğitim Logları";
            // 
            // txtTrainingLog
            // 
            this.txtTrainingLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.txtTrainingLog.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtTrainingLog.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            this.txtTrainingLog.Location = new System.Drawing.Point(13, 26);
            this.txtTrainingLog.Multiline = true;
            this.txtTrainingLog.Name = "txtTrainingLog";
            this.txtTrainingLog.ReadOnly = true;
            this.txtTrainingLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtTrainingLog.Size = new System.Drawing.Size(979, 304);
            this.txtTrainingLog.TabIndex = 0;
            // 
            // btnClearLog
            // 
            this.btnClearLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(158)))), ((int)(((byte)(158)))));
            this.btnClearLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearLog.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnClearLog.ForeColor = System.Drawing.Color.White;
            this.btnClearLog.Location = new System.Drawing.Point(13, 338);
            this.btnClearLog.Name = "btnClearLog";
            this.btnClearLog.Size = new System.Drawing.Size(103, 30);
            this.btnClearLog.TabIndex = 1;
            this.btnClearLog.Text = "🗑️ Logları Temizle";
            this.btnClearLog.UseVisualStyleBackColor = false;
            // 
            // btnSaveLog
            // 
            this.btnSaveLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnSaveLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveLog.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSaveLog.ForeColor = System.Drawing.Color.White;
            this.btnSaveLog.Location = new System.Drawing.Point(124, 338);
            this.btnSaveLog.Name = "btnSaveLog";
            this.btnSaveLog.Size = new System.Drawing.Size(103, 30);
            this.btnSaveLog.TabIndex = 2;
            this.btnSaveLog.Text = "💾 Logları Kaydet";
            this.btnSaveLog.UseVisualStyleBackColor = false;
            // 
            // lblLogCount
            // 
            this.lblLogCount.AutoSize = true;
            this.lblLogCount.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblLogCount.ForeColor = System.Drawing.Color.Gray;
            this.lblLogCount.Location = new System.Drawing.Point(240, 347);
            this.lblLogCount.Name = "lblLogCount";
            this.lblLogCount.Size = new System.Drawing.Size(86, 15);
            this.lblLogCount.TabIndex = 3;
            this.lblLogCount.Text = "📊 Log Sayısı: 0";
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus,
            this.pbStatus,
            this.lblTime});
            this.statusStrip.Location = new System.Drawing.Point(0, 671);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 12, 0);
            this.statusStrip.Size = new System.Drawing.Size(1029, 22);
            this.statusStrip.TabIndex = 1;
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(72, 17);
            this.lblStatus.Text = "Sistem Hazır";
            // 
            // pbStatus
            // 
            this.pbStatus.ForeColor = System.Drawing.Color.Green;
            this.pbStatus.Name = "pbStatus";
            this.pbStatus.Size = new System.Drawing.Size(14, 17);
            this.pbStatus.Text = "●";
            // 
            // lblTime
            // 
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(49, 17);
            this.lblTime.Text = "19:00:49";
            // 
            // IndustrialTrainingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1029, 693);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.statusStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(859, 612);
            this.Name = "IndustrialTrainingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "🏭 Endüstriyel Kalite Kontrol - Gelişmiş Eğitim Modülü";
            this.tabControl.ResumeLayout(false);
            this.tabTraining.ResumeLayout(false);
            this.grpTraining.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbPythonStatus)).EndInit();
            this.grpAugmentation.ResumeLayout(false);
            this.grpAugmentation.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudScaleMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudScaleMax)).EndInit();
            this.grpTrainingParams.ResumeLayout(false);
            this.tableLayoutParams.ResumeLayout(false);
            this.tableLayoutParams.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBatchSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudImageSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLearningRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEpochs)).EndInit();
            this.grpDataset.ResumeLayout(false);
            this.grpDataset.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbDatasetStatus)).EndInit();
            this.tabAdvanced.ResumeLayout(false);
            this.grpConfig.ResumeLayout(false);
            this.grpOptimization.ResumeLayout(false);
            this.grpOptimization.PerformLayout();
            this.grpModelOptions.ResumeLayout(false);
            this.tabMonitoring.ResumeLayout(false);
            this.grpResults.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbResultsStatus)).EndInit();
            this.grpLogging.ResumeLayout(false);
            this.grpLogging.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
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
        
        // Script and ResNet Variant Controls
        private System.Windows.Forms.Label lblScript;
        private System.Windows.Forms.ComboBox cmbScript;
        private System.Windows.Forms.Label lblResNetVariant;
        private System.Windows.Forms.ComboBox cmbResNetVariant;
        
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