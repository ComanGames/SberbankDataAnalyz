namespace DataTools
{
    public class Customer
    {
        public int Id { get; set; }
        public int BankId { get; set; } 
        public float Gender { get; set; }
        public bool GenderKnowne { get; set; }
        public Transaction[] Transactions { get; set; }
    }
}
