using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using CsvHelper;
using DataTools;
using static ConvertCsvDb.TypeReader;

namespace ConvertCsvDb
{
    public static class CsvToDb
    {
        public const string MccCodeFile = "tr_mcc_codes.csv";
        public const string TransactionTypeFile = "tr_types.csv";
        public const string CustomerGenderTrainFile = "customers_gender_train.csv";
        public const string PathToTransactionFile = @"C:\Users\coman\OneDrive\Documents\Visual Studio 2015\Projects\SberBankDataAnalyze\ConvertCsvDb\Materials\transactions.csv";

        public static bool IsReadingProgress = true;
        public static Action<string,int> LogWriteLine = (x,y)=>{};
        public static Action<string,int> LogReWriteLine = (x,y)=>{};

        public static void ConvertAllData()
        {

            OperationInfo.LogAction = LogWriteLine;

            CreateDb();

            //Loading all data

            string pathToMccCsv = PathToDateFile(MccCodeFile);
            string pathToTransactionTypeCsv = PathToDateFile(TransactionTypeFile);
            string pathToGenderFile = PathToDateFile(CustomerGenderTrainFile);

            List<MccCode> mccCodes;
            List<TransactionType> transactionTypes;
            List<Customer> customers;
            List<Transaction> transactions;

            using (new OperationInfo("Read All csv ", 0))
            {
                using (new OperationInfo($"Reading from {MccCodeFile}", 1))
                    mccCodes = GetDataFromCsv(";", GetMccCode, pathToMccCsv);

                using (new OperationInfo($"Reading from {TransactionTypeFile}", 1))
                    transactionTypes = GetDataFromCsv(";", GetTransactionType, pathToTransactionTypeCsv);

                using (new OperationInfo($"Reading from {CustomerGenderTrainFile}", 1))
                    customers = GetDataFromCsv(",", GetCustomerWithGender, pathToGenderFile);

                using (new OperationInfo($"Reading from {Path.GetFileName(PathToTransactionFile)}", 1))
                    transactions = GetDataFromCsv(",", GetTransaction, PathToTransactionFile);
            }
            LogWriteLine("================================================================", 0);
            using (new OperationInfo("Cleaning memory", 1))
            {
                GC.Collect();
            }
            LogWriteLine("================================================================", 0);
            LogWriteLine("", 0);
            using (new OperationInfo("Add Data to db",0))
            {
                using (SberBankDbContext context = new SberBankDbContext())
                {
                    using (new OperationInfo("Add mccCodes table to Db",1))
                        context.MccCodesDbSet.AddRange(mccCodes);
                    using (new OperationInfo("Saving Changes to Db", 2))
                        context.SaveChangesAsync();

                    using (new OperationInfo("Add Transaction Type table to Db",1))
                        context.TransactionTypesDbSet.AddRange(transactionTypes);
                    using (new OperationInfo("Saving Changes to Db", 2))
                        context.SaveChangesAsync();


                    using (new OperationInfo("Add customers with Gender table to Db", 1))
                        context.CustomersDbSet.AddRange(customers);
                    using (new OperationInfo("Saving Changes  to Db", 2))
                        context.SaveChangesAsync();


                    using (new OperationInfo("Add Transactions table to Db", 1))
                        context.TransactionsDbSet.AddRange(transactions);

                    using(new OperationInfo("Saving Changes to Db",2)) 
                        context.SaveChangesAsync();
                    
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

        private static void CreateDb()
        {
            using (new OperationInfo("Creating Db and removing old",0))
                Database.SetInitializer(new DropCreateDatabaseAlways<SberBankDbContext>());
        }

        private static List<T> GetDataFromCsv<T>(string configurationDelimiter, Func<CsvReader,T>readDataLine, string pathToFile)
        {
            if(!File.Exists(pathToFile))
                throw new Exception($"Please reset path to file {pathToFile}");
            double countOfLines = 0;
            double currentLine = 0;
            int procent = 0;
            if (IsReadingProgress)
            {
                countOfLines = File.ReadAllLines(pathToFile).Length;
                LogWriteLine("", 2);
                LogWriteLine($"{countOfLines} lines in { Path.GetFileName(pathToFile)}", 2);

                LogWriteLine("", 2);
                LogWriteLine(" Reading In Progress ", 2);

                LogWriteLine("", 2);
                LogWriteLine($"{0}%", 2);

            }

            List<T> mccCodes = new List<T>();
            StreamReader textReader = new StreamReader(pathToFile);
            CsvReader csv = new CsvReader(textReader);
            csv.Configuration.Delimiter = configurationDelimiter;
           
            while (csv.Read())
            {
                mccCodes.Add(readDataLine(csv));
                if (IsReadingProgress)
                {
                    int newProcent =(int)(((++currentLine)/countOfLines)*100);
                    if (newProcent != procent)
                    {
                        procent = newProcent;
                        LogReWriteLine($"{newProcent}%", 2);
                    }
                }
            }
            if (IsReadingProgress)
            {
                LogReWriteLine($"{100}%", 2);
                
                LogWriteLine(" ", 2);
            }
            return mccCodes;
        }

        private static string PathToDateFile(string fileName)
        {
            return Path.Combine(Environment.CurrentDirectory, @"Materials\", fileName);
        }
    }
}