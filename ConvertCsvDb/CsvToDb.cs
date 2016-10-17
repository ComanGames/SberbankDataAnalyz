using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using DataTools;
using DataTools.DefaultData;
using EntityFramework.BulkInsert.Extensions;
using static ConvertCsvDb.TypeReader;

namespace ConvertCsvDb
{
    public static class CsvToDb
    {
        public static int CoreCount = 4;
        public const int MbxMb = 1048576;
        public const int BlockSize = 1024;
        public const int StpsForBigDb = 1200;
        public const string MccCodeFile = "tr_mcc_codes.csv";
        public const string TransactionTypeFile = "tr_types.csv";
        public const string CustomerGenderTrainFile = "customers_gender_train.csv";
        public const string PathToTransactionFile = @"C:\Users\coman\Source\Repos\SberbankDataAnalyz\ConvertCsvDb\Materials\transactions.csv";

        public static bool IsReadingProgress = true;
        public static Action<string,int> LogWriteLine = (x,y)=>{};
        public static Action<string,int> LogReWriteLine = (x,y)=>{};


        private static void BullingToDb<T>(T[] transactions, int startPoint=0, int itemsToDrop=0)
        {
            using (SberBankDbContext context = new SberBankDbContext())
            {
                    context.Configuration.AutoDetectChangesEnabled = false;
                    context.Configuration.ValidateOnSaveEnabled = false;

                    context.BulkInsert(itemsToDrop != 0 ? transactions.SubArray(startPoint, itemsToDrop) : transactions);

            }
        }

        public static void ConvertAllData()
        {

            OperationInfo.LogAction = LogWriteLine;

            //Loading all data

            string pathToMccCsv = PathToDateFile(MccCodeFile);
            string pathToTransactionTypeCsv = PathToDateFile(TransactionTypeFile);
            string pathToGenderFile = PathToDateFile(CustomerGenderTrainFile);

            MccCode[] mccCodes;
            TransactionType[] transactionTypes;
            Customer[] customers;
            Transaction[] transactions;

            using (new OperationInfo("Read All csv ", 0))
            {
                using (new OperationInfo($"Reading from {MccCodeFile}", 1))
                    mccCodes = GetDataFromCsv(';', GetMccCode, pathToMccCsv);

                using (new OperationInfo($"Reading from {TransactionTypeFile}", 1))
                    transactionTypes = GetDataFromCsv(';', GetTransactionType, pathToTransactionTypeCsv);

                using (new OperationInfo($"Reading from {CustomerGenderTrainFile}", 1))
                    customers = GetDataFromCsv(',', GetCustomerWithGender, pathToGenderFile);

                using (new OperationInfo($"Reading from {Path.GetFileName(PathToTransactionFile)}", 1))
                    transactions = GetDataFromCsv(',', GetTransaction, PathToTransactionFile);
            }
            Cleaning();
            using (new OperationInfo("Add Data to db",0))
            {
                AddToDb(mccCodes, transactionTypes, customers, transactions);
            }

        }

        private static void Cleaning()
        {
            LogWriteLine("================================================================", 0);
            using (new OperationInfo("Cleaning memory", 1))
            {
                GC.Collect();
            }
            LogWriteLine("================================================================", 0);
            LogWriteLine("", 0);
        }

        private static void AddToDb(MccCode[] mccCodes, TransactionType[] transactionTypes, Customer[] customers,
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
                using (ProgressCount progress = new ProgressCount(transactions.Length/StpsForBigDb))
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
            OperationInfo.LogAction = LogWriteLine;

            using (new OperationInfo("Testing db speed", 0))
            {
                ReadMccFromDbTest();
            }
        }

        private static void ReadMccFromDbTest()
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

        private static T[] GetDataFromCsv<T>(char delimiter, Func<string[],T>readDataLine, string pathToFile)
        {
            if(!File.Exists(pathToFile))
                throw new Exception($"Please reset path to file {pathToFile}");

            string[] allLines;

            using(new OperationInfo("Reading text from file",2))
                allLines = File.ReadAllLines(pathToFile);

            int linesCount = allLines.Length;
            LogWriteLine($"{linesCount} lines", 2);
            LogWriteLine("", 2);

            float fileSizeBytes = new FileInfo(pathToFile).Length;
            float fileSizeInMb = ((int)((fileSizeBytes/MbxMb)*100))/100.0f;
            LogWriteLine($"File Size is { fileSizeInMb} MB",2);
            LogWriteLine("", 2);

            T[] result = new T[linesCount-1];

            using (ProgressCount progress = new ProgressCount(result.Length))
            {

                for (int i = 0; i < result.Length; i++)
                {
                    result[i] = readDataLine(allLines[i + 1].Split(delimiter));
                    progress.Update();
                }
            }

                return result;
            }

        private static string PathToDateFile(string fileName)
        {
            return Path.Combine(Environment.CurrentDirectory, @"Materials\", fileName);
        }
    }
}