using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing
{
    class Program
    {
        static void Main(string[] args)
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
            Console.ReadKey();
        }
    }
}
