using System;

namespace DataTools.DefaultData
{
    [Serializable]
    public class Transaction
    {
        public int Id { get; set; }
        public int BankId { get; set; }
        public short TimeDay { get; set; }
        public int TimeSeconds { get; set; }
        public short MccCode { get; set; }
        public short TransactionType { get; set; }
        public float Amount { get; set; }
        public string TermId { get; set; }
}

}