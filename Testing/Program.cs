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
            string strangeDate = "427 16:27:06.0";
            string[] splited = strangeDate.Split(' ');
            int day = Int32.Parse(splited[0]);
            TimeSpan time = TimeSpan.Parse(splited[1]);

            Console.WriteLine(time);
            Console.ReadKey();
        }
    }
}
