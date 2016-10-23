using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms.DataVisualization.Charting;
using DataTools.DefaultData;
using DataTools.LocalData;

namespace DataAnalytics
{
    public static class DataAnalyze
    {
        public static int MaxCutomerCount;
        public static Customer[] Customers;

        public static void SetData(int count)
        {
            using (new OperationInfo($"Getting {count} customers"))
            {
                    for (int i = 0; i <LocalData.Transactions.Length; i++)
                    {
                        Transaction trans = LocalData.Transactions[i];
                        trans.Reset();
                         Customer customer = LocalData.Customers[trans.CustomerId];
                        if (customer.Transactions==null)
                            customer.Transactions = new List<Transaction>();
                        customer.Transactions.Add(trans);
                    }
                    Customers = LocalData.Customers.SubArray(0,count);
            }
        }
        public static void Initialize(Action<string,int> outputMethod,Action<string,int> reWriteLine)
        {

            DataWorker.LogWriteLine = outputMethod;
            DataWorker.LogReWriteLine = reWriteLine;
            OperationInfo.LogAction = DataWorker.LogWriteLine;
            ProgressCount.LogWriteLine = DataWorker.LogWriteLine;
            ProgressCount.LogReWriteLine = DataWorker.LogReWriteLine;
            using (new OperationInfo("Loading Local data"))
                LocalData.Initialize();
            MaxCutomerCount = LocalData.Customers.Length;
        }

        public static void DrawChart(Action<Series[]> drawChart)
        {
            using (new OperationInfo("Drawing chart count to amount", 1))
            {
                Series[] series = new[] {new Series("Man"), new Series("Woman")};
                var amountToCount = from m in Customers
                                    where m.Transactions.Count<2000 && m.Transactions.Sum(n => (long)(n.Amount > 0 ? n.Amount : 0))>-2000000000
                    select new
                    {
                        IsMan = m.IsMan,
                        Count = m.Transactions.Count,
                        Amount = m.Transactions.Sum(n =>  (long) (n.Amount>0?n.Amount:0))/m.Transactions.Count
                        
                    };
                foreach (var variable in amountToCount)
                {
                    if (variable.IsMan)
                        series[0].Points.AddXY(variable.Count, variable.Amount);
                    else
                        series[1].Points.AddXY(variable.Count, variable.Amount);
                }
                drawChart(series);
            }
        }
    }
}
