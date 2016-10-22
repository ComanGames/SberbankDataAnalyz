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
            using (new OperationInfo("Cleaning db..."))
            {
                using (SberBankDbContext context = new SberBankDbContext())
                {
                    context.Database.ExecuteSqlCommand("TRUNCATE TABLE Transactions");
                    context.Database.ExecuteSqlCommand("TRUNCATE TABLE MccCodes");
                    context.Database.ExecuteSqlCommand("TRUNCATE TABLE TransactionTypes");
                    context.Database.ExecuteSqlCommand("DELETE FROM Customers");
                    context.Database.ExecuteSqlCommand("DELETE FROM Transactions");
                }
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
        public static void FillDb(int transacionsCount)
        {
            LocalData.LoadData();
            DataBaseUtil.CleanDb();
            DataBaseUtil.AddToDb(LocalData.MccCodes, LocalData.TransactionTypes, LocalData.Customers, LocalData.Transactions);
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
            using (new OperationInfo("Creating Db and removing old"))
                Database.SetInitializer(new DropCreateDatabaseAlways<SberBankDbContext>());
        }
    }
}