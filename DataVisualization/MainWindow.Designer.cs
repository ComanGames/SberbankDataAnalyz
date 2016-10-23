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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.infoBox = new MetroFramework.Controls.MetroTextBox();
            this.customerCountTextBox = new MetroFramework.Controls.MetroTextBox();
            this.customerCountLabel = new MetroFramework.Controls.MetroLabel();
            this.getCustomersButton = new MetroFramework.Controls.MetroButton();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.mainChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.metroComboBox1 = new MetroFramework.Controls.MetroComboBox();
            this.createChartButton = new MetroFramework.Controls.MetroButton();
            ((System.ComponentModel.ISupportInitialize)(this.mainChart)).BeginInit();
            this.SuspendLayout();
            // 
            // infoBox
            // 
            this.infoBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
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
            this.customerCountTextBox.Text = "0";
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
            chartArea2.Name = "ChartArea1";
            this.mainChart.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.mainChart.Legends.Add(legend2);
            this.mainChart.Location = new System.Drawing.Point(700, 33);
            this.mainChart.Name = "mainChart";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.mainChart.Series.Add(series2);
            this.mainChart.Size = new System.Drawing.Size(578, 386);
            this.mainChart.TabIndex = 5;
            this.mainChart.Text = "MainChart";
            // 
            // metroComboBox1
            // 
            this.metroComboBox1.FormattingEnabled = true;
            this.metroComboBox1.ItemHeight = 23;
            this.metroComboBox1.Location = new System.Drawing.Point(543, 33);
            this.metroComboBox1.Name = "metroComboBox1";
            this.metroComboBox1.Size = new System.Drawing.Size(151, 29);
            this.metroComboBox1.TabIndex = 6;
            // 
            // createChartButton
            // 
            this.createChartButton.Location = new System.Drawing.Point(543, 95);
            this.createChartButton.Name = "createChartButton";
            this.createChartButton.Size = new System.Drawing.Size(151, 47);
            this.createChartButton.TabIndex = 7;
            this.createChartButton.Text = "Create Chart";
            this.createChartButton.Click += new System.EventHandler(this.createChartButton_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1298, 688);
            this.Controls.Add(this.createChartButton);
            this.Controls.Add(this.metroComboBox1);
            this.Controls.Add(this.mainChart);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.getCustomersButton);
            this.Controls.Add(this.customerCountLabel);
            this.Controls.Add(this.customerCountTextBox);
            this.Controls.Add(this.infoBox);
            this.Name = "MainWindow";
            this.Text = "MainWindow";
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
        private MetroFramework.Controls.MetroComboBox metroComboBox1;
        private MetroFramework.Controls.MetroButton createChartButton;
    }
}

