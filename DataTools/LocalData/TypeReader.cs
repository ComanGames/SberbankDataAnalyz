using System;
using DataTools.DefaultData;

namespace DataTools.LocalData
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
            var codeIdex = short.Parse(csv[0]);
            var codeDescription = csv[1];
            MccCode mccCode = new MccCode() {Value = codeIdex, Description = codeDescription };
            return mccCode;
        }
        public static TransactionType GetTransactionType(string[] csv)
        {
            var codeIdex = int.Parse(csv[0]);
            var codeDescription = csv[1];
            TransactionType transactionType = new TransactionType() {Value = codeIdex, Description = codeDescription};
            return transactionType;
        }
        public static Customer GetCustomerWithGender(string[] csv)
        {
            int bankId = int.Parse(csv[0]);
            bool gender = byte.Parse(csv[1])==1;
            Customer customer = new Customer(){BankId = bankId,IsMan = gender,GenderKnown =true };
            return customer;
        }
        public static Transaction GetTransaction(string[] csv)
        {
            int bankId = int.Parse(csv[0]);
            string transactionTimestring = csv[1];
            short mccCode = short.Parse(csv[2]);
            int transcationType = int.Parse(csv[3]);
            float amount = float.Parse(csv[4]);
            string termId = csv[5];

            string[] splitedDay = transactionTimestring.Split(' ');
            ushort day = ushort.Parse(splitedDay[0]);
            string[] splitedTime = splitedDay[1].Split(':');
            byte hour = byte.Parse(splitedTime[0]);
            byte minute = byte.Parse(splitedTime[1]);
            byte seconds = byte.Parse(splitedTime[2]);

            Transaction transaction = new Transaction()
            {
                BankId = bankId,
                MccCode = mccCode,
                TransactionType = transcationType,
                Amount = amount,
                Day = day ,
                Hour = hour,
                Minute = minute,
                Second = seconds,
                Terminal = string.IsNullOrEmpty(termId)?"null":termId
            };
            return transaction;
        }
    }
}