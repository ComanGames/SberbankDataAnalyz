using System;
using System.IO;
using DataTools.DefaultData;

namespace DataTools.LocalData
{
    public static class LocalData
    {
        public static string MccCodeFile => PathToDateFile("tr_mcc_codes.csv");
        public static string TransactionTypeFile => PathToDateFile( "tr_types.csv");
        public static string CustomerGenderTrainFile => PathToDateFile( "customers_gender_train.csv");
        public static string TransactionsFile => PathToDateFile( "transactions.dat");
        private static TransactionType[] _transactionTypes;
        private static Transaction[] _transactions;
        private static MccCode[] _mccCodes;
        private static Customer[] _customers;

        public static string PathToDateFile(string fileName)
        {
            return Path.Combine(Environment.CurrentDirectory, @"Materials\", fileName);
        }

        public static void LoadData()
        {
            _mccCodes = DataWorker.GetMcc(MccCodeFile);
            _customers = DataWorker.GetCustomersWithGender(CustomerGenderTrainFile);
            _transactionTypes = DataWorker.GetTransactionTypes(TransactionTypeFile);
            _transactions = DataWorker.GetTransactions(TransactionsFile);
        }

        public static Transaction[] Transactions
        {
            get
            {
                if (_transactions != null) return _transactions;
                _transactions = DataWorker.GetTransactions(TransactionsFile);
                return _transactions;
            }
        }

        public static MccCode[] MccCodes 
        {
            get
            {
                if (_mccCodes != null) return _mccCodes;
                _mccCodes = DataWorker.GetMcc(MccCodeFile);
                return _mccCodes;
            }
        }

        public static Customer[] Customers 
        {
            get
            {
                if (_customers != null) return _customers;
                _customers = DataWorker.GetCustomersWithGender(CustomerGenderTrainFile);
                return _customers;
            }
        }

        public static TransactionType[] TransactionTypes 
        {
            get
            {
                if (_transactionTypes != null) return _transactionTypes;
                _transactionTypes = DataWorker.GetTransactionTypes(TransactionTypeFile);
                return _transactionTypes;
            }
        }
    }
}