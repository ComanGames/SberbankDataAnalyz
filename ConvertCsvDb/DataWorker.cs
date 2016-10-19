using System;
using System.IO;
using DataTools.DefaultData;
using static ConvertCsvDb.TypeReader;

namespace ConvertCsvDb
{
    public static class DataWorker
    {
        public static int CoreCount = 4;
        public const int MbxMb = 1048576;
        public const int BlockSize = 1024;

        public static bool IsReadingProgress = true;
        public static Action<string,int> LogWriteLine = (x,y)=>{};
        public static Action<string,int> LogReWriteLine = (x,y)=>{};


        public static void ConvertAllData()
        {

            OperationInfo.LogAction = LogWriteLine;

            //Loading all data

            string pathToMccCsv = DataFromCsv.PathToDateFile(DataFromCsv.MccCodeFile);
            string pathToTransactionTypeCsv = DataFromCsv.PathToDateFile(DataFromCsv.TransactionTypeFile);
            string pathToGenderFile = DataFromCsv.PathToDateFile(DataFromCsv.CustomerGenderTrainFile);

            MccCode[] mccCodes;
            TransactionType[] transactionTypes;
            Customer[] customers;
            Transaction[] transactions;

            using (new OperationInfo("Read All csv ", 0))
            {
                using (new OperationInfo($"Reading from {DataFromCsv.MccCodeFile}", 1))
                    mccCodes = DataFromCsv.GetDataFromCsv(';', GetMccCode, pathToMccCsv);

                using (new OperationInfo($"Reading from {DataFromCsv.TransactionTypeFile}", 1))
                    transactionTypes = DataFromCsv.GetDataFromCsv(';', GetTransactionType, pathToTransactionTypeCsv);

                using (new OperationInfo($"Reading from {DataFromCsv.CustomerGenderTrainFile}", 1))
                    customers = DataFromCsv.GetDataFromCsv(',', GetCustomerWithGender, pathToGenderFile);

                using (new OperationInfo($"Reading from {Path.GetFileName(DataFromCsv.PathToTransactionFile)}", 1))
                    transactions = DataFromCsv.GetDataFromCsv(',', GetTransaction, DataFromCsv.PathToTransactionFile);
            }
            Cleaning();
            using (new OperationInfo("Add Data to db",0))
            {
                DataBaseUtil.AddToDb(mccCodes, transactionTypes, customers, transactions);
            }

        }

        private static void Cleaning()
        {
            LogWriteLine("================================================================", 0);
            using (new OperationInfo("Cleaning memory", 1))
            {
                GC.Collect();
            }
            LogWriteLine("================================================================", 0);
            LogWriteLine("", 0);
        }
    }
}