using System;
using System.Linq;
using System.Text;
using DataTools.DefaultData;
using DataTools.LocalData;

namespace Testing
{
    class Program
    {
        static void Main(string[] args)
        {

            Initialization();
            Customer[] girls;
            Customer[] boys;
            Console.WriteLine("Done");
            Console.ReadKey();
            Transaction[] unknownGender;
            using (new OperationInfo("Looking for transactions with unknowne gender"))
            {
                unknownGender = LocalData.Transactions.Where(n => (LocalData.Customers.Count(c => c.BankId == n.BankId))>0) .ToArray();
            }
            Console.WriteLine(unknownGender.Length);
            Console.ReadKey();
        }


        private static void Initialization()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.CursorVisible = false;

            DataWorker.LogWriteLine = (text, tabs) => { Console.WriteLine($"{new string('\t', tabs)}{text}"); };

            DataWorker.LogReWriteLine = (text, tabs) =>
            {
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Console.WriteLine($"{new string('\t', tabs)} {text}");
            };

            OperationInfo.LogAction = DataWorker.LogWriteLine;
            ProgressCount.LogWriteLine = DataWorker.LogWriteLine;
            ProgressCount.LogReWriteLine = DataWorker.LogReWriteLine;
            LocalData.Initilize();
        }

        private static void ConvertSTringToData()
        {
            string strangeDate = "27 16:27:06";
            string[] splitedDay = strangeDate.Split(' ');
            byte day = byte.Parse(splitedDay[0]);
            string[] splitedTime = splitedDay[1].Split(':');
            byte hour = byte.Parse(splitedTime[0]);
            byte minute = byte.Parse(splitedTime[1]);
            byte seconds = byte.Parse(splitedTime[2]);
            Console.WriteLine($"date is {strangeDate}");
            Console.WriteLine($"You Parse {day} {hour}:{minute}:{seconds}");
        }
    }
}
