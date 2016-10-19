using System.Collections.Generic;

namespace DataTools.DefaultData
{
    public class Customer
    {
        public int Id { get; set; }
        public int BankId { get; set; } 
        public bool IsMan { get; set; }
        public bool GenderKnown { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
