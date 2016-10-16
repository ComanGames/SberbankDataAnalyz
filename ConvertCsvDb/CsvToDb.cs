using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using CsvHelper;
using DataTools;

namespace ConvertCsvDb
{
    public static class CsvToDb
    {
        public const string CustomerGenderTrainFile = "customers_gender_train.csv";
        public const string MccCodeFile = "tr_mcc_codes.csv";
        public static Action<string,int> LogWriteLine = (x,y)=>{};
        public static Action<string,int> LogReWriteLine = (x,y)=>{};

        public static void ConvertAllData()
        {

            OperationInfo.LogAction = LogWriteLine;

            CreateDb();

            using (new OperationInfo("Read All csv and puting to Db ",0))
            {
                ConvertMcc();
            }

        }

        public static void TestDbSpeed()
        {
            OperationInfo.LogAction = LogWriteLine;

            using (new OperationInfo("Testing db speed", 0))
            {
                ReadMccFromDbTest();
            }
        }

        private static void ReadMccFromDbTest()
        {
            using (new OperationInfo("Test getting mcc code from db",1))
            {
                using (SberBankDbContext context = new SberBankDbContext())
                {
                    // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
                       context.MccCodesDbSet.ToArray();
                }
            }
        }

        private static void ConvertMcc()
        {
            List<MccCode> mccCodes;

            using (new OperationInfo($"Reading from {MccCodeFile}",1))
                mccCodes = GetDataFromCsv(";", TypeReader.GetMccCode, PathToDateFile(MccCodeFile));


            using (new OperationInfo("Add mccCodes to Db and saving",1))
            {
                using (SberBankDbContext context = new SberBankDbContext())
                {
                    context.MccCodesDbSet.AddRange(mccCodes);
                    context.SaveChanges();
                }
            }
        }

        private static void CreateDb()
        {
            using (new OperationInfo("Creating Db and removing old",1))
                Database.SetInitializer(new DropCreateDatabaseAlways<SberBankDbContext>());
        }

        private static List<T> GetDataFromCsv<T>(string configurationDelimiter, Func<CsvReader,T>readDataLine, string pathToFile)
        {
            List<T> mccCodes = new List<T>();
            StreamReader textReader = new StreamReader(pathToFile);
            CsvReader csv = new CsvReader(textReader);
            csv.Configuration.Delimiter = configurationDelimiter;
            while (csv.Read())
            {
                mccCodes.Add(readDataLine(csv));
            }
            return mccCodes;
        }

        private static string PathToDateFile(string fileName)
        {
            return Path.Combine(Environment.CurrentDirectory, @"Materials\", fileName);
        }
    }
}