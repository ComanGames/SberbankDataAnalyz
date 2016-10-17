using System.Data.Entity;

namespace DataTools.DataPrediction
{
    public class SberBankPrediction :  DbContext
    {
        protected SberBankPrediction():base(Properties.Settings.Default.SberBankPreditionModel)
        {
        }
        
    }
}