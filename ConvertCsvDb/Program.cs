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

            CsvToDb.LogWriteLine = (text, tabs) =>
            {
                WriteLine($"{new string('\t',tabs)}{text}");
            };

            CsvToDb.LogReWriteLine = (text, tabs) =>
            {

                SetCursorPosition(0, CursorTop-1);
                WriteLine($"{new  string('\t',tabs)} {text}");
            };

            OperationInfo.LogAction = CsvToDb.LogWriteLine;
            ProgressCount.LogWriteLine = CsvToDb.LogWriteLine;
            ProgressCount.LogReWriteLine = CsvToDb.LogReWriteLine;
            

            CsvToDb.CoreCount = Environment.ProcessorCount;
            RealConverting();
            ReadKey();

        }

        private static void RealConverting()
        {
            WriteLine($"Detected {CsvToDb.CoreCount} cores.Done Initialization...");
            WriteLine();
            WriteLine("Press enter to get data from csv files...");
            WriteLine();

            WriteLine("Start converting csv to Db");

            CsvToDb.ConvertAllData();

            WriteLine("Done converting csv to Db");
            WriteLine();
            WriteLine("If you want to run db test press Enter if not close app");
            ReadKey();

            CsvToDb.TestDbSpeed();

        }
    }

}
