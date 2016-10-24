using System.Windows.Forms.DataVisualization.Charting;

namespace DataAnalytics
{
    public class ChartLimit
    {
        public bool IsProcent;
        public DataPoint MaxPoint ;
        public DataPoint MinPoint ;

        public ChartLimit()
        {
        }

        public ChartLimit(bool isProcent, DataPoint maxPoint, DataPoint minPoint)
        {
            IsProcent = isProcent;
            MaxPoint = maxPoint;
            MinPoint = minPoint;
        }
    }
}