using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms.DataVisualization.Charting;
using DataTools.DefaultData;
using DataTools.LocalData;

namespace DataAnalytics
{
    public static class ChartTools
    {
        public static DataPoint[][] SumToCount()
        {
            List<DataPoint> mans = new List<DataPoint>();
            List<DataPoint> girls = new List<DataPoint>();
            var amountToCount = from m in DataAnalyze.Customers
                select new
                {
                    IsMan = m.IsMan,
                    Count = (from t in m.Transactions where t.Amount<0 select  t).Count(),
                    Amount = Math.Abs(m.Transactions.Sum(n => (long) (n.Amount < 0 ? n.Amount : 0)))
                };
            foreach (var variable in amountToCount)
            {
                if (variable.IsMan)
                    mans.Add(new DataPoint(variable.Count, variable.Amount));
                else
                    girls.Add(new DataPoint(variable.Count, variable.Amount));
            }
            return new[] {mans.ToArray(),girls.ToArray()};
        }

        public static DataPoint[][] SumToCountPerMonth()
        {
            List<DataPoint> mans = new List<DataPoint>();
            List<DataPoint> girls = new List<DataPoint>();
            var amountToCount = from m in DataAnalyze.Customers
                                select new
                                {
                                    IsMan = m.IsMan,
                                    CountOfMonth= ((m.Transactions.Max(t => t.Day) - m.Transactions.Min(t => t.Day))/30.0),
                                    Count = (from t in m.Transactions where t.Amount < 0 select t).Count(),
                                    Amount = Math.Abs(m.Transactions.Sum(n => (long)(n.Amount < 0 ? n.Amount : 0)))
                                };
            foreach (var v in amountToCount)
            {
                if (v.CountOfMonth > 1)
                {
                    if (v.IsMan)
                        mans.Add(new DataPoint(v.Count/v.CountOfMonth, v.Amount/v.CountOfMonth));

                    else
                        girls.Add(new DataPoint(v.Count/v.CountOfMonth, v.Amount/v.CountOfMonth));
                }
            }
            return new[] { mans.ToArray(), girls.ToArray() };

        }

        public static DataPoint[][] MoneySpendTotal()
        {
            double[] totalMan = (from c in DataAnalyze.Customers
                            where c.IsMan
                            select c.Transactions.Sum(n =>  (double)(n.Amount < 0 ?(double) n.Amount : 0))/10000000.0).ToArray();
            double[] totalWoman = (from c in DataAnalyze.Customers
                            where !c.IsMan
                            select c.Transactions.Sum(n =>  (double)(n.Amount < 0 ?(double) n.Amount : 0))/10000000.0).ToArray();
            double totalM = 0;
            double totalW = 0;
            foreach (int i in totalMan)
                totalM += Math.Abs((double) i);

            foreach (int i in totalWoman)
                totalW += Math.Abs((double) i);
            double total = totalM + totalW;
            totalM /= total;
            totalW /= total;

            return new[] { new [] {new DataPoint(1, (double) totalM)},new [] {new DataPoint(2,  (double)totalW)} };
        }

        public static DataPoint[][] SumToMcc()
        {
            Dictionary<byte,double> mccDictionaryM = new Dictionary<byte, double>();
            Dictionary<byte,double> mccDictionaryW = new Dictionary<byte, double>();
            foreach (Customer customer in LocalData.Customers)
            {
                if (customer.IsMan)
                {
                    foreach (Transaction transaction in customer.Transactions)
                        UdateDictionaryValue(mccDictionaryM, transaction,transaction.Amount,(x,y)=> x+y);

                }
                else
                {
                    
                    foreach (Transaction transaction in customer.Transactions)
                        UdateDictionaryValue(mccDictionaryW, transaction,transaction.Amount,(x,y)=> x+y);
                }

            }
            DataPoint[] mans = (from m in mccDictionaryM select new DataPoint(m.Key, m.Value)).ToArray();
            DataPoint[] girls = (from m in mccDictionaryW select new DataPoint(m.Key, m.Value)).ToArray();

            return new[] {mans, girls};
        }

        private static void UdateDictionaryValue<T>(Dictionary<byte, T> mccDictionaryM, Transaction transaction, T toAdd,Func<T,T,T> sum) 
        {
            if (!mccDictionaryM.ContainsKey(transaction.MccCodeId))
                mccDictionaryM.Add(transaction.MccCodeId, toAdd);
            else
                mccDictionaryM[transaction.MccCodeId]= sum(mccDictionaryM[transaction.MccCodeId],toAdd);
        }

        public static DataPoint[][] TransactionCountToMcc()
        {
            Dictionary<byte, int> mccDictionaryM = new Dictionary<byte, int>();
            Dictionary<byte, int> mccDictionaryW = new Dictionary<byte, int>();
            foreach (Customer customer in LocalData.Customers)
            {
                if (customer.IsMan)
                {
                    foreach (Transaction transaction in customer.Transactions)
                        UdateDictionaryValue(mccDictionaryM, transaction,1,(x,y)=> x+y);

                }
                else
                {

                    foreach (Transaction transaction in customer.Transactions)
                        UdateDictionaryValue(mccDictionaryW, transaction,1,(x,y)=>  x+y);
                }

            }
            DataPoint[] mans = (from m in mccDictionaryM select new DataPoint(m.Key, m.Value)).ToArray();
            DataPoint[] girls = (from m in mccDictionaryW select new DataPoint(m.Key, m.Value)).ToArray();

            return new[] { mans, girls };

        }
    }
}