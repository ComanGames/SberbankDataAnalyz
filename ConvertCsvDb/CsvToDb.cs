using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using DataTools;
using static ConvertCsvDb.TypeReader;

namespace ConvertCsvDb
{
    public static class CsvToDb
    {
        public static int CoreCount = 4;
        public const int LineCountToDevide = 1048576;
        public const int BlockSize = 1024;
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

        private  static void AddToDb(MccCode[] mccCodes, TransactionType[] transactionTypes, Customer[] customers, Transaction[] transactions)
        {
            using (SberBankDbContext context = new SberBankDbContext())
            {
                using (new OperationInfo("Add mccCodes table to Db", 1))
                    context.MccCodesDbSet.AddRange(mccCodes);
                using (new OperationInfo("Saving Changes to Db", 2))
                     context.SaveChanges();

                using (new OperationInfo("Add Transaction Type table to Db", 1))
                    context.TransactionTypesDbSet.AddRange(transactionTypes);
                using (new OperationInfo("Saving Changes to Db", 2))
                    context.SaveChanges();


                using (new OperationInfo("Add customers with Gender table to Db", 1))
                    context.CustomersDbSet.AddRange(customers);
                using (new OperationInfo("Saving Changes  to Db", 2))
                    context.SaveChanges();

            }


            Cleaning();

                using (new OperationInfo("Add Transactions table to Db and Saving", 1))
                {
                    Transaction[] result = transactions;
                    int threadStep = result.Length / (3);
                    int dataStep = 1024;

                    var countOfThreds = (result.Length / threadStep) + 1;
                    Thread[] threads = new Thread[countOfThreds];

                    using (ProcentCalc progress= new ProcentCalc((transactions.Length/dataStep) + 1, LogWriteLine, LogReWriteLine))
                    {
                        for (int i = 0; i < countOfThreds; i++)
                        {
                            LogWriteLine($"Thread index{i}", 4);
                                int x = i*threadStep;
                                int max = (x + threadStep < result.Length) ? x + threadStep : result.Length;
                            using (SberBankDbContext context = new SberBankDbContext())
                            {
                                for (int j = x; j < max; j++)
                                {
                                    context.TransactionsDbSet.Add(transactions[j]);
                                    LogReWriteLine($"j={j}", 3);


                                    if (j % dataStep==0)
                                    {
                                        context.SaveChanges();
                                        LogReWriteLine("", 0);
                                        progress.Update();
                                    }

                                }


                            }
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

        private static void CreateDb()
        {
            using (new OperationInfo("Creating Db and removing old",0))
                Database.SetInitializer(new DropCreateDatabaseAlways<SberBankDbContext>());
        }

        public static SynchronizationContext sc;
        private static T[] GetDataFromCsv<T>(char delimiter, Func<string[],T>readDataLine, string pathToFile)
        {
            sc=SynchronizationContext.Current;
            if(!File.Exists(pathToFile))
                throw new Exception($"Please reset path to file {pathToFile}");

            StreamReader textReader = new StreamReader(pathToFile);

            string[] allLines = new string[0];

            allLines = File.ReadAllLines(pathToFile);


            int linesCount = allLines.Length;
            LogWriteLine($"{linesCount} lines", 2);
            LogWriteLine("", 2);

             T[] result = new T[linesCount-1];
            int step = result.Length/(3);

//                Parallel.For(0, (result.Length/step) + 1, (i) =>

            var countOfThreds = (result.Length/step)+1;
            Task[] threads = new Task[countOfThreds];
            for (int i = 0; i < countOfThreds; i++)
            { 
                        int x = i*step;
                        int max = (x + step < result.Length) ? x + step : result.Length;
                        for (int j = x; j < max; j++)
                        {
                                result[j] = readDataLine(allLines[j + 1].Split(delimiter));
                        }
//                            result[i] = readdataline(alllines[i+1].split(delimiter));

            };


            return result;
            }

        private static async Task<List<T>> LineToDataAsync<T>(string[] allLines, string delimiter, Func<string[], T> readDataLine, Action progressUpdate)
        {

            List<string[]> listOfArray=  allLines
                .Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index /(allLines.Length/CoreCount))
                .Select(x => x.Select(v => v.Value).ToArray())
                .ToList();

            Task<List<T>>[] listOfTask = new Task<List<T>>[listOfArray.Count];

                LogWriteLine($"List of array count {listOfArray.Count}", 0);
                LogWriteLine($"", 0);
                LogWriteLine($"", 0);
            for (int i = 0; i < listOfArray.Count; i++)
            {
                listOfTask[i]=new Task<List<T>>( (list) =>
                {
                    string[] listData = (string[])list; 
                    return LineToData<T>(listData,delimiter,readDataLine,()=> {});
                }, listOfArray[i]);
            }
            LogReWriteLine("Waiting for all tasks", 0);
            for (int i = 0; i < listOfTask.Length; i++)
            {
                listOfTask[i].Start();
            }
            Task.WaitAll(listOfTask);

            return null;
        }

        private static List<T> LineToData<T>(string[] allLines, string delimiter, Func<string[], T> readDataLine,Action progressUpdate)
        {
            List<T> resultData = new List<T>();
                for (int i = 0; i < allLines.Length; i++)
                {
                    resultData.Add(readDataLine(Regex.Split(allLines[i], delimiter)));
                    progressUpdate();
                }
            return resultData;
        }


        private static string PathToDateFile(string fileName)
        {
            return Path.Combine(Environment.CurrentDirectory, @"Materials\", fileName);
        }
    }
}