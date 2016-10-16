using System;
using CsvHelper;
using DataTools;

namespace ConvertCsvDb
{
    public class TypeReader
    {
        public static MccCode GetMccCode(CsvReader csv)
        {
            var codeIdex = csv.GetField<int>(0);
            var codeDescription = csv.GetField<string>(1);
            MccCode mccCode = new MccCode() {Value = codeIdex, Description = codeDescription, ManProc = -1};
            return mccCode;
        }
        public static TransactionType GetTransactionType(CsvReader csv)
        {
            var codeIdex = csv.GetField<int>(0);
            var codeDescription = csv.GetField<string>(1);
            TransactionType transactionType = new TransactionType() {Value = codeIdex, Description = codeDescription, ManProc = -1};
            return transactionType;
        }
        public static Customer GetCustomerWithGender(CsvReader csv)
        {
            int bankId = csv.GetField<int>(0);
            float gender = csv.GetField<int>(1);
            Customer customer = new Customer(){BankId = bankId,Gender = gender,GenderKnowne =true };
            return customer;
        }
        public static Transaction GetTransaction(CsvReader csv)
        {
            int bankId = csv.GetField<int>(0);
            string transactionTimestring = csv.GetField<string>(1);
            int mccCode = csv.GetField<int>(2);
            int transcationType = csv.GetField<int>(3);
            double amount = csv.GetField<double>(4);
            string termId = csv.GetField<string>(5);

            string[] splited = transactionTimestring.Split(' ');
            int day = Int32.Parse(splited[0]);
            TimeSpan time = TimeSpan.Parse(splited[1].Replace("60", "59")+".0");


            Transaction transaction = new Transaction(){BankId = bankId,MccCode = mccCode,TransactionType = transcationType,Amount = amount,TimeDay = day,TimeHours = time,TermId = termId};
            return transaction;
        }
    }
}