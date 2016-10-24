using System;
using System.Collections.Generic;
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
            for (int i = 0; i < 1000; i++)
            {
                Console.WriteLine(LocalData.Terminals[i]); 
            }

            Console.WriteLine("Done");
            Console.ReadKey();
        }

        private static void CreateCutBinaryFile()
        {
            Dictionary<string, int> terminals = new Dictionary<string, int>();
            Dictionary<int, byte> mccCode = new Dictionary<int, byte>();
            Dictionary<int, byte> trType = new Dictionary<int, byte>();
            Dictionary<int, short> customers = new Dictionary<int, short>();
            List<string> terminaslToSave = new List<string>();

            for (int i = 0; i < LocalData.MccCodes.Length; i++)
                mccCode.Add(LocalData.MccCodes[i].Value, (byte) mccCode.Count);

            for (int i = 0; i < LocalData.TransactionTypes.Length; i++)
                trType.Add(LocalData.TransactionTypes[i].Value, (byte) trType.Count);

            for (int i = 0; i < LocalData.Customers.Length; i++)
                customers.Add(LocalData.Customers[i].BankId, (short) customers.Count);


            for (int i = 0; i < LocalData.Transactions.Length; i++)
            {
                Transaction transaction = LocalData.Transactions[i];
                if (!terminals.ContainsKey(transaction.Terminal))
                {
                    transaction.TerminalId = terminaslToSave.Count;
                    terminals.Add(transaction.Terminal, terminals.Count);
                    terminaslToSave.Add(transaction.Terminal);
                }
                transaction.MccCodeId = mccCode[transaction.MccCode];

                if (customers.ContainsKey(transaction.BankId))
                    transaction.CustomerId = customers[transaction.BankId];
                else
                    transaction.CustomerId = -1;

                if (trType.ContainsKey(transaction.TransactionType))
                    transaction.TransactionTypeId = trType[transaction.TransactionType];
            }

            List<Transaction> transactionsWithCustomer = new List<Transaction>();
            int countOfTransactionsWithType = 0;
            for (int i = 0; i < LocalData.Transactions.Length; i++)
            {
                if (LocalData.Transactions[i].CustomerId != -1)
                    transactionsWithCustomer.Add(LocalData.Transactions[i]);
                else
                    countOfTransactionsWithType++;
            }

            Console.WriteLine($"Transactions Without customer {countOfTransactionsWithType}");
            BinaryTools.SaveTerminalsToBinary(terminaslToSave.ToArray(),LocalData.TerminalsFile);
            BinaryTools.SaveTransactionsToBinaryCut(transactionsWithCustomer.ToArray(), LocalData.TransactionTypeFile);
            Console.WriteLine(terminals.Count);
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
            LocalData.LoadData();
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
