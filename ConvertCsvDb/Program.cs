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
            Action<string,int> logAction = (text, tabs) =>
            {
                WriteLine($"new string('\t',tabs){text}");
            };

            CsvToDb.ConvertAllData(logAction);

            CsvToDb.TestDbSpeed(logAction);

            ReadKey();
        }
    }

}
