using System;
using System.IO;
using System.Text;
using System.Threading;
using DataTools.DefaultData;
using static System.Console;
using static ConvertCsvDb.TypeReader;

namespace ConvertCsvDb
{
    class Program
    {
        static void Main()
        {
            OutputEncoding = Encoding.UTF8;
            CursorVisible = false;

            DataWorker.LogWriteLine = (text, tabs) =>
            {
                WriteLine($"{new string('\t',tabs)}{text}");
            };

            DataWorker.LogReWriteLine = (text, tabs) =>
            {

                SetCursorPosition(0, CursorTop-1);
                WriteLine($"{new  string('\t',tabs)} {text}");
            };

            OperationInfo.LogAction = DataWorker.LogWriteLine;
            ProgressCount.LogWriteLine = DataWorker.LogWriteLine;
            ProgressCount.LogReWriteLine = DataWorker.LogReWriteLine;

            DataWorker.CoreCount = Environment.ProcessorCount;
            BinaryTools.Compression = true;

            Transaction[] transactions;
            using (new OperationInfo("Loading from binary file", 1))
            {
                transactions = BinaryTools.LoadTransactionsFromBinary(DataFromCsv.PathToDateFile(DataFromCsv.TransactionsFile));
            }
            WriteLine(transactions.Length);


//            CleanDb();
//            RealConverting();
//            DataFromCsv.MadeCutVersionOfFile(DataFromCsv.PathToTransactionFile,500000);
            ReadKey();

        }

        private static void TranscationsToBinary(string pathToTransansFile)
        {
            string pathToTransactionFile = pathToTransansFile;
            Transaction[] transactions;
            using (new OperationInfo($"Reading from {Path.GetFileName(pathToTransactionFile)}", 1))
                transactions = DataFromCsv.GetDataFromCsv(',', GetTransaction, pathToTransactionFile);

            BinaryTools.SaveTransactionsToBinary(transactions, pathToTransactionFile);
        }

        private static void CleanDb()
        {
            using (new OperationInfo("Cleaning db", 0))
            {
                DataBaseUtil.CleanDb();
            }
        }

        private static void RealConverting()
        {
            WriteLine($"Detected {DataWorker.CoreCount} cores.Done Initialization...");
            WriteLine();
            WriteLine("Press enter to get data from csv files...");
            WriteLine();

            WriteLine("Start converting csv to Db");

            DataWorker.ConvertAllData(DataFromCsv.PathToTransactionFileCut);


            WriteLine("Done converting csv to Db");
            WriteLine();
            WriteLine("If you want to run db test press Enter if not close app");
            ReadKey();

            DataBaseUtil.TestDbSpeed();

        }
    }
}
