using System;
using System.IO;
using DataTools.DefaultData;

namespace DataTools.LocalData
{
    public static class DataWorker
    {
        public static int CoreCount = 4;
        public const int MbxMb = 1048576;
        public const int BlockSize = 1024;

        public static bool IsReadingProgress = true;
        public static Action<string,int> LogWriteLine = (x,y)=>{};
        public static Action<string,int> LogReWriteLine = (x,y)=>{};


        public static void ConvertAllData(string pathToTransactionFile)
        {

            OperationInfo.LogAction = LogWriteLine;

            //Loading all data

            string pathToMccCsv = LocalData.PathToDateFile(LocalData.MccCodeFile);
            string pathToTransactionTypeCsv = LocalData.PathToDateFile(LocalData.TransactionTypeFile);
            string pathToGenderFile = LocalData.PathToDateFile(LocalData.CustomerGenderTrainFile);

            MccCode[] mccCodes;
            TransactionType[] transactionTypes;
            Customer[] customers;
            Transaction[] transactions;

            using (new OperationInfo("Read All csv ", 0))
            {
                using (new OperationInfo($"Reading from {LocalData.MccCodeFile}", 1))
                    mccCodes = GetMcc(pathToMccCsv);

                using (new OperationInfo($"Reading from {LocalData.TransactionTypeFile}", 1))
                    transactionTypes = GetTransactionTypes(pathToTransactionTypeCsv);

                using (new OperationInfo($"Reading from {LocalData.CustomerGenderTrainFile}", 1))
                    customers = GetCustomersWithGender(pathToGenderFile);

                using (new OperationInfo($"Reading from {Path.GetFileName(pathToTransactionFile)}", 1))
                    transactions = GetTransactionsBinary(pathToTransactionFile);
            }
            Cleaning();
            using (new OperationInfo("Add Data to db",0))
            {
                DataBaseUtil.AddToDb(mccCodes, transactionTypes, customers, transactions);
            }

        }

        public static Transaction[] GetTransactionsCSV(string pathToTransactionFile)
        {
            Transaction[] transactions = DataFromCsv.GetDataFromCsv(',', TypeReader.GetTransaction, @"C:\Users\coman\OneDrive\Desktop\transactions.csv");
            return transactions;
        }
        public static Transaction[] GetTransactionsBinary(string pathToTransactionFile)
        {
            Transaction[] transactions = BinaryTools.LoadTransactionsFromBinaryCut(pathToTransactionFile);
            return transactions;
        }

        public static Customer[] GetCustomersWithGender(string pathToGenderFile)
        {
            var customers = DataFromCsv.GetDataFromCsv(',', TypeReader.GetCustomerWithGender, pathToGenderFile);
            return customers;
        }

        public static TransactionType[] GetTransactionTypes(string pathToTransactionTypeCsv)
        {
            var transactionTypes = DataFromCsv.GetDataFromCsv(';', TypeReader.GetTransactionType, pathToTransactionTypeCsv);
            return transactionTypes;
        }

        public static MccCode[] GetMcc(string pathToMccCsv)
        {
            var mccCodes = DataFromCsv.GetDataFromCsv(';', TypeReader.GetMccCode, pathToMccCsv);
            return mccCodes;
        }

        public static void Cleaning()
        {
            LogWriteLine("================================================================", 0);
            using (new OperationInfo("Cleaning memory", 1))
            {
                GC.Collect();
            }
            LogWriteLine("================================================================", 0);
            LogWriteLine("", 0);
        }

        public static string[] GetTerminals(string terminalsFile)
        {
            string[] terminals = BinaryTools.LoadTerminalsFromBinary(terminalsFile);
            return terminals;

        }
    }
}