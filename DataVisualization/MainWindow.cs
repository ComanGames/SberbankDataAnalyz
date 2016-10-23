using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using DataAnalytics;
using MetroFramework;
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
            SynchronizationContextWindow = SynchronizationContext.Current;
            _infoBoxText = new List<string>();
            Task.Factory.StartNew(() =>
            {
                 Task.Delay(100);
                DataAnalyze.Initialize(AddInfoLine, RewriteLine);
                EnableCustomerCountChose();
            });

        }

        public void DrawChart(Series[] series)
        {
            SynchronizationContextWindow.Post((sender) =>
                {

                    mainChart.Series.Clear();
                    for (int i = 0; i < series.Length; i++)
                    {
                        mainChart.Series.Add(series[i]);
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

        private void getCustomersButton_Click(object sender, System.EventArgs e)
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
            
                MetroMessageBox mb = new MetroMessageBox();
                mb.Text = text;
                mb.Style = MetroColorStyle.Red;
                mb.Size = new Size(500,150);
                mb.Show();
        }

        private void createChartButton_Click(object sender, System.EventArgs e)
        {
          Task.Factory.StartNew(()=>DataAnalyze.DrawChart(DrawChart));
        }
    }
}
