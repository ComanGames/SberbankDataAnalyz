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

            WriteLine("Start converting csv to Db");

            CsvToDb.ConvertAllData();

            WriteLine("Done converting csv to Db");
            WriteLine();
            WriteLine("If you want to run db test press Enter if not close app");
            ReadKey();

            CsvToDb.TestDbSpeed();

            ReadKey();
        }
    }

}
