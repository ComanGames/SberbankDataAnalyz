using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DataTools.DefaultData;
using DataTools.LocalData;

namespace DataAnalyze
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LocalData.Initilize();
            SetInfoAboutData();
        }

        private void SetInfoAboutData()
        {
            using (var context = new SberBankDbContext())
            {
                var sql = "SELECT COUNT(*) FROM Transactions";
                var total = context.Database.SqlQuery<int>(sql).Single();
//                IsDbSet.Content = $"Your current data amount is:{total}";
            }
        }
    }
}
