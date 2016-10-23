using System;
using System.Data.Entity;
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
        public static string TerminalsFile => PathToDateFile( "terminals.dat");

        private static TransactionType[] _transactionTypes;
        private static Transaction[] _transactions;
        private static MccCode[] _mccCodes;
        private static Customer[] _customers;
        private static string[] _terminals;

        public static void Initialize()
        {
            using (new OperationInfo("Creating DataBase",1))
                Database.SetInitializer(new CreateDatabaseIfNotExists<SberBankDbContext>());

            LoadData();
        }

        public static string PathToDateFile(string fileName)
        {
            return Path.Combine(Environment.CurrentDirectory, @"Materials\", fileName);
        }

        public static void LoadData()
        {
            using (new OperationInfo("Getting Mcc codes"))
                _mccCodes = DataWorker.GetMcc(MccCodeFile);
            using (new OperationInfo("Getting Customers"))
                _customers = DataWorker.GetCustomersWithGender(CustomerGenderTrainFile);
            using (new OperationInfo("Getting Transaction types"))
                _transactionTypes = DataWorker.GetTransactionTypes(TransactionTypeFile);
            using (new OperationInfo("Getting Terminals from binary"))
                _terminals = DataWorker.GetTerminals(TerminalsFile);
            using (new OperationInfo("Getting Transactions from binary"))
                _transactions = DataWorker.GetTransactionsBinary(TransactionsFile);
        }

        public static void UnloadData()
        {

            using (new OperationInfo("Cleaning RAM"))
            {
                _transactions = null;
                GC.Collect();
            }
        }

        public static string[] Terminals 
        {
            get
            {
                if (_terminals != null) return _terminals;
                    _terminals = DataWorker.GetTerminals(TerminalsFile);
                return _terminals;
            }
        }
        public static Transaction[] Transactions
        {
            get
            {
                if (_transactions != null) return _transactions;
                _transactions = DataWorker.GetTransactionsBinary(TransactionsFile);
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