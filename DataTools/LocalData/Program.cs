using System;
using System.IO;
using System.Text;
using DataTools.DefaultData;

namespace DataTools.LocalData
{
    class Program
    {
        public static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.CursorVisible = false;

            DataWorker.LogWriteLine = (text, tabs) =>
            {
                Console.WriteLine($"{new string('\t',tabs)}{text}");
            };

            DataWorker.LogReWriteLine = (text, tabs) =>
            {

                Console.SetCursorPosition(0, Console.CursorTop-1);
                Console.WriteLine($"{new  string('\t',tabs)} {text}");
            };

            OperationInfo.LogAction = DataWorker.LogWriteLine;
            ProgressCount.LogWriteLine = DataWorker.LogWriteLine;
            ProgressCount.LogReWriteLine = DataWorker.LogReWriteLine;

            DataWorker.CoreCount = Environment.ProcessorCount;
            BinaryTools.Compression = true;



//            CleanDb();
//            RealConverting();
//            DataFromCsv.MadeCutVersionOfFile(DataFromCsv.PathToTransactionFile,500000);
            Console.ReadKey();

        }

        private static void TranscationsToBinary(string pathToTransansFile)
        {
            string pathToTransactionFile = pathToTransansFile;
            Transaction[] transactions;
            using (new OperationInfo($"Reading from {Path.GetFileName(pathToTransactionFile)}", 1))
                transactions = DataFromCsv.GetDataFromCsv(',', TypeReader.GetTransaction, pathToTransactionFile);

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
            Console.WriteLine($"Detected {DataWorker.CoreCount} cores.Done Initialization...");
            Console.WriteLine();
            Console.WriteLine("Press enter to get data from csv files...");
            Console.WriteLine();

            Console.WriteLine("Start converting csv to Db");

            DataWorker.ConvertAllData(LocalData.TransactionsFile);


            Console.WriteLine("Done converting csv to Db");
            Console.WriteLine();
            Console.WriteLine("If you want to run db test press Enter if not close app");
            Console.ReadKey();

            DataBaseUtil.TestDbSpeed();

        }
    }
}
