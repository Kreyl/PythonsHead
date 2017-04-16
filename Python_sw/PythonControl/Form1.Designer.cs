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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint9 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(1D, 27D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint10 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(2D, 20D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint11 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(3D, 18D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint12 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(4D, 27D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint13 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(5D, 27D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint14 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(6D, 29D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint15 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(7D, 36D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint16 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(8D, 27D);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblPythonReplying = new System.Windows.Forms.ToolStripStatusLabel();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.timerHeartBeat = new System.Windows.Forms.Timer(this.components);
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.BtnTLimits = new System.Windows.Forms.Button();
            this.TxtbTLimitTop = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TxtbTLimitBottom = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.BtnLRPoint2 = new System.Windows.Forms.Button();
            this.TxtbLRPwrPoint2 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.TxtbLRTPoint2 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.BtnLRPoint1 = new System.Windows.Forms.Button();
            this.TxtbLRPwrPoint1 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.TxtbLRTPoint1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.chbEnableLR = new System.Windows.Forms.CheckBox();
            this.gbLeds = new System.Windows.Forms.GroupBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.BtnLedPoint2 = new System.Windows.Forms.Button();
            this.TxtbLedPwrPoint2 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.TxtbLedTPoint2 = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.BtnLedPoint1 = new System.Windows.Forms.Button();
            this.TxtbLedPwrPoint1 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.TxtbLedTPoint1 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.chbEnableLeds = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.BtnFreqLeds = new System.Windows.Forms.Button();
            this.TxtbFreqLeds = new System.Windows.Forms.TextBox();
            this.statusStrip1.SuspendLayout();
            this.gbChannelsLeds.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.gbLeds.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel,
            this.lblPythonReplying});
            this.statusStrip1.Location = new System.Drawing.Point(0, 375);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(923, 22);
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
            this.gbChannelsLeds.Location = new System.Drawing.Point(12, 12);
            this.gbChannelsLeds.Name = "gbChannelsLeds";
            this.gbChannelsLeds.Size = new System.Drawing.Size(433, 357);
            this.gbChannelsLeds.TabIndex = 3;
            this.gbChannelsLeds.TabStop = false;
            this.gbChannelsLeds.Text = "Каналы датчика";
            // 
            // chart1
            // 
            chartArea2.AxisX.LabelStyle.Enabled = false;
            chartArea2.AxisX.Maximum = 9D;
            chartArea2.AxisX.Minimum = 0D;
            chartArea2.AxisY.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea2.AxisY.Maximum = 45D;
            chartArea2.AxisY.Minimum = 15D;
            chartArea2.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea2);
            this.chart1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.chart1.Location = new System.Drawing.Point(3, 54);
            this.chart1.Name = "chart1";
            series2.ChartArea = "ChartArea1";
            series2.Name = "Series1";
            dataPoint9.Color = System.Drawing.SystemColors.Highlight;
            dataPoint10.Color = System.Drawing.SystemColors.Highlight;
            dataPoint11.Color = System.Drawing.SystemColors.Highlight;
            dataPoint12.Color = System.Drawing.SystemColors.Highlight;
            dataPoint13.Color = System.Drawing.SystemColors.Highlight;
            dataPoint14.Color = System.Drawing.SystemColors.Highlight;
            dataPoint15.Color = System.Drawing.SystemColors.Highlight;
            dataPoint16.Color = System.Drawing.SystemColors.Highlight;
            series2.Points.Add(dataPoint9);
            series2.Points.Add(dataPoint10);
            series2.Points.Add(dataPoint11);
            series2.Points.Add(dataPoint12);
            series2.Points.Add(dataPoint13);
            series2.Points.Add(dataPoint14);
            series2.Points.Add(dataPoint15);
            series2.Points.Add(dataPoint16);
            this.chart1.Series.Add(series2);
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
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.groupBox4);
            this.panel1.Controls.Add(this.gbLeds);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(452, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(471, 375);
            this.panel1.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.BtnTLimits);
            this.groupBox1.Controls.Add(this.TxtbTLimitTop);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.TxtbTLimitBottom);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(10, 316);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(448, 53);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Пределы температуры на диаграмме";
            // 
            // BtnTLimits
            // 
            this.BtnTLimits.Image = global::PythonControl.Properties.Resources.Checked2;
            this.BtnTLimits.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnTLimits.Location = new System.Drawing.Point(295, 17);
            this.BtnTLimits.Name = "BtnTLimits";
            this.BtnTLimits.Size = new System.Drawing.Size(127, 23);
            this.BtnTLimits.TabIndex = 6;
            this.BtnTLimits.Text = "Ok";
            this.BtnTLimits.UseVisualStyleBackColor = true;
            this.BtnTLimits.Click += new System.EventHandler(this.BtnTLimits_Click);
            // 
            // TxtbTLimitTop
            // 
            this.TxtbTLimitTop.Location = new System.Drawing.Point(201, 19);
            this.TxtbTLimitTop.Name = "TxtbTLimitTop";
            this.TxtbTLimitTop.Size = new System.Drawing.Size(49, 20);
            this.TxtbTLimitTop.TabIndex = 5;
            this.TxtbTLimitTop.Text = "45";
            this.TxtbTLimitTop.TextChanged += new System.EventHandler(this.TxtbChecker);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(145, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Верхняя:";
            // 
            // TxtbTLimitBottom
            // 
            this.TxtbTLimitBottom.Location = new System.Drawing.Point(67, 19);
            this.TxtbTLimitBottom.Name = "TxtbTLimitBottom";
            this.TxtbTLimitBottom.Size = new System.Drawing.Size(49, 20);
            this.TxtbTLimitBottom.TabIndex = 3;
            this.TxtbTLimitBottom.Text = "18";
            this.TxtbTLimitBottom.TextChanged += new System.EventHandler(this.TxtbChecker);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Нижняя:";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.groupBox6);
            this.groupBox4.Controls.Add(this.groupBox5);
            this.groupBox4.Controls.Add(this.chbEnableLR);
            this.groupBox4.Location = new System.Drawing.Point(10, 163);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(449, 147);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Элементы Пельтье";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.BtnLRPoint2);
            this.groupBox6.Controls.Add(this.TxtbLRPwrPoint2);
            this.groupBox6.Controls.Add(this.label7);
            this.groupBox6.Controls.Add(this.TxtbLRTPoint2);
            this.groupBox6.Controls.Add(this.label8);
            this.groupBox6.Location = new System.Drawing.Point(281, 19);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(158, 116);
            this.groupBox6.TabIndex = 8;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Точка2";
            // 
            // BtnLRPoint2
            // 
            this.BtnLRPoint2.Image = global::PythonControl.Properties.Resources.ArrowRight;
            this.BtnLRPoint2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnLRPoint2.Location = new System.Drawing.Point(14, 87);
            this.BtnLRPoint2.Name = "BtnLRPoint2";
            this.BtnLRPoint2.Size = new System.Drawing.Size(127, 23);
            this.BtnLRPoint2.TabIndex = 4;
            this.BtnLRPoint2.Text = "Ok";
            this.BtnLRPoint2.UseVisualStyleBackColor = true;
            this.BtnLRPoint2.Click += new System.EventHandler(this.BtnLRPoint2_Click);
            // 
            // TxtbLRPwrPoint2
            // 
            this.TxtbLRPwrPoint2.Location = new System.Drawing.Point(92, 49);
            this.TxtbLRPwrPoint2.Name = "TxtbLRPwrPoint2";
            this.TxtbLRPwrPoint2.Size = new System.Drawing.Size(49, 20);
            this.TxtbLRPwrPoint2.TabIndex = 3;
            this.TxtbLRPwrPoint2.Text = "1500";
            this.TxtbLRPwrPoint2.TextChanged += new System.EventHandler(this.TxtbChecker);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 52);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Мощность:";
            // 
            // TxtbLRTPoint2
            // 
            this.TxtbLRTPoint2.Location = new System.Drawing.Point(92, 23);
            this.TxtbLRTPoint2.Name = "TxtbLRTPoint2";
            this.TxtbLRTPoint2.Size = new System.Drawing.Size(49, 20);
            this.TxtbLRTPoint2.TabIndex = 1;
            this.TxtbLRTPoint2.Text = "45";
            this.TxtbLRTPoint2.TextChanged += new System.EventHandler(this.TxtbChecker);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(11, 26);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "Температура:";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.BtnLRPoint1);
            this.groupBox5.Controls.Add(this.TxtbLRPwrPoint1);
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Controls.Add(this.TxtbLRTPoint1);
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Location = new System.Drawing.Point(117, 19);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(158, 116);
            this.groupBox5.TabIndex = 7;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Точка1";
            // 
            // BtnLRPoint1
            // 
            this.BtnLRPoint1.Image = global::PythonControl.Properties.Resources.ArrowRight;
            this.BtnLRPoint1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnLRPoint1.Location = new System.Drawing.Point(14, 87);
            this.BtnLRPoint1.Name = "BtnLRPoint1";
            this.BtnLRPoint1.Size = new System.Drawing.Size(127, 23);
            this.BtnLRPoint1.TabIndex = 4;
            this.BtnLRPoint1.Text = "Ok";
            this.BtnLRPoint1.UseVisualStyleBackColor = true;
            this.BtnLRPoint1.Click += new System.EventHandler(this.BtnLRPoint1_Click);
            // 
            // TxtbLRPwrPoint1
            // 
            this.TxtbLRPwrPoint1.Location = new System.Drawing.Point(92, 49);
            this.TxtbLRPwrPoint1.Name = "TxtbLRPwrPoint1";
            this.TxtbLRPwrPoint1.Size = new System.Drawing.Size(49, 20);
            this.TxtbLRPwrPoint1.TabIndex = 3;
            this.TxtbLRPwrPoint1.Text = "-1500";
            this.TxtbLRPwrPoint1.TextChanged += new System.EventHandler(this.TxtbChecker);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 52);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Мощность:";
            // 
            // TxtbLRTPoint1
            // 
            this.TxtbLRTPoint1.Location = new System.Drawing.Point(92, 23);
            this.TxtbLRTPoint1.Name = "TxtbLRTPoint1";
            this.TxtbLRTPoint1.Size = new System.Drawing.Size(49, 20);
            this.TxtbLRTPoint1.TabIndex = 1;
            this.TxtbLRTPoint1.Text = "20";
            this.TxtbLRTPoint1.TextChanged += new System.EventHandler(this.TxtbChecker);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Температура:";
            // 
            // chbEnableLR
            // 
            this.chbEnableLR.AutoSize = true;
            this.chbEnableLR.Location = new System.Drawing.Point(12, 44);
            this.chbEnableLR.Name = "chbEnableLR";
            this.chbEnableLR.Size = new System.Drawing.Size(75, 17);
            this.chbEnableLR.TabIndex = 6;
            this.chbEnableLR.Text = "Включить";
            this.chbEnableLR.UseVisualStyleBackColor = true;
            this.chbEnableLR.CheckedChanged += new System.EventHandler(this.ChbEnableLR_CheckedChanged);
            // 
            // gbLeds
            // 
            this.gbLeds.Controls.Add(this.groupBox8);
            this.gbLeds.Controls.Add(this.groupBox7);
            this.gbLeds.Controls.Add(this.chbEnableLeds);
            this.gbLeds.Controls.Add(this.groupBox3);
            this.gbLeds.Location = new System.Drawing.Point(10, 12);
            this.gbLeds.Name = "gbLeds";
            this.gbLeds.Size = new System.Drawing.Size(449, 145);
            this.gbLeds.TabIndex = 1;
            this.gbLeds.TabStop = false;
            this.gbLeds.Text = "Светодиоды";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.BtnLedPoint2);
            this.groupBox8.Controls.Add(this.TxtbLedPwrPoint2);
            this.groupBox8.Controls.Add(this.label11);
            this.groupBox8.Controls.Add(this.TxtbLedTPoint2);
            this.groupBox8.Controls.Add(this.label12);
            this.groupBox8.Location = new System.Drawing.Point(281, 18);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(158, 116);
            this.groupBox8.TabIndex = 9;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Точка2";
            // 
            // BtnLedPoint2
            // 
            this.BtnLedPoint2.Image = global::PythonControl.Properties.Resources.ArrowRight;
            this.BtnLedPoint2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnLedPoint2.Location = new System.Drawing.Point(14, 87);
            this.BtnLedPoint2.Name = "BtnLedPoint2";
            this.BtnLedPoint2.Size = new System.Drawing.Size(127, 23);
            this.BtnLedPoint2.TabIndex = 4;
            this.BtnLedPoint2.Text = "Ok";
            this.BtnLedPoint2.UseVisualStyleBackColor = true;
            this.BtnLedPoint2.Click += new System.EventHandler(this.BtnLedPoint2_Click);
            // 
            // TxtbLedPwrPoint2
            // 
            this.TxtbLedPwrPoint2.Location = new System.Drawing.Point(92, 49);
            this.TxtbLedPwrPoint2.Name = "TxtbLedPwrPoint2";
            this.TxtbLedPwrPoint2.Size = new System.Drawing.Size(49, 20);
            this.TxtbLedPwrPoint2.TabIndex = 3;
            this.TxtbLedPwrPoint2.Text = "100";
            this.TxtbLedPwrPoint2.TextChanged += new System.EventHandler(this.TxtbChecker);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(11, 52);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(67, 13);
            this.label11.TabIndex = 2;
            this.label11.Text = "Яркость, %:";
            // 
            // TxtbLedTPoint2
            // 
            this.TxtbLedTPoint2.Location = new System.Drawing.Point(92, 23);
            this.TxtbLedTPoint2.Name = "TxtbLedTPoint2";
            this.TxtbLedTPoint2.Size = new System.Drawing.Size(49, 20);
            this.TxtbLedTPoint2.TabIndex = 1;
            this.TxtbLedTPoint2.Text = "45";
            this.TxtbLedTPoint2.TextChanged += new System.EventHandler(this.TxtbChecker);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(11, 26);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(77, 13);
            this.label12.TabIndex = 0;
            this.label12.Text = "Температура:";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.BtnLedPoint1);
            this.groupBox7.Controls.Add(this.TxtbLedPwrPoint1);
            this.groupBox7.Controls.Add(this.label9);
            this.groupBox7.Controls.Add(this.TxtbLedTPoint1);
            this.groupBox7.Controls.Add(this.label10);
            this.groupBox7.Location = new System.Drawing.Point(117, 18);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(158, 116);
            this.groupBox7.TabIndex = 8;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Точка1";
            // 
            // BtnLedPoint1
            // 
            this.BtnLedPoint1.Image = global::PythonControl.Properties.Resources.ArrowRight;
            this.BtnLedPoint1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnLedPoint1.Location = new System.Drawing.Point(14, 87);
            this.BtnLedPoint1.Name = "BtnLedPoint1";
            this.BtnLedPoint1.Size = new System.Drawing.Size(127, 23);
            this.BtnLedPoint1.TabIndex = 4;
            this.BtnLedPoint1.Text = "Ok";
            this.BtnLedPoint1.UseVisualStyleBackColor = true;
            this.BtnLedPoint1.Click += new System.EventHandler(this.BtnLedPoint1_Click);
            // 
            // TxtbLedPwrPoint1
            // 
            this.TxtbLedPwrPoint1.Location = new System.Drawing.Point(92, 49);
            this.TxtbLedPwrPoint1.Name = "TxtbLedPwrPoint1";
            this.TxtbLedPwrPoint1.Size = new System.Drawing.Size(49, 20);
            this.TxtbLedPwrPoint1.TabIndex = 3;
            this.TxtbLedPwrPoint1.Text = "1";
            this.TxtbLedPwrPoint1.TextChanged += new System.EventHandler(this.TxtbChecker);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(11, 52);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(67, 13);
            this.label9.TabIndex = 2;
            this.label9.Text = "Яркость, %:";
            // 
            // TxtbLedTPoint1
            // 
            this.TxtbLedTPoint1.Location = new System.Drawing.Point(92, 23);
            this.TxtbLedTPoint1.Name = "TxtbLedTPoint1";
            this.TxtbLedTPoint1.Size = new System.Drawing.Size(49, 20);
            this.TxtbLedTPoint1.TabIndex = 1;
            this.TxtbLedTPoint1.Text = "18";
            this.TxtbLedTPoint1.TextChanged += new System.EventHandler(this.TxtbChecker);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(11, 26);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(77, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "Температура:";
            // 
            // chbEnableLeds
            // 
            this.chbEnableLeds.AutoSize = true;
            this.chbEnableLeds.Location = new System.Drawing.Point(12, 19);
            this.chbEnableLeds.Name = "chbEnableLeds";
            this.chbEnableLeds.Size = new System.Drawing.Size(75, 17);
            this.chbEnableLeds.TabIndex = 5;
            this.chbEnableLeds.Text = "Включить";
            this.chbEnableLeds.UseVisualStyleBackColor = true;
            this.chbEnableLeds.CheckedChanged += new System.EventHandler(this.ChbEnableLeds_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.BtnFreqLeds);
            this.groupBox3.Controls.Add(this.TxtbFreqLeds);
            this.groupBox3.Location = new System.Drawing.Point(6, 53);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(105, 81);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Частота";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "1...250";
            // 
            // BtnFreqLeds
            // 
            this.BtnFreqLeds.Image = global::PythonControl.Properties.Resources.ArrowRight;
            this.BtnFreqLeds.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnFreqLeds.Location = new System.Drawing.Point(6, 52);
            this.BtnFreqLeds.Name = "BtnFreqLeds";
            this.BtnFreqLeds.Size = new System.Drawing.Size(93, 23);
            this.BtnFreqLeds.TabIndex = 10;
            this.BtnFreqLeds.Text = "Ok";
            this.BtnFreqLeds.UseVisualStyleBackColor = true;
            this.BtnFreqLeds.Click += new System.EventHandler(this.BtnFreqLeds_Click);
            // 
            // TxtbFreqLeds
            // 
            this.TxtbFreqLeds.Location = new System.Drawing.Point(54, 19);
            this.TxtbFreqLeds.MaxLength = 36;
            this.TxtbFreqLeds.Name = "TxtbFreqLeds";
            this.TxtbFreqLeds.Size = new System.Drawing.Size(45, 20);
            this.TxtbFreqLeds.TabIndex = 8;
            this.TxtbFreqLeds.Text = "4";
            this.TxtbFreqLeds.TextChanged += new System.EventHandler(this.TxtbChecker);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(923, 397);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.gbChannelsLeds);
            this.Controls.Add(this.statusStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Управление индикацией";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.gbChannelsLeds.ResumeLayout(false);
            this.gbChannelsLeds.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.gbLeds.ResumeLayout(false);
            this.gbLeds.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel lblPythonReplying;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Timer timerHeartBeat;
        private System.Windows.Forms.GroupBox gbChannelsLeds;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.CheckBox cbCh8Leds;
        private System.Windows.Forms.CheckBox cbCh7Leds;
        private System.Windows.Forms.CheckBox cbCh6Leds;
        private System.Windows.Forms.CheckBox cbCh5Leds;
        private System.Windows.Forms.CheckBox cbCh4Leds;
        private System.Windows.Forms.CheckBox cbCh3Leds;
        private System.Windows.Forms.CheckBox cbCh2Leds;
        private System.Windows.Forms.CheckBox cbCh1Leds;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox gbLeds;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button BtnFreqLeds;
        private System.Windows.Forms.TextBox TxtbFreqLeds;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox TxtbLRTPoint1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chbEnableLR;
        private System.Windows.Forms.CheckBox chbEnableLeds;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button BtnLRPoint2;
        private System.Windows.Forms.TextBox TxtbLRPwrPoint2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox TxtbLRTPoint2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button BtnLRPoint1;
        private System.Windows.Forms.TextBox TxtbLRPwrPoint1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button BtnTLimits;
        private System.Windows.Forms.TextBox TxtbTLimitTop;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TxtbTLimitBottom;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Button BtnLedPoint2;
        private System.Windows.Forms.TextBox TxtbLedPwrPoint2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox TxtbLedTPoint2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Button BtnLedPoint1;
        private System.Windows.Forms.TextBox TxtbLedPwrPoint1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox TxtbLedTPoint1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label1;
    }
}

