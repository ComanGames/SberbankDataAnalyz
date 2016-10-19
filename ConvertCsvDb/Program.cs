using System;
using System.Text;
using static System.Console;

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
//            RealConverting();
            DataFromCsv.MadeCutVersionOfFile(DataFromCsv.PathToTransactionFile,500000);
            ReadKey();

        }

        private static void RealConverting()
        {
            WriteLine($"Detected {DataWorker.CoreCount} cores.Done Initialization...");
            WriteLine();
            WriteLine("Press enter to get data from csv files...");
            WriteLine();

            WriteLine("Start converting csv to Db");

            DataWorker.ConvertAllData();

            WriteLine("Done converting csv to Db");
            WriteLine();
            WriteLine("If you want to run db test press Enter if not close app");
            ReadKey();

            DataBaseUtil.TestDbSpeed();

        }
    }

}
