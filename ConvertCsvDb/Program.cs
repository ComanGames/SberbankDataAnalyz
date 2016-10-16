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

            Action<string,int> logWriteLine = (text, tabs) => { WriteLine($"new string('\t',tabs){text}"); };
            Action<string,int> logReWriteLine = (text, tabs) =>
            {

                SetCursorPosition(0, Console.CursorTop-1);
                WriteLine($"new string('\t',tabs){text}");
            };

            CsvToDb.ConvertAllData();

            CsvToDb.TestDbSpeed();

            ReadKey();
        }
    }

}
