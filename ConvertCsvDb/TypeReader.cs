using System;
using DataTools;

namespace ConvertCsvDb
{
    public static class TypeReader
    {
        public static T[] SubArray<T>(this T[] data, int index, int length)
        {
            T[] result = new T[length];
            Array.Copy(data, index, result, 0, length);
            return result;
        }
        public static MccCode GetMccCode(string[] csv)
        {
            var codeIdex = int.Parse(csv[0]);
            var codeDescription = csv[1];
            MccCode mccCode = new MccCode() {Value = codeIdex, Description = codeDescription, ManProc = -1};
            return mccCode;
        }
        public static TransactionType GetTransactionType(string[] csv)
        {
            var codeIdex = int.Parse(csv[0]);
            var codeDescription = csv[1];
            TransactionType transactionType = new TransactionType() {Value = codeIdex, Description = codeDescription, ManProc = -1};
            return transactionType;
        }
        public static Customer GetCustomerWithGender(string[] csv)
        {
            int bankId = int.Parse(csv[0]);
            float gender = int.Parse(csv[1]);
            Customer customer = new Customer(){BankId = bankId,Gender = gender,GenderKnowne =true };
            return customer;
        }
        public static Transaction GetTransaction(string[] csv)
        {
            int bankId = int.Parse(csv[0]);
            string transactionTimestring = csv[1];
            int mccCode = int.Parse(csv[2]);
            int transcationType = int.Parse(csv[3]);
            double amount = double.Parse(csv[4]);
            string termId = csv[5];

            string[] splited = transactionTimestring.Split(' ');
            int day = Int32.Parse(splited[0]);
            TimeSpan time = TimeSpan.Parse(splited[1].Replace("60", "59")+".0");


            Transaction transaction = new Transaction(){BankId = bankId,MccCode = mccCode,TransactionType = transcationType,Amount = amount,TimeDay = day,TimeHours = time,TermId = termId};
            return transaction;
        }
    }
}