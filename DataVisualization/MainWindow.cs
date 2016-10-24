using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using DataAnalytics;
using MetroFramework;
using MetroFramework.Controls;
using MetroFramework.Forms;

namespace DataVisualization
{
    public partial class MainWindow : MetroForm
    {
        public int MaxTextBoxLinesCount = 100;
        private static List<string> _infoBoxText;
        public static SynchronizationContext SynchronizationContextWindow;
        public MainWindow()
        {
            InitializeComponent();
            foreach (string typeOfChart in Enum.GetNames(typeof(TypeOfChart)))
                chartDataComboBox.Items.Add(typeOfChart);

            foreach (string typeOfSeries in Enum.GetNames(typeof(SeriesChartType)))
                chartTypeComboBox.Items.Add(typeOfSeries);

            chartManOrWomanComboBox.Items.Add("Man");
            chartManOrWomanComboBox.Items.Add("Women");
            chartManOrWomanComboBox.Items.Add("Man And Woman");
            chartDataComboBox.SelectedIndex = 0;
            chartTypeComboBox.SelectedIndex = 0;
            chartManOrWomanComboBox.SelectedIndex = 2;
            SynchronizationContextWindow = SynchronizationContext.Current;
            _infoBoxText = new List<string>();
            Task.Factory.StartNew(() =>
            {
                 Task.Delay(100);
                DataAnalyze.Initialize(AddInfoLine, RewriteLine);
                EnableCustomerCountChose();
                DataAnalyze.SetData(12000);
            });

        }
   
        public void DrawChart(Series[] series)
        {
            SynchronizationContextWindow.Post((sender) =>
                {

                    mainChart.Series.Clear();
                    SeriesChartType type = (SeriesChartType)chartTypeComboBox.SelectedIndex;
                    for (int i = 0; i < series.Length; i++)
                    {
                        mainChart.Series.Add(series[i]);
                        mainChart.Series[i].ChartType = type;
                    }
                }
                , null);
        }
        private void EnableCustomerCountChose()
        {
            SynchronizationContextWindow.Post((sender) =>
            {
                getCustomersButton.Enabled = true;
                customerCountLabel.Text = @"/ " + DataAnalyze.MaxCutomerCount;
            }, null);

        }

        public void RewriteLine(string text,int tabs)
        {

            RemoveInfoLine();
            AddInfoLine(text,tabs);
            
        }

        public void RemoveInfoLine()
        {
            if(_infoBoxText.Count>0)
                _infoBoxText.RemoveAt(0);
        }

        public void AddInfoLine(string text, int tabs)
        {
            if (_infoBoxText.Count > MaxTextBoxLinesCount)
                _infoBoxText.RemoveAt(_infoBoxText.Count - 1);

            _infoBoxText.Insert(0, new string('\t', tabs) + text);
            WriteTextToInfoBox();
        }

        private void WriteTextToInfoBox()
        {
            SynchronizationContextWindow.Post((sender) =>
            {
                string[] texts = _infoBoxText.ToArray();
                    infoBox.Text = string.Join("\r\n", texts);
            }, null);
        }

        private void getCustomersButton_Click(object sender, EventArgs e)
        {
            int count;
            string text = customerCountTextBox.Text;
            if (!int.TryParse(text, out count))
            {
                ShowMessage($"Can't convert {text} into int");
                return;
            }
            if (count <= 0 || count > DataAnalyze.MaxCutomerCount)
            {
                
                ShowMessage($"Number is out of range {text} ");
                return;
            }

            Task.Factory.StartNew(() => { DataAnalyze.SetData(count);});

        }

        public void ShowMessage(string text)
        {
            MetroMessageBox mb = new MetroMessageBox
            {
                Text = text,
                Style = MetroColorStyle.Red,
                Size = new Size(500, 150)
            };
            mb.Show();
        }

        private void createChartButton_Click(object sender, EventArgs e)
        {
            TypeOfChart type = (TypeOfChart) chartDataComboBox.SelectedIndex;

//            maxX =1.0/ Math.Pow((DoubleFromTextBox(chartMaxXTextBox) / 100.0),-3);
            double maxX =DoubleFromTextBox(chartMaxXTextBox);
            double maxY =DoubleFromTextBox(chartMaxYTextBox);

            double minX =DoubleFromTextBox(chartMinXTextBox);
            double minY =DoubleFromTextBox(chartMinYTextBox);

            ChartLimit limits= new ChartLimit(isProcentCheckBox.Checked,new DataPoint(maxX,maxY),new DataPoint(minX,minY));
            int dataToAdd = chartManOrWomanComboBox.SelectedIndex;
          Task.Factory.StartNew(() =>
          {
              DataAnalyze.DrawChart(DrawChart,type,dataToAdd,limits);
          });
        }

        private double DoubleFromTextBox(MetroTextBox metroTextBox)
        {
            double result = 0;
            if (!double.TryParse(metroTextBox.Text, out result))
                ShowMessage($"Can't convert {metroTextBox.Text} into double");
            return result;
        }

        private void metroLabel6_Click(object sender, EventArgs e)
        {

        }

        private void MainWindow_Load(object sender, EventArgs e)
        {

        }

    }
}
