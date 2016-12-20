namespace PythonControl {
    partial class MainForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint1 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(1D, 27D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint2 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(2D, 20D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint3 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(3D, 18D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint4 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(4D, 27D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint5 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(5D, 27D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint6 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(6D, 29D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint7 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(7D, 36D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint8 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(8D, 27D);
            this.gbLeds = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.BtnFreqLeds = new System.Windows.Forms.Button();
            this.TxtbFreqLeds = new System.Windows.Forms.TextBox();
            this.BtnSendAllLeds = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnBottomBlinkFreq = new System.Windows.Forms.Button();
            this.btnTopBlinkFreq = new System.Windows.Forms.Button();
            this.txtbBottomLeds = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtbTopLeds = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnTBottomLeds = new System.Windows.Forms.Button();
            this.btnTTopLeds = new System.Windows.Forms.Button();
            this.txtbTBottomLeds = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtbTTopLeds = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gbChannelsLeds = new System.Windows.Forms.GroupBox();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.cbCh8Leds = new System.Windows.Forms.CheckBox();
            this.cbCh7Leds = new System.Windows.Forms.CheckBox();
            this.cbCh6Leds = new System.Windows.Forms.CheckBox();
            this.cbCh5Leds = new System.Windows.Forms.CheckBox();
            this.cbCh4Leds = new System.Windows.Forms.CheckBox();
            this.cbCh3Leds = new System.Windows.Forms.CheckBox();
            this.cbCh2Leds = new System.Windows.Forms.CheckBox();
            this.cbCh1Leds = new System.Windows.Forms.CheckBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblPythonReplying = new System.Windows.Forms.ToolStripStatusLabel();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.timerHeartBeat = new System.Windows.Forms.Timer(this.components);
            this.gbLeds.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.gbChannelsLeds.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbLeds
            // 
            this.gbLeds.Controls.Add(this.groupBox3);
            this.gbLeds.Controls.Add(this.BtnSendAllLeds);
            this.gbLeds.Controls.Add(this.groupBox2);
            this.gbLeds.Controls.Add(this.groupBox1);
            this.gbLeds.Controls.Add(this.gbChannelsLeds);
            this.gbLeds.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbLeds.Location = new System.Drawing.Point(0, 0);
            this.gbLeds.Name = "gbLeds";
            this.gbLeds.Size = new System.Drawing.Size(448, 566);
            this.gbLeds.TabIndex = 0;
            this.gbLeds.TabStop = false;
            this.gbLeds.Text = "Светодиоды";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.BtnFreqLeds);
            this.groupBox3.Controls.Add(this.TxtbFreqLeds);
            this.groupBox3.Location = new System.Drawing.Point(316, 19);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(123, 81);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Частота, Гц (1...250)";
            // 
            // BtnFreqLeds
            // 
            this.BtnFreqLeds.Image = global::PythonControl.Properties.Resources.ArrowRight;
            this.BtnFreqLeds.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnFreqLeds.Location = new System.Drawing.Point(6, 45);
            this.BtnFreqLeds.Name = "BtnFreqLeds";
            this.BtnFreqLeds.Size = new System.Drawing.Size(108, 23);
            this.BtnFreqLeds.TabIndex = 10;
            this.BtnFreqLeds.Text = "Ok";
            this.BtnFreqLeds.UseVisualStyleBackColor = true;
            this.BtnFreqLeds.Click += new System.EventHandler(this.BtnFreqLeds_Click);
            // 
            // TxtbFreqLeds
            // 
            this.TxtbFreqLeds.Location = new System.Drawing.Point(6, 17);
            this.TxtbFreqLeds.MaxLength = 36;
            this.TxtbFreqLeds.Name = "TxtbFreqLeds";
            this.TxtbFreqLeds.Size = new System.Drawing.Size(108, 20);
            this.TxtbFreqLeds.TabIndex = 8;
            this.TxtbFreqLeds.Text = "4";
            this.TxtbFreqLeds.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtbFreqLeds_KeyPress);
            // 
            // BtnSendAllLeds
            // 
            this.BtnSendAllLeds.Location = new System.Drawing.Point(316, 461);
            this.BtnSendAllLeds.Name = "BtnSendAllLeds";
            this.BtnSendAllLeds.Size = new System.Drawing.Size(123, 74);
            this.BtnSendAllLeds.TabIndex = 3;
            this.BtnSendAllLeds.Text = "Send all";
            this.BtnSendAllLeds.UseVisualStyleBackColor = true;
            this.BtnSendAllLeds.Click += new System.EventHandler(this.BtnSendAllLeds_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnBottomBlinkFreq);
            this.groupBox2.Controls.Add(this.btnTopBlinkFreq);
            this.groupBox2.Controls.Add(this.txtbBottomLeds);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtbTopLeds);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(6, 454);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(303, 81);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Яркость светодиодов (0...100)";
            // 
            // btnBottomBlinkFreq
            // 
            this.btnBottomBlinkFreq.Image = global::PythonControl.Properties.Resources.ArrowRight;
            this.btnBottomBlinkFreq.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBottomBlinkFreq.Location = new System.Drawing.Point(216, 45);
            this.btnBottomBlinkFreq.Name = "btnBottomBlinkFreq";
            this.btnBottomBlinkFreq.Size = new System.Drawing.Size(75, 23);
            this.btnBottomBlinkFreq.TabIndex = 12;
            this.btnBottomBlinkFreq.Text = "Ok";
            this.btnBottomBlinkFreq.UseVisualStyleBackColor = true;
            this.btnBottomBlinkFreq.Click += new System.EventHandler(this.BtnBottomBlinkFreq_Click);
            // 
            // btnTopBlinkFreq
            // 
            this.btnTopBlinkFreq.Image = global::PythonControl.Properties.Resources.ArrowRight;
            this.btnTopBlinkFreq.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTopBlinkFreq.Location = new System.Drawing.Point(216, 15);
            this.btnTopBlinkFreq.Name = "btnTopBlinkFreq";
            this.btnTopBlinkFreq.Size = new System.Drawing.Size(75, 23);
            this.btnTopBlinkFreq.TabIndex = 11;
            this.btnTopBlinkFreq.Text = "Ok";
            this.btnTopBlinkFreq.UseVisualStyleBackColor = true;
            this.btnTopBlinkFreq.Click += new System.EventHandler(this.BtnTopBlinkFreq_Click);
            // 
            // txtbBottomLeds
            // 
            this.txtbBottomLeds.Location = new System.Drawing.Point(162, 47);
            this.txtbBottomLeds.Name = "txtbBottomLeds";
            this.txtbBottomLeds.Size = new System.Drawing.Size(49, 20);
            this.txtbBottomLeds.TabIndex = 9;
            this.txtbBottomLeds.Text = "1";
            this.txtbBottomLeds.WordWrap = false;
            this.txtbBottomLeds.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtbBottomBlinkFreq_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Максимум, %:";
            // 
            // txtbTopLeds
            // 
            this.txtbTopLeds.Location = new System.Drawing.Point(162, 17);
            this.txtbTopLeds.MaxLength = 36;
            this.txtbTopLeds.Name = "txtbTopLeds";
            this.txtbTopLeds.Size = new System.Drawing.Size(49, 20);
            this.txtbTopLeds.TabIndex = 8;
            this.txtbTopLeds.Text = "100";
            this.txtbTopLeds.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtbTopBlinkFreq_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Минимум, %:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnTBottomLeds);
            this.groupBox1.Controls.Add(this.btnTTopLeds);
            this.groupBox1.Controls.Add(this.txtbTBottomLeds);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtbTTopLeds);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(6, 19);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(303, 81);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Пороги температуры";
            // 
            // btnTBottomLeds
            // 
            this.btnTBottomLeds.Image = global::PythonControl.Properties.Resources.ArrowRight;
            this.btnTBottomLeds.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTBottomLeds.Location = new System.Drawing.Point(216, 45);
            this.btnTBottomLeds.Name = "btnTBottomLeds";
            this.btnTBottomLeds.Size = new System.Drawing.Size(75, 23);
            this.btnTBottomLeds.TabIndex = 11;
            this.btnTBottomLeds.Text = "Ok";
            this.btnTBottomLeds.UseVisualStyleBackColor = true;
            this.btnTBottomLeds.Click += new System.EventHandler(this.BtnTBottomLeds_Click);
            // 
            // btnTTopLeds
            // 
            this.btnTTopLeds.Image = global::PythonControl.Properties.Resources.ArrowRight;
            this.btnTTopLeds.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTTopLeds.Location = new System.Drawing.Point(216, 15);
            this.btnTTopLeds.Name = "btnTTopLeds";
            this.btnTTopLeds.Size = new System.Drawing.Size(75, 23);
            this.btnTTopLeds.TabIndex = 10;
            this.btnTTopLeds.Text = "Ok";
            this.btnTTopLeds.UseVisualStyleBackColor = true;
            this.btnTTopLeds.Click += new System.EventHandler(this.BtnTTopLeds_Click);
            // 
            // txtbTBottomLeds
            // 
            this.txtbTBottomLeds.Location = new System.Drawing.Point(162, 47);
            this.txtbTBottomLeds.Name = "txtbTBottomLeds";
            this.txtbTBottomLeds.Size = new System.Drawing.Size(49, 20);
            this.txtbTBottomLeds.TabIndex = 9;
            this.txtbTBottomLeds.Text = "18";
            this.txtbTBottomLeds.WordWrap = false;
            this.txtbTBottomLeds.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtbTBottomLeds_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(149, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Нижний, градусов Цельсия:";
            // 
            // txtbTTopLeds
            // 
            this.txtbTTopLeds.Location = new System.Drawing.Point(162, 17);
            this.txtbTTopLeds.MaxLength = 36;
            this.txtbTTopLeds.Name = "txtbTTopLeds";
            this.txtbTTopLeds.Size = new System.Drawing.Size(49, 20);
            this.txtbTTopLeds.TabIndex = 8;
            this.txtbTTopLeds.Text = "45";
            this.txtbTTopLeds.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtbTTopLeds_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(151, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Верхний, градусов Цельсия:";
            // 
            // gbChannelsLeds
            // 
            this.gbChannelsLeds.Controls.Add(this.chart1);
            this.gbChannelsLeds.Controls.Add(this.cbCh8Leds);
            this.gbChannelsLeds.Controls.Add(this.cbCh7Leds);
            this.gbChannelsLeds.Controls.Add(this.cbCh6Leds);
            this.gbChannelsLeds.Controls.Add(this.cbCh5Leds);
            this.gbChannelsLeds.Controls.Add(this.cbCh4Leds);
            this.gbChannelsLeds.Controls.Add(this.cbCh3Leds);
            this.gbChannelsLeds.Controls.Add(this.cbCh2Leds);
            this.gbChannelsLeds.Controls.Add(this.cbCh1Leds);
            this.gbChannelsLeds.Location = new System.Drawing.Point(6, 106);
            this.gbChannelsLeds.Name = "gbChannelsLeds";
            this.gbChannelsLeds.Size = new System.Drawing.Size(433, 342);
            this.gbChannelsLeds.TabIndex = 0;
            this.gbChannelsLeds.TabStop = false;
            this.gbChannelsLeds.Text = "Каналы датчика";
            // 
            // chart1
            // 
            chartArea1.AxisX.LabelStyle.Enabled = false;
            chartArea1.AxisX.Maximum = 9D;
            chartArea1.AxisX.Minimum = 0D;
            chartArea1.AxisY.Maximum = 45D;
            chartArea1.AxisY.Minimum = 15D;
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.chart1.Location = new System.Drawing.Point(3, 39);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Name = "Series1";
            dataPoint1.Color = System.Drawing.SystemColors.Highlight;
            dataPoint2.Color = System.Drawing.SystemColors.Highlight;
            dataPoint3.Color = System.Drawing.SystemColors.Highlight;
            dataPoint4.Color = System.Drawing.SystemColors.Highlight;
            dataPoint5.Color = System.Drawing.SystemColors.Highlight;
            dataPoint6.Color = System.Drawing.SystemColors.Highlight;
            dataPoint7.Color = System.Drawing.SystemColors.Highlight;
            dataPoint8.Color = System.Drawing.SystemColors.Highlight;
            series1.Points.Add(dataPoint1);
            series1.Points.Add(dataPoint2);
            series1.Points.Add(dataPoint3);
            series1.Points.Add(dataPoint4);
            series1.Points.Add(dataPoint5);
            series1.Points.Add(dataPoint6);
            series1.Points.Add(dataPoint7);
            series1.Points.Add(dataPoint8);
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(427, 300);
            this.chart1.TabIndex = 8;
            this.chart1.Text = "chart1";
            // 
            // cbCh8Leds
            // 
            this.cbCh8Leds.AutoSize = true;
            this.cbCh8Leds.Checked = true;
            this.cbCh8Leds.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbCh8Leds.Location = new System.Drawing.Point(369, 18);
            this.cbCh8Leds.Name = "cbCh8Leds";
            this.cbCh8Leds.Size = new System.Drawing.Size(15, 14);
            this.cbCh8Leds.TabIndex = 7;
            this.cbCh8Leds.Tag = "8";
            this.cbCh8Leds.UseVisualStyleBackColor = true;
            this.cbCh8Leds.CheckedChanged += new System.EventHandler(this.CbChLeds_CheckedChanged);
            // 
            // cbCh7Leds
            // 
            this.cbCh7Leds.AutoSize = true;
            this.cbCh7Leds.Checked = true;
            this.cbCh7Leds.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbCh7Leds.Location = new System.Drawing.Point(328, 18);
            this.cbCh7Leds.Name = "cbCh7Leds";
            this.cbCh7Leds.Size = new System.Drawing.Size(15, 14);
            this.cbCh7Leds.TabIndex = 6;
            this.cbCh7Leds.Tag = "7";
            this.cbCh7Leds.UseVisualStyleBackColor = true;
            this.cbCh7Leds.CheckedChanged += new System.EventHandler(this.CbChLeds_CheckedChanged);
            // 
            // cbCh6Leds
            // 
            this.cbCh6Leds.AutoSize = true;
            this.cbCh6Leds.Checked = true;
            this.cbCh6Leds.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbCh6Leds.Location = new System.Drawing.Point(287, 18);
            this.cbCh6Leds.Name = "cbCh6Leds";
            this.cbCh6Leds.Size = new System.Drawing.Size(15, 14);
            this.cbCh6Leds.TabIndex = 5;
            this.cbCh6Leds.Tag = "6";
            this.cbCh6Leds.UseVisualStyleBackColor = true;
            this.cbCh6Leds.CheckedChanged += new System.EventHandler(this.CbChLeds_CheckedChanged);
            // 
            // cbCh5Leds
            // 
            this.cbCh5Leds.AutoSize = true;
            this.cbCh5Leds.Checked = true;
            this.cbCh5Leds.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbCh5Leds.Location = new System.Drawing.Point(246, 18);
            this.cbCh5Leds.Name = "cbCh5Leds";
            this.cbCh5Leds.Size = new System.Drawing.Size(15, 14);
            this.cbCh5Leds.TabIndex = 4;
            this.cbCh5Leds.Tag = "5";
            this.cbCh5Leds.UseVisualStyleBackColor = true;
            this.cbCh5Leds.CheckedChanged += new System.EventHandler(this.CbChLeds_CheckedChanged);
            // 
            // cbCh4Leds
            // 
            this.cbCh4Leds.AutoSize = true;
            this.cbCh4Leds.Checked = true;
            this.cbCh4Leds.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbCh4Leds.Location = new System.Drawing.Point(205, 18);
            this.cbCh4Leds.Name = "cbCh4Leds";
            this.cbCh4Leds.Size = new System.Drawing.Size(15, 14);
            this.cbCh4Leds.TabIndex = 3;
            this.cbCh4Leds.Tag = "4";
            this.cbCh4Leds.UseVisualStyleBackColor = true;
            this.cbCh4Leds.CheckedChanged += new System.EventHandler(this.CbChLeds_CheckedChanged);
            // 
            // cbCh3Leds
            // 
            this.cbCh3Leds.AutoSize = true;
            this.cbCh3Leds.Checked = true;
            this.cbCh3Leds.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbCh3Leds.Location = new System.Drawing.Point(164, 18);
            this.cbCh3Leds.Name = "cbCh3Leds";
            this.cbCh3Leds.Size = new System.Drawing.Size(15, 14);
            this.cbCh3Leds.TabIndex = 2;
            this.cbCh3Leds.Tag = "3";
            this.cbCh3Leds.UseVisualStyleBackColor = true;
            this.cbCh3Leds.CheckedChanged += new System.EventHandler(this.CbChLeds_CheckedChanged);
            // 
            // cbCh2Leds
            // 
            this.cbCh2Leds.AutoSize = true;
            this.cbCh2Leds.Checked = true;
            this.cbCh2Leds.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbCh2Leds.Location = new System.Drawing.Point(123, 18);
            this.cbCh2Leds.Name = "cbCh2Leds";
            this.cbCh2Leds.Size = new System.Drawing.Size(15, 14);
            this.cbCh2Leds.TabIndex = 1;
            this.cbCh2Leds.Tag = "2";
            this.cbCh2Leds.UseVisualStyleBackColor = true;
            this.cbCh2Leds.CheckedChanged += new System.EventHandler(this.CbChLeds_CheckedChanged);
            // 
            // cbCh1Leds
            // 
            this.cbCh1Leds.AutoSize = true;
            this.cbCh1Leds.Checked = true;
            this.cbCh1Leds.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbCh1Leds.Location = new System.Drawing.Point(82, 18);
            this.cbCh1Leds.Name = "cbCh1Leds";
            this.cbCh1Leds.Size = new System.Drawing.Size(15, 14);
            this.cbCh1Leds.TabIndex = 0;
            this.cbCh1Leds.Tag = "1";
            this.cbCh1Leds.UseVisualStyleBackColor = true;
            this.cbCh1Leds.CheckedChanged += new System.EventHandler(this.CbChLeds_CheckedChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel,
            this.lblPythonReplying});
            this.statusStrip1.Location = new System.Drawing.Point(0, 544);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(448, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // StatusLabel
            // 
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(154, 17);
            this.StatusLabel.Text = "Передатчик не подключен";
            // 
            // lblPythonReplying
            // 
            this.lblPythonReplying.Name = "lblPythonReplying";
            this.lblPythonReplying.Size = new System.Drawing.Size(109, 17);
            this.lblPythonReplying.Text = "Питон не отвечает";
            this.lblPythonReplying.Visible = false;
            // 
            // serialPort1
            // 
            this.serialPort1.ReadTimeout = 540;
            // 
            // timerHeartBeat
            // 
            this.timerHeartBeat.Enabled = true;
            this.timerHeartBeat.Interval = 1000;
            this.timerHeartBeat.Tick += new System.EventHandler(this.TimerHeartBeat_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(448, 566);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.gbLeds);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Управление индикацией";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.gbLeds.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbChannelsLeds.ResumeLayout(false);
            this.gbChannelsLeds.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbLeds;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox gbChannelsLeds;
        private System.Windows.Forms.CheckBox cbCh8Leds;
        private System.Windows.Forms.CheckBox cbCh7Leds;
        private System.Windows.Forms.CheckBox cbCh6Leds;
        private System.Windows.Forms.CheckBox cbCh5Leds;
        private System.Windows.Forms.CheckBox cbCh4Leds;
        private System.Windows.Forms.CheckBox cbCh3Leds;
        private System.Windows.Forms.CheckBox cbCh2Leds;
        private System.Windows.Forms.CheckBox cbCh1Leds;
        private System.Windows.Forms.TextBox txtbTTopLeds;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtbTBottomLeds;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtbBottomLeds;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtbTopLeds;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel lblPythonReplying;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Timer timerHeartBeat;
        private System.Windows.Forms.Button btnTTopLeds;
        private System.Windows.Forms.Button btnBottomBlinkFreq;
        private System.Windows.Forms.Button btnTopBlinkFreq;
        private System.Windows.Forms.Button btnTBottomLeds;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Button BtnSendAllLeds;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button BtnFreqLeds;
        private System.Windows.Forms.TextBox TxtbFreqLeds;
    }
}

