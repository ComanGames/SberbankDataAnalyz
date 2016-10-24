namespace DataVisualization
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.infoBox = new MetroFramework.Controls.MetroTextBox();
            this.customerCountTextBox = new MetroFramework.Controls.MetroTextBox();
            this.customerCountLabel = new MetroFramework.Controls.MetroLabel();
            this.getCustomersButton = new MetroFramework.Controls.MetroButton();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.mainChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartDataComboBox = new MetroFramework.Controls.MetroComboBox();
            this.createChartButton = new MetroFramework.Controls.MetroButton();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel4 = new MetroFramework.Controls.MetroLabel();
            this.chartTypeComboBox = new MetroFramework.Controls.MetroComboBox();
            this.metroLabel5 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel6 = new MetroFramework.Controls.MetroLabel();
            this.chartManOrWomanComboBox = new MetroFramework.Controls.MetroComboBox();
            this.metroLabel7 = new MetroFramework.Controls.MetroLabel();
            this.chartMaxXTextBox = new MetroFramework.Controls.MetroTextBox();
            this.chartMinXTextBox = new MetroFramework.Controls.MetroTextBox();
            this.chartMinYTextBox = new MetroFramework.Controls.MetroTextBox();
            this.chartMaxYTextBox = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel8 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel9 = new MetroFramework.Controls.MetroLabel();
            this.isProcentCheckBox = new MetroFramework.Controls.MetroCheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.mainChart)).BeginInit();
            this.SuspendLayout();
            // 
            // infoBox
            // 
            this.infoBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.infoBox.Location = new System.Drawing.Point(-2, 462);
            this.infoBox.Margin = new System.Windows.Forms.Padding(0);
            this.infoBox.Multiline = true;
            this.infoBox.Name = "infoBox";
            this.infoBox.ReadOnly = true;
            this.infoBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.infoBox.Size = new System.Drawing.Size(1280, 222);
            this.infoBox.TabIndex = 0;
            this.infoBox.Text = "Start";
            // 
            // customerCountTextBox
            // 
            this.customerCountTextBox.Location = new System.Drawing.Point(59, 96);
            this.customerCountTextBox.MaxLength = 6;
            this.customerCountTextBox.Name = "customerCountTextBox";
            this.customerCountTextBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.customerCountTextBox.Size = new System.Drawing.Size(73, 23);
            this.customerCountTextBox.TabIndex = 1;
            this.customerCountTextBox.Text = "12000";
            // 
            // customerCountLabel
            // 
            this.customerCountLabel.AutoSize = true;
            this.customerCountLabel.Location = new System.Drawing.Point(138, 96);
            this.customerCountLabel.Name = "customerCountLabel";
            this.customerCountLabel.Size = new System.Drawing.Size(25, 19);
            this.customerCountLabel.TabIndex = 2;
            this.customerCountLabel.Text = "/ 0";
            // 
            // getCustomersButton
            // 
            this.getCustomersButton.Enabled = false;
            this.getCustomersButton.Location = new System.Drawing.Point(9, 125);
            this.getCustomersButton.Name = "getCustomersButton";
            this.getCustomersButton.Size = new System.Drawing.Size(173, 39);
            this.getCustomersButton.TabIndex = 3;
            this.getCustomersButton.Text = "Get customes";
            this.getCustomersButton.Click += new System.EventHandler(this.getCustomersButton_Click);
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(9, 96);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(44, 19);
            this.metroLabel2.TabIndex = 4;
            this.metroLabel2.Text = "Count";
            // 
            // mainChart
            // 
            this.mainChart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.Name = "ChartArea1";
            this.mainChart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.mainChart.Legends.Add(legend1);
            this.mainChart.Location = new System.Drawing.Point(700, 33);
            this.mainChart.Name = "mainChart";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.mainChart.Series.Add(series1);
            this.mainChart.Size = new System.Drawing.Size(575, 414);
            this.mainChart.TabIndex = 5;
            this.mainChart.Text = "MainChart";
            // 
            // chartDataComboBox
            // 
            this.chartDataComboBox.FormattingEnabled = true;
            this.chartDataComboBox.ItemHeight = 23;
            this.chartDataComboBox.Location = new System.Drawing.Point(440, 33);
            this.chartDataComboBox.Name = "chartDataComboBox";
            this.chartDataComboBox.Size = new System.Drawing.Size(254, 29);
            this.chartDataComboBox.TabIndex = 6;
            // 
            // createChartButton
            // 
            this.createChartButton.Location = new System.Drawing.Point(440, 304);
            this.createChartButton.Name = "createChartButton";
            this.createChartButton.Size = new System.Drawing.Size(254, 47);
            this.createChartButton.TabIndex = 7;
            this.createChartButton.Text = "Create Chart";
            this.createChartButton.Click += new System.EventHandler(this.createChartButton_Click);
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(40, 60);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(103, 19);
            this.metroLabel1.TabIndex = 8;
            this.metroLabel1.Text = "Customer Setup";
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.Location = new System.Drawing.Point(351, 43);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(83, 19);
            this.metroLabel3.TabIndex = 9;
            this.metroLabel3.Text = "Type of Data";
            // 
            // metroLabel4
            // 
            this.metroLabel4.AutoSize = true;
            this.metroLabel4.Location = new System.Drawing.Point(345, 78);
            this.metroLabel4.Name = "metroLabel4";
            this.metroLabel4.Size = new System.Drawing.Size(89, 19);
            this.metroLabel4.TabIndex = 10;
            this.metroLabel4.Text = "Type of Chart";
            // 
            // chartTypeComboBox
            // 
            this.chartTypeComboBox.FormattingEnabled = true;
            this.chartTypeComboBox.ItemHeight = 23;
            this.chartTypeComboBox.Location = new System.Drawing.Point(440, 68);
            this.chartTypeComboBox.Name = "chartTypeComboBox";
            this.chartTypeComboBox.Size = new System.Drawing.Size(254, 29);
            this.chartTypeComboBox.TabIndex = 11;
            // 
            // metroLabel5
            // 
            this.metroLabel5.AutoSize = true;
            this.metroLabel5.Location = new System.Drawing.Point(388, 178);
            this.metroLabel5.Name = "metroLabel5";
            this.metroLabel5.Size = new System.Drawing.Size(46, 19);
            this.metroLabel5.TabIndex = 13;
            this.metroLabel5.Text = "Max X";
            // 
            // metroLabel6
            // 
            this.metroLabel6.AutoSize = true;
            this.metroLabel6.Location = new System.Drawing.Point(388, 207);
            this.metroLabel6.Name = "metroLabel6";
            this.metroLabel6.Size = new System.Drawing.Size(43, 19);
            this.metroLabel6.TabIndex = 15;
            this.metroLabel6.Text = "Min X";
            this.metroLabel6.Click += new System.EventHandler(this.metroLabel6_Click);
            // 
            // chartManOrWomanComboBox
            // 
            this.chartManOrWomanComboBox.FormattingEnabled = true;
            this.chartManOrWomanComboBox.ItemHeight = 23;
            this.chartManOrWomanComboBox.Location = new System.Drawing.Point(440, 103);
            this.chartManOrWomanComboBox.Name = "chartManOrWomanComboBox";
            this.chartManOrWomanComboBox.Size = new System.Drawing.Size(254, 29);
            this.chartManOrWomanComboBox.TabIndex = 17;
            // 
            // metroLabel7
            // 
            this.metroLabel7.AutoSize = true;
            this.metroLabel7.Location = new System.Drawing.Point(332, 113);
            this.metroLabel7.Name = "metroLabel7";
            this.metroLabel7.Size = new System.Drawing.Size(102, 19);
            this.metroLabel7.TabIndex = 16;
            this.metroLabel7.Text = "Man or Woman";
            // 
            // chartMaxXTextBox
            // 
            this.chartMaxXTextBox.Location = new System.Drawing.Point(440, 178);
            this.chartMaxXTextBox.MaxLength = 17;
            this.chartMaxXTextBox.Name = "chartMaxXTextBox";
            this.chartMaxXTextBox.Size = new System.Drawing.Size(254, 23);
            this.chartMaxXTextBox.TabIndex = 18;
            this.chartMaxXTextBox.Text = "100";
            // 
            // chartMinXTextBox
            // 
            this.chartMinXTextBox.Location = new System.Drawing.Point(440, 207);
            this.chartMinXTextBox.MaxLength = 17;
            this.chartMinXTextBox.Name = "chartMinXTextBox";
            this.chartMinXTextBox.Size = new System.Drawing.Size(254, 23);
            this.chartMinXTextBox.TabIndex = 19;
            this.chartMinXTextBox.Text = "0.0";
            // 
            // chartMinYTextBox
            // 
            this.chartMinYTextBox.Location = new System.Drawing.Point(440, 265);
            this.chartMinYTextBox.MaxLength = 17;
            this.chartMinYTextBox.Name = "chartMinYTextBox";
            this.chartMinYTextBox.Size = new System.Drawing.Size(254, 23);
            this.chartMinYTextBox.TabIndex = 23;
            this.chartMinYTextBox.Text = "0.0";
            // 
            // chartMaxYTextBox
            // 
            this.chartMaxYTextBox.Location = new System.Drawing.Point(440, 236);
            this.chartMaxYTextBox.MaxLength = 17;
            this.chartMaxYTextBox.Name = "chartMaxYTextBox";
            this.chartMaxYTextBox.Size = new System.Drawing.Size(254, 23);
            this.chartMaxYTextBox.TabIndex = 22;
            this.chartMaxYTextBox.Text = "100";
            // 
            // metroLabel8
            // 
            this.metroLabel8.AutoSize = true;
            this.metroLabel8.Location = new System.Drawing.Point(388, 265);
            this.metroLabel8.Name = "metroLabel8";
            this.metroLabel8.Size = new System.Drawing.Size(43, 19);
            this.metroLabel8.TabIndex = 21;
            this.metroLabel8.Text = "Min Y";
            // 
            // metroLabel9
            // 
            this.metroLabel9.AutoSize = true;
            this.metroLabel9.Location = new System.Drawing.Point(388, 236);
            this.metroLabel9.Name = "metroLabel9";
            this.metroLabel9.Size = new System.Drawing.Size(46, 19);
            this.metroLabel9.TabIndex = 20;
            this.metroLabel9.Text = "Max Y";
            // 
            // isProcentCheckBox
            // 
            this.isProcentCheckBox.AutoSize = true;
            this.isProcentCheckBox.Checked = true;
            this.isProcentCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.isProcentCheckBox.Location = new System.Drawing.Point(440, 157);
            this.isProcentCheckBox.Name = "isProcentCheckBox";
            this.isProcentCheckBox.Size = new System.Drawing.Size(98, 15);
            this.isProcentCheckBox.TabIndex = 25;
            this.isProcentCheckBox.Text = "Is Procentrage";
            this.isProcentCheckBox.UseVisualStyleBackColor = true;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1298, 688);
            this.Controls.Add(this.isProcentCheckBox);
            this.Controls.Add(this.chartMinYTextBox);
            this.Controls.Add(this.chartMaxYTextBox);
            this.Controls.Add(this.metroLabel8);
            this.Controls.Add(this.metroLabel9);
            this.Controls.Add(this.chartMinXTextBox);
            this.Controls.Add(this.chartMaxXTextBox);
            this.Controls.Add(this.chartManOrWomanComboBox);
            this.Controls.Add(this.metroLabel7);
            this.Controls.Add(this.metroLabel6);
            this.Controls.Add(this.metroLabel5);
            this.Controls.Add(this.chartTypeComboBox);
            this.Controls.Add(this.metroLabel4);
            this.Controls.Add(this.metroLabel3);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.createChartButton);
            this.Controls.Add(this.chartDataComboBox);
            this.Controls.Add(this.mainChart);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.getCustomersButton);
            this.Controls.Add(this.customerCountLabel);
            this.Controls.Add(this.customerCountTextBox);
            this.Controls.Add(this.infoBox);
            this.Name = "MainWindow";
            this.Text = "MainWindow";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.mainChart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroTextBox infoBox;
        private MetroFramework.Controls.MetroTextBox customerCountTextBox;
        private MetroFramework.Controls.MetroLabel customerCountLabel;
        private MetroFramework.Controls.MetroButton getCustomersButton;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private System.Windows.Forms.DataVisualization.Charting.Chart mainChart;
        private MetroFramework.Controls.MetroComboBox chartDataComboBox;
        private MetroFramework.Controls.MetroButton createChartButton;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroLabel metroLabel4;
        private MetroFramework.Controls.MetroComboBox chartTypeComboBox;
        private MetroFramework.Controls.MetroLabel metroLabel5;
        private MetroFramework.Controls.MetroLabel metroLabel6;
        private MetroFramework.Controls.MetroComboBox chartManOrWomanComboBox;
        private MetroFramework.Controls.MetroLabel metroLabel7;
        private MetroFramework.Controls.MetroTextBox chartMaxXTextBox;
        private MetroFramework.Controls.MetroTextBox chartMinXTextBox;
        private MetroFramework.Controls.MetroTextBox chartMinYTextBox;
        private MetroFramework.Controls.MetroTextBox chartMaxYTextBox;
        private MetroFramework.Controls.MetroLabel metroLabel8;
        private MetroFramework.Controls.MetroLabel metroLabel9;
        private MetroFramework.Controls.MetroCheckBox isProcentCheckBox;
    }
}

