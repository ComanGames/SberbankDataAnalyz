using System.Data.Entity;

namespace DataTools
{
    public class SberBankDbContext:DbContext
    {
        public SberBankDbContext():base(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='C:\Users\coman\OneDrive\Documents\Visual Studio 2015\Projects\SberBankDataAnalyze\DataTools\Data\SberBankData.mdf';Integrated Security=True")
        {
        }

        public DbSet<Customer> CustomersDbSet { get; set; }
        public DbSet<Transaction> TransactionsDbSet { get; set; }
        public DbSet<MccCode> MccCodesDbSet { get; set; }
        public DbSet<TransactionType> TransactionTypesDbSet { get; set; }
        public DbSet<GloabalData> GloabalDatasDbSet { get; set; }

    }
}