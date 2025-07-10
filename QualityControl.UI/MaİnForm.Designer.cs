namespace QualityControl.UI
{
    partial class MaİnForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MaİnForm));
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.grpCamera = new System.Windows.Forms.GroupBox();
            this.pictureBoxPreview = new System.Windows.Forms.PictureBox();
            this.panelCameraControls = new System.Windows.Forms.Panel();
            this.btnStartCam = new System.Windows.Forms.Button();
            this.btnStopCam = new System.Windows.Forms.Button();
            this.lblCameraStatus = new System.Windows.Forms.Label();
            this.panelRight = new System.Windows.Forms.Panel();
            this.grpLogging = new System.Windows.Forms.GroupBox();
            this.lstLog = new System.Windows.Forms.ListView();
            this.progressBarTrain = new System.Windows.Forms.ProgressBar();
            this.lblProgress = new System.Windows.Forms.Label();
            this.panelTrainingButtons = new System.Windows.Forms.Panel();
            this.btnTrain = new System.Windows.Forms.Button();
            this.btnIndustrialTraining = new System.Windows.Forms.Button();
            this.grpParameters = new System.Windows.Forms.GroupBox();
            this.tableLayoutParams = new System.Windows.Forms.TableLayoutPanel();
            this.lblEpoch = new System.Windows.Forms.Label();
            this.nudEpoch = new System.Windows.Forms.NumericUpDown();
            this.lblBatchSize = new System.Windows.Forms.Label();
            this.nudBatchSize = new System.Windows.Forms.NumericUpDown();
            this.lblLr = new System.Windows.Forms.Label();
            this.nudLr = new System.Windows.Forms.NumericUpDown();
            this.lblMin = new System.Windows.Forms.Label();
            this.nudMin = new System.Windows.Forms.NumericUpDown();
            this.lblMax = new System.Windows.Forms.Label();
            this.nudMax = new System.Windows.Forms.NumericUpDown();
            this.grpTraining = new System.Windows.Forms.GroupBox();
            this.lblClassCount = new System.Windows.Forms.Label();
            this.cmbClassCount = new System.Windows.Forms.ComboBox();
            this.panelSampleControls = new System.Windows.Forms.TableLayoutPanel();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.pbStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.FrameTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            this.panelLeft.SuspendLayout();
            this.grpCamera.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).BeginInit();
            this.panelCameraControls.SuspendLayout();
            this.panelRight.SuspendLayout();
            this.grpLogging.SuspendLayout();
            this.panelTrainingButtons.SuspendLayout();
            this.grpParameters.SuspendLayout();
            this.tableLayoutParams.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudEpoch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBatchSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMax)).BeginInit();
            this.grpTraining.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMain.Location = new System.Drawing.Point(0, 0);
            this.splitContainerMain.Name = "splitContainerMain";
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.panelLeft);
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.panelRight);
            this.splitContainerMain.Size = new System.Drawing.Size(1029, 671);
            this.splitContainerMain.SplitterDistance = 514;
            this.splitContainerMain.SplitterWidth = 3;
            this.splitContainerMain.TabIndex = 0;
            // 
            // panelLeft
            // 
            this.panelLeft.Controls.Add(this.grpCamera);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Padding = new System.Windows.Forms.Padding(9);
            this.panelLeft.Size = new System.Drawing.Size(514, 671);
            this.panelLeft.TabIndex = 0;
            // 
            // grpCamera
            // 
            this.grpCamera.Controls.Add(this.pictureBoxPreview);
            this.grpCamera.Controls.Add(this.panelCameraControls);
            this.grpCamera.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpCamera.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.grpCamera.Location = new System.Drawing.Point(9, 9);
            this.grpCamera.Name = "grpCamera";
            this.grpCamera.Size = new System.Drawing.Size(496, 653);
            this.grpCamera.TabIndex = 0;
            this.grpCamera.TabStop = false;
            this.grpCamera.Text = "📹 Kamera Görüntüsü";
            // 
            // pictureBoxPreview
            // 
            this.pictureBoxPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxPreview.Location = new System.Drawing.Point(3, 19);
            this.pictureBoxPreview.Name = "pictureBoxPreview";
            this.pictureBoxPreview.Size = new System.Drawing.Size(490, 588);
            this.pictureBoxPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxPreview.TabIndex = 0;
            this.pictureBoxPreview.TabStop = false;
            // 
            // panelCameraControls
            // 
            this.panelCameraControls.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.panelCameraControls.Controls.Add(this.btnStartCam);
            this.panelCameraControls.Controls.Add(this.btnStopCam);
            this.panelCameraControls.Controls.Add(this.lblCameraStatus);
            this.panelCameraControls.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelCameraControls.Location = new System.Drawing.Point(3, 607);
            this.panelCameraControls.Name = "panelCameraControls";
            this.panelCameraControls.Size = new System.Drawing.Size(490, 43);
            this.panelCameraControls.TabIndex = 1;
            // 
            // btnStartCam
            // 
            this.btnStartCam.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnStartCam.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStartCam.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnStartCam.ForeColor = System.Drawing.Color.White;
            this.btnStartCam.Location = new System.Drawing.Point(9, 7);
            this.btnStartCam.Name = "btnStartCam";
            this.btnStartCam.Size = new System.Drawing.Size(103, 30);
            this.btnStartCam.TabIndex = 0;
            this.btnStartCam.Text = "▶️ Kamera Başlat";
            this.btnStartCam.UseVisualStyleBackColor = false;
            // 
            // btnStopCam
            // 
            this.btnStopCam.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.btnStopCam.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStopCam.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnStopCam.ForeColor = System.Drawing.Color.White;
            this.btnStopCam.Location = new System.Drawing.Point(120, 7);
            this.btnStopCam.Name = "btnStopCam";
            this.btnStopCam.Size = new System.Drawing.Size(103, 30);
            this.btnStopCam.TabIndex = 1;
            this.btnStopCam.Text = "⏹️ Kamera Durdur";
            this.btnStopCam.UseVisualStyleBackColor = false;
            // 
            // lblCameraStatus
            // 
            this.lblCameraStatus.AutoSize = true;
            this.lblCameraStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCameraStatus.ForeColor = System.Drawing.Color.Red;
            this.lblCameraStatus.Location = new System.Drawing.Point(240, 13);
            this.lblCameraStatus.Name = "lblCameraStatus";
            this.lblCameraStatus.Size = new System.Drawing.Size(97, 15);
            this.lblCameraStatus.TabIndex = 2;
            this.lblCameraStatus.Text = "🔴 Kamera Kapalı";
            // 
            // panelRight
            // 
            this.panelRight.Controls.Add(this.grpLogging);
            this.panelRight.Controls.Add(this.panelTrainingButtons);
            this.panelRight.Controls.Add(this.grpParameters);
            this.panelRight.Controls.Add(this.grpTraining);
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRight.Location = new System.Drawing.Point(0, 0);
            this.panelRight.Name = "panelRight";
            this.panelRight.Padding = new System.Windows.Forms.Padding(9);
            this.panelRight.Size = new System.Drawing.Size(512, 671);
            this.panelRight.TabIndex = 0;
            // 
            // grpLogging
            // 
            this.grpLogging.Controls.Add(this.lstLog);
            this.grpLogging.Controls.Add(this.progressBarTrain);
            this.grpLogging.Controls.Add(this.lblProgress);
            this.grpLogging.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpLogging.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.grpLogging.Location = new System.Drawing.Point(9, 477);
            this.grpLogging.Name = "grpLogging";
            this.grpLogging.Size = new System.Drawing.Size(494, 185);
            this.grpLogging.TabIndex = 0;
            this.grpLogging.TabStop = false;
            this.grpLogging.Text = "📋 Log ve İlerleme";
            // 
            // lstLog
            // 
            this.lstLog.FullRowSelect = true;
            this.lstLog.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lstLog.HideSelection = false;
            this.lstLog.Location = new System.Drawing.Point(0, 34);
            this.lstLog.Name = "lstLog";
            this.lstLog.Size = new System.Drawing.Size(488, 120);
            this.lstLog.TabIndex = 0;
            this.lstLog.UseCompatibleStateImageBehavior = false;
            this.lstLog.View = System.Windows.Forms.View.Details;
            // 
            // progressBarTrain
            // 
            this.progressBarTrain.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBarTrain.Location = new System.Drawing.Point(3, 160);
            this.progressBarTrain.Name = "progressBarTrain";
            this.progressBarTrain.Size = new System.Drawing.Size(488, 22);
            this.progressBarTrain.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBarTrain.TabIndex = 1;
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblProgress.Location = new System.Drawing.Point(9, 22);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(33, 13);
            this.lblProgress.TabIndex = 2;
            this.lblProgress.Text = "Hazır";
            // 
            // panelTrainingButtons
            // 
            this.panelTrainingButtons.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.panelTrainingButtons.Controls.Add(this.btnTrain);
            this.panelTrainingButtons.Controls.Add(this.btnIndustrialTraining);
            this.panelTrainingButtons.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTrainingButtons.Location = new System.Drawing.Point(9, 425);
            this.panelTrainingButtons.Name = "panelTrainingButtons";
            this.panelTrainingButtons.Size = new System.Drawing.Size(494, 52);
            this.panelTrainingButtons.TabIndex = 1;
            // 
            // btnTrain
            // 
            this.btnTrain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.btnTrain.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTrain.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnTrain.ForeColor = System.Drawing.Color.White;
            this.btnTrain.Location = new System.Drawing.Point(9, 7);
            this.btnTrain.Name = "btnTrain";
            this.btnTrain.Size = new System.Drawing.Size(129, 39);
            this.btnTrain.TabIndex = 0;
            this.btnTrain.Text = "🚀 Basit Eğitim";
            this.btnTrain.UseVisualStyleBackColor = false;
            // 
            // btnIndustrialTraining
            // 
            this.btnIndustrialTraining.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(39)))), ((int)(((byte)(176)))));
            this.btnIndustrialTraining.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIndustrialTraining.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnIndustrialTraining.ForeColor = System.Drawing.Color.White;
            this.btnIndustrialTraining.Location = new System.Drawing.Point(146, 7);
            this.btnIndustrialTraining.Name = "btnIndustrialTraining";
            this.btnIndustrialTraining.Size = new System.Drawing.Size(129, 39);
            this.btnIndustrialTraining.TabIndex = 1;
            this.btnIndustrialTraining.Text = "🏭 Endüstriyel Eğitim";
            this.btnIndustrialTraining.UseVisualStyleBackColor = false;
            // 
            // grpParameters
            // 
            this.grpParameters.Controls.Add(this.tableLayoutParams);
            this.grpParameters.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpParameters.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.grpParameters.Location = new System.Drawing.Point(9, 269);
            this.grpParameters.Name = "grpParameters";
            this.grpParameters.Size = new System.Drawing.Size(494, 156);
            this.grpParameters.TabIndex = 2;
            this.grpParameters.TabStop = false;
            this.grpParameters.Text = "⚙️ Eğitim Parametreleri";
            // 
            // tableLayoutParams
            // 
            this.tableLayoutParams.ColumnCount = 4;
            this.tableLayoutParams.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutParams.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutParams.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutParams.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutParams.Controls.Add(this.lblEpoch, 0, 0);
            this.tableLayoutParams.Controls.Add(this.nudEpoch, 1, 0);
            this.tableLayoutParams.Controls.Add(this.lblBatchSize, 2, 0);
            this.tableLayoutParams.Controls.Add(this.nudBatchSize, 3, 0);
            this.tableLayoutParams.Controls.Add(this.lblLr, 0, 1);
            this.tableLayoutParams.Controls.Add(this.nudLr, 1, 1);
            this.tableLayoutParams.Controls.Add(this.lblMin, 2, 1);
            this.tableLayoutParams.Controls.Add(this.nudMin, 3, 1);
            this.tableLayoutParams.Controls.Add(this.lblMax, 0, 2);
            this.tableLayoutParams.Controls.Add(this.nudMax, 1, 2);
            this.tableLayoutParams.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutParams.Location = new System.Drawing.Point(3, 19);
            this.tableLayoutParams.Name = "tableLayoutParams";
            this.tableLayoutParams.RowCount = 3;
            this.tableLayoutParams.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutParams.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutParams.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutParams.Size = new System.Drawing.Size(488, 134);
            this.tableLayoutParams.TabIndex = 0;
            // 
            // lblEpoch
            // 
            this.lblEpoch.Location = new System.Drawing.Point(3, 0);
            this.lblEpoch.Name = "lblEpoch";
            this.lblEpoch.Size = new System.Drawing.Size(86, 20);
            this.lblEpoch.TabIndex = 0;
            this.lblEpoch.Text = "Epoch:";
            // 
            // nudEpoch
            // 
            this.nudEpoch.Location = new System.Drawing.Point(125, 3);
            this.nudEpoch.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudEpoch.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudEpoch.Name = "nudEpoch";
            this.nudEpoch.Size = new System.Drawing.Size(69, 23);
            this.nudEpoch.TabIndex = 1;
            this.nudEpoch.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // lblBatchSize
            // 
            this.lblBatchSize.Location = new System.Drawing.Point(247, 0);
            this.lblBatchSize.Name = "lblBatchSize";
            this.lblBatchSize.Size = new System.Drawing.Size(86, 20);
            this.lblBatchSize.TabIndex = 2;
            this.lblBatchSize.Text = "Batch Size:";
            // 
            // nudBatchSize
            // 
            this.nudBatchSize.Location = new System.Drawing.Point(369, 3);
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
            this.nudBatchSize.Size = new System.Drawing.Size(69, 23);
            this.nudBatchSize.TabIndex = 3;
            this.nudBatchSize.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            // 
            // lblLr
            // 
            this.lblLr.Location = new System.Drawing.Point(3, 20);
            this.lblLr.Name = "lblLr";
            this.lblLr.Size = new System.Drawing.Size(86, 20);
            this.lblLr.TabIndex = 4;
            this.lblLr.Text = "Learning Rate:";
            // 
            // nudLr
            // 
            this.nudLr.DecimalPlaces = 5;
            this.nudLr.Increment = new decimal(new int[] {
            1,
            0,
            0,
            262144});
            this.nudLr.Location = new System.Drawing.Point(125, 23);
            this.nudLr.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.nudLr.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            327680});
            this.nudLr.Name = "nudLr";
            this.nudLr.Size = new System.Drawing.Size(69, 23);
            this.nudLr.TabIndex = 5;
            this.nudLr.Value = new decimal(new int[] {
            1,
            0,
            0,
            262144});
            // 
            // lblMin
            // 
            this.lblMin.Location = new System.Drawing.Point(247, 20);
            this.lblMin.Name = "lblMin";
            this.lblMin.Size = new System.Drawing.Size(86, 20);
            this.lblMin.TabIndex = 6;
            this.lblMin.Text = "Scale Min:";
            // 
            // nudMin
            // 
            this.nudMin.DecimalPlaces = 2;
            this.nudMin.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudMin.Location = new System.Drawing.Point(369, 23);
            this.nudMin.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            65536});
            this.nudMin.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudMin.Name = "nudMin";
            this.nudMin.Size = new System.Drawing.Size(69, 23);
            this.nudMin.TabIndex = 7;
            this.nudMin.Value = new decimal(new int[] {
            9,
            0,
            0,
            65536});
            // 
            // lblMax
            // 
            this.lblMax.Location = new System.Drawing.Point(3, 40);
            this.lblMax.Name = "lblMax";
            this.lblMax.Size = new System.Drawing.Size(86, 20);
            this.lblMax.TabIndex = 8;
            this.lblMax.Text = "Scale Max:";
            // 
            // nudMax
            // 
            this.nudMax.DecimalPlaces = 2;
            this.nudMax.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudMax.Location = new System.Drawing.Point(125, 43);
            this.nudMax.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            65536});
            this.nudMax.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudMax.Name = "nudMax";
            this.nudMax.Size = new System.Drawing.Size(69, 23);
            this.nudMax.TabIndex = 9;
            this.nudMax.Value = new decimal(new int[] {
            11,
            0,
            0,
            65536});
            // 
            // grpTraining
            // 
            this.grpTraining.Controls.Add(this.lblClassCount);
            this.grpTraining.Controls.Add(this.cmbClassCount);
            this.grpTraining.Controls.Add(this.panelSampleControls);
            this.grpTraining.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpTraining.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.grpTraining.Location = new System.Drawing.Point(9, 9);
            this.grpTraining.Name = "grpTraining";
            this.grpTraining.Size = new System.Drawing.Size(494, 260);
            this.grpTraining.TabIndex = 3;
            this.grpTraining.TabStop = false;
            this.grpTraining.Text = "🎯 Örnek Toplama";
            // 
            // lblClassCount
            // 
            this.lblClassCount.AutoSize = true;
            this.lblClassCount.Location = new System.Drawing.Point(9, 22);
            this.lblClassCount.Name = "lblClassCount";
            this.lblClassCount.Size = new System.Drawing.Size(68, 15);
            this.lblClassCount.TabIndex = 0;
            this.lblClassCount.Text = "Sınıf Sayısı:";
            // 
            // cmbClassCount
            // 
            this.cmbClassCount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbClassCount.Items.AddRange(new object[] {
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.cmbClassCount.Location = new System.Drawing.Point(86, 19);
            this.cmbClassCount.Name = "cmbClassCount";
            this.cmbClassCount.Size = new System.Drawing.Size(69, 23);
            this.cmbClassCount.TabIndex = 1;
            // 
            // panelSampleControls
            // 
            this.panelSampleControls.AutoScroll = true;
            this.panelSampleControls.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this.panelSampleControls.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelSampleControls.ColumnCount = 4;
            this.panelSampleControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.panelSampleControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.panelSampleControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.panelSampleControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.panelSampleControls.Location = new System.Drawing.Point(9, 48);
            this.panelSampleControls.Name = "panelSampleControls";
            this.panelSampleControls.RowCount = 1;
            this.panelSampleControls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 198F));
            this.panelSampleControls.Size = new System.Drawing.Size(470, 200);
            this.panelSampleControls.TabIndex = 2;
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus,
            this.pbStatus});
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
            // MaİnForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1029, 693);
            this.Controls.Add(this.splitContainerMain);
            this.Controls.Add(this.statusStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(859, 612);
            this.Name = "MaİnForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "🎯 Kalite Kontrol - Eğitim Modülü";
            this.Load += new System.EventHandler(this.MaİnForm_Load_1);
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            this.panelLeft.ResumeLayout(false);
            this.grpCamera.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).EndInit();
            this.panelCameraControls.ResumeLayout(false);
            this.panelCameraControls.PerformLayout();
            this.panelRight.ResumeLayout(false);
            this.grpLogging.ResumeLayout(false);
            this.grpLogging.PerformLayout();
            this.panelTrainingButtons.ResumeLayout(false);
            this.grpParameters.ResumeLayout(false);
            this.tableLayoutParams.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudEpoch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBatchSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMax)).EndInit();
            this.grpTraining.ResumeLayout(false);
            this.grpTraining.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #region Windows Form Designer generated code
        private System.Windows.Forms.SplitContainer splitContainerMain;
        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.Panel panelRight;
        private System.Windows.Forms.GroupBox grpCamera;
        private System.Windows.Forms.PictureBox pictureBoxPreview;
        private System.Windows.Forms.Panel panelCameraControls;
        private System.Windows.Forms.Button btnStartCam;
        private System.Windows.Forms.Button btnStopCam;
        private System.Windows.Forms.Label lblCameraStatus;
        private System.Windows.Forms.GroupBox grpTraining;
        private System.Windows.Forms.TableLayoutPanel panelSampleControls;
        private System.Windows.Forms.Label lblClassCount;
        private System.Windows.Forms.ComboBox cmbClassCount;
        private System.Windows.Forms.GroupBox grpParameters;
        private System.Windows.Forms.TableLayoutPanel tableLayoutParams;
        private System.Windows.Forms.Label lblEpoch;
        private System.Windows.Forms.NumericUpDown nudEpoch;
        private System.Windows.Forms.Label lblBatchSize;
        private System.Windows.Forms.NumericUpDown nudBatchSize;
        private System.Windows.Forms.Label lblLr;
        private System.Windows.Forms.NumericUpDown nudLr;
        private System.Windows.Forms.Label lblMin;
        private System.Windows.Forms.NumericUpDown nudMin;
        private System.Windows.Forms.Label lblMax;
        private System.Windows.Forms.NumericUpDown nudMax;
        private System.Windows.Forms.Panel panelTrainingButtons;
        private System.Windows.Forms.Button btnTrain;
        private System.Windows.Forms.Button btnIndustrialTraining;
        private System.Windows.Forms.GroupBox grpLogging;
        private System.Windows.Forms.ListView lstLog;
        private System.Windows.Forms.ProgressBar progressBarTrain;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.ToolStripStatusLabel pbStatus;
        private System.Windows.Forms.Timer FrameTimer;
        #endregion
    }
} 