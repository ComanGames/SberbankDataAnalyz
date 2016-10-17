using System.Data.Entity;

namespace DataTools.DefaultData
{
    public class SberBankDbContext:DbContext
    {
        public SberBankDbContext():base(Properties.Settings.Default.SberBankCS)
        {

        }

        public DbSet<Customer> CustomersDbSet { get; set; }
        public DbSet<Transaction> TransactionsDbSet { get; set; }
        public DbSet<MccCode> MccCodesDbSet { get; set; }
        public DbSet<TransactionType> TransactionTypesDbSet { get; set; }
        public DbSet<GloabalData> GloabalDatasDbSet { get; set; }

    }
}