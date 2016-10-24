using System;
using System.Collections.Generic;
using System.Drawing;
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

            using (new OperationInfo("Mapping transactions to customers"))
            {
                for (int i = 0; i < LocalData.Transactions.Length; i++)
                {
                    Transaction trans = LocalData.Transactions[i];
                    trans.Reset();
                    Customer customer = LocalData.Customers[trans.CustomerId];
                    if (customer.Transactions == null)
                        customer.Transactions = new List<Transaction>();
                    customer.Transactions.Add(trans);
                }
            }

            MaxCutomerCount = LocalData.Customers.Length;
        }

        public static void DrawChart(Action<Series[]> drawChart,TypeOfChart type,int dataToAdd, ChartLimit limits)
        {

            if (Customers == null)
              return;  
            using (new OperationInfo("Drawing chart count to amount", 1))
            {
                Series[] series;
                series = new[] {new Series("Man"), new Series("Woman")};
                series[1].Color = Color.FromArgb(255, 63, 63);
                DataPoint[][] manWomanPoints=new DataPoint[0][];

                switch (type)
                {
                    case TypeOfChart.TransactionCountToSum:
                        manWomanPoints = ChartTools.SumToCount();
                    break;
                    case TypeOfChart.SumToCountPerMonth:
                        manWomanPoints = ChartTools.SumToCountPerMonth();
                    break;
                    case TypeOfChart.MoneySpendTotal:
                        manWomanPoints = ChartTools.MoneySpendTotal();
                    break;
                    case TypeOfChart.SumToMcc:
                        manWomanPoints = ChartTools.SumToMcc();
                    break;
                    case TypeOfChart.TransactionCountToMcc:
                        manWomanPoints = ChartTools.TransactionCountToMcc();
                    break;
                }
                series = SetLimits(limits, manWomanPoints, series);
                OperationInfo.LogAction($"Count Man ={series[0].Points.Count}, Woman ={series[1].Points.Count}", 1);

                if (dataToAdd == 0)
                    series= new [] {series[0]};
                else if (dataToAdd == 1)
                    series= new [] {series[1]};


                drawChart(series);
            }
        }

        private static Series[] SetLimits(ChartLimit limits, DataPoint[][] manWomanPoints, Series[] series)
        {
            DataPoint[] points = manWomanPoints[0].Union(manWomanPoints[1]).ToArray();

            double realMaxX;
            double realMaxY;
            double realMinX;
            double realMinY;

            if (limits.IsProcent)
            {

               
                realMaxX = (points.Max(p => p.XValue))*(limits.MaxPoint.XValue/99.0);
                realMaxY = (points.Max(p => p.YValues.Max()))*(limits.MaxPoint.YValues.Max()/99.0);
                realMinX = (points.Max(p => p.XValue))*(limits.MinPoint.XValue/99.0);
                realMinY = (points.Max(p => p.YValues.Max()))*(limits.MinPoint.YValues.Min()/99.0);

            }
            else
            {

               realMaxX = limits.MaxPoint.XValue;
               realMaxY = limits.MaxPoint.YValues.Max();
               realMinX = limits.MinPoint.XValue;
               realMinY = limits.MinPoint.YValues.Min();

            }

            ProgressCount.LogWriteLine($"realMaxX={realMaxX},realMaxY={realMaxY}", 0);
            ProgressCount.LogWriteLine($"realMinX={realMinX},realMinY={realMinY}", 0);
            for (int i = 0; i < series.Length; i++)
            {
                var cleanPoints = from p in manWomanPoints[i]
                    where p.XValue <= realMaxX && p.YValues.Max() <= realMaxY
                    && p.XValue > realMinX && p.YValues.Min() > realMinY
                    select p;
                foreach (DataPoint cleanPoint in cleanPoints)
                    series[i].Points.Add(cleanPoint);
            }
            return series;
        }
    }
}
