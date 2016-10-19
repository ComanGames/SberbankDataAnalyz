using System.Collections.Generic;
using System.Linq;

namespace DataTools.DefaultData
{
    public class Customer
    {
        public int Id { get; set; }
        public int BankId { get; set; } 
        public bool IsMan { get; set; }
        public bool GenderKnown { get; set; }
        public  List<Transaction> Transactions { get; set; }
    }
}
