using System;

namespace DataTools
{
    public class Transaction
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime DateTimeTr { get; set; }
        public int MccCode { get; set; }
        public int TransactionType { get; set; }
        public double Amount { get; set; }
        public string TermId { get; set; }
        public bool GenderKnown { get; set; }
}

}