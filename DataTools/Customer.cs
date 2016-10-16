using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTools
{
    public class Customer
    {
        public int Id { get; set; }
        public float Gender { get; set; }
        public Transaction[] Transactions { get; set; }
    }
}
