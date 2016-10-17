using System;

namespace DataTools
{
    [Serializable]
    public class Transaction
    {
        public int Id { get; set; }
        public int BankId { get; set; }
        public int TimeDay { get; set; }
        public TimeSpan TimeHours { get; set; }
        public int MccCode { get; set; }
        public int TransactionType { get; set; }
        public double Amount { get; set; }
        public string TermId { get; set; }
}

}