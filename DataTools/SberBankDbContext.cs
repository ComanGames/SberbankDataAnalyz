using System.Data.Entity;

namespace DataTools
{
    public class SberBankDbContext:DbContext
    {
        public SberBankDbContext():base(@"Data Source=sber-bank.database.windows.net;Initial Catalog=SberBankDb;Integrated Security=False;User ID=Yura;Password=Train2brain4;Connect Timeout=15;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
        {
        }

        public DbSet<Customer> CustomersDbSet { get; set; }
        public DbSet<Transaction> TransactionsDbSet { get; set; }
        public DbSet<MccCode> MccCodesDbSet { get; set; }
        public DbSet<TransactionType> TransactionTypesDbSet { get; set; }
        public DbSet<GloabalData> GloabalDatasDbSet { get; set; }

    }
}