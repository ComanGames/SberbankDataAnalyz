using System.Data.Entity;
using System.Linq;
using DataTools.DefaultData;
using EntityFramework.BulkInsert.Extensions;

namespace DataTools.LocalData
{
    public static class DataBaseUtil
    {
        public const int StpsForBigDb = 10240;

        public static void CleanDb()
        {
            using (SberBankDbContext context = new SberBankDbContext())
            {
                using (new OperationInfo("Removing Transactions", 1))
                    context.TransactionsDbSet.Create();

                using (new OperationInfo("Removing Customers", 1))
                    context.CustomersDbSet.Create();

                using (new OperationInfo("Removing Mcc", 1))
                    context.MccCodesDbSet.Create();

                using (new OperationInfo("Removing Transactions types", 1))
                    context.TransactionTypesDbSet.Create();
                context.SaveChanges();

            }
        }

        public static void BullingToDb<T>(T[] transactions, int startPoint=0, int itemsToDrop=0)
        {
            using (SberBankDbContext context = new SberBankDbContext())
            {
                context.Configuration.AutoDetectChangesEnabled = false;
                context.Configuration.ValidateOnSaveEnabled = false;

                context.BulkInsert(itemsToDrop != 0 ? transactions.SubArray(startPoint, itemsToDrop) : transactions);

            }
        }

        public static void AddToDb(MccCode[] mccCodes, TransactionType[] transactionTypes, Customer[] customers,
            Transaction[] transactions)
        {
            using (new OperationInfo("Add mccCodes table to Db", 1))
                BullingToDb(mccCodes);

            using (new OperationInfo("Add Transaction Type table to Db", 1))
                BullingToDb(transactionTypes);

            using (new OperationInfo("Add customers with Gender table to Db", 1))
                BullingToDb(customers);

            using (new OperationInfo("Add Transactions", 1))
            {
                int stepNuber = 0;
                using (ProgressCount progress = new ProgressCount((transactions.Length/StpsForBigDb)+1))
                {

                    while (stepNuber < transactions.Length)
                    {
                        int count = (stepNuber + StpsForBigDb - 1 < transactions.Length)
                            ? StpsForBigDb - 1
                            : transactions.Length - stepNuber;
                        BullingToDb(transactions.SubArray(stepNuber, count));
                        progress.Update();
                        stepNuber += StpsForBigDb;
                    }
                }
            }

        }

        public static void TestDbSpeed()
        {
            OperationInfo.LogAction = DataWorker.LogWriteLine;

            using (new OperationInfo("Testing db speed", 0))
            {
                ReadMccFromDbTest();
            }
        }

        public static void ReadMccFromDbTest()
        {
            using (new OperationInfo("Test getting mcc code from db",1))
            {
                using (SberBankDbContext context = new SberBankDbContext())
                {
                    // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
                    context.MccCodesDbSet.ToArray();
                }
            }
        }

        public static void CreateDb()
        {
            using (new OperationInfo("Creating Db and removing old",0))
                Database.SetInitializer(new DropCreateDatabaseAlways<SberBankDbContext>());
        }
    }
}