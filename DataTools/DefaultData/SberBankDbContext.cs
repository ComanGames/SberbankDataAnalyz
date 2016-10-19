using System;
using System.Data.Entity;

namespace DataTools.DefaultData
{
    public class SberBankDbContext:DbContext
    {
        public SberBankDbContext()
            :base( Properties.Settings.Default.UseLocalDb?
             string.Format(Properties.Settings.Default.LocalDbSberBank, Environment.CurrentDirectory+@"\Materials\")
            :Properties.Settings.Default.SberBankCS)
        {

        }

        public DbSet<Customer> CustomersDbSet { get; set; }
        public DbSet<Transaction> TransactionsDbSet { get; set; }
        public DbSet<MccCode> MccCodesDbSet { get; set; }
        public DbSet<TransactionType> TransactionTypesDbSet { get; set; }
        public DbSet<GloabalData> GloabalDatasDbSet { get; set; }

    }
}