using System;
using System.IO;

namespace ConvertCsvDb
{
    public static class DataFromCsv
    {
        public const string MccCodeFile = "tr_mcc_codes.csv";
        public const string TransactionTypeFile = "tr_types.csv";
        public const string CustomerGenderTrainFile = "customers_gender_train.csv";
        public const string TransactionCutedFile = "transactions_cut_500000_lines.csv";
        public static string TransactionsFile = "transactions.dat";

        public static string PathToTransactionFileCut =>PathToDateFile(TransactionCutedFile);
        public static string PathToTransactionFile => Properties.Settings.Default.TransactionFile;

        public static void MadeCutVersionOfFile(string filePath, int newLineCount)
        {
            string fileName = Path.GetFileName(filePath);
            string newFileName = Path.GetFileNameWithoutExtension(filePath) +"_cut_"+newLineCount+"_lines"+  Path.GetExtension(filePath);
            int fileNumber = 0;
            while (File.Exists(newFileName))
            {
                fileNumber++;
                newFileName = Path.GetFileNameWithoutExtension(filePath) + "_cut_"+ newLineCount + "_lines" + "_"+fileNumber+ Path.GetExtension(filePath);
            }
            string newFilePath = PathToDateFile(newFileName);
            
            using (new OperationInfo($"Cutting file {fileName} to {newFileName}", 0))
            {
                string[] allLines;
                using (new OperationInfo("Reading text from file", 1))
                    allLines = File.ReadAllLines(filePath);

                DataWorker.LogWriteLine("", 2);
                DataWorker.LogWriteLine($" {allLines.Length} lines", 2);
                DataWorker.LogWriteLine("", 2);

                float fileSizeBytes = new FileInfo(filePath).Length;
                float fileSizeInMb = ((int)((fileSizeBytes / DataWorker.MbxMb) * 100)) / 100.0f;
                DataWorker.LogWriteLine($"File Size is { fileSizeInMb} MB", 2);
                DataWorker.LogWriteLine("", 2);


                using (new OperationInfo("Cutting and saving", 1))
                {
                    File.Create(newFilePath);
                    File.WriteAllLines(newFilePath, allLines.SubArray(0, newLineCount));
                }


                DataWorker.LogWriteLine("", 2);
                DataWorker.LogWriteLine($" {newLineCount} lines", 2);
                DataWorker.LogWriteLine("", 2);

                fileSizeBytes = new FileInfo(newFilePath).Length;
                fileSizeInMb = ((int)((fileSizeBytes / DataWorker.MbxMb) * 100)) / 100.0f;
                DataWorker.LogWriteLine($"New File Size is { fileSizeInMb} MB", 2);
                DataWorker.LogWriteLine("", 2);

            }
        }

        public static T[] GetDataFromCsv<T>(char delimiter, Func<string[],T>readDataLine, string pathToFile)
        {
            if(!File.Exists(pathToFile))
                throw new Exception($"Please reset path to file {pathToFile}");

            string[] allLines;

            using(new OperationInfo("Reading text from file",2))
                allLines = File.ReadAllLines(pathToFile);

            int linesCount = allLines.Length;
            DataWorker.LogWriteLine($"{linesCount} lines", 2);
            DataWorker.LogWriteLine("", 2);

            float fileSizeBytes = new FileInfo(pathToFile).Length;
            float fileSizeInMb = ((int)((fileSizeBytes/DataWorker.MbxMb)*100))/100.0f;
            DataWorker.LogWriteLine($"File Size is { fileSizeInMb} MB",2);
            DataWorker.LogWriteLine("", 2);

            T[] result = new T[linesCount-1];

            using (ProgressCount progress = new ProgressCount(result.Length))
            {

                for (int i = 0; i < result.Length; i++)
                {
                    result[i] = readDataLine(allLines[i + 1].Split(delimiter));
                    progress.Update();
                }
            }

            return result;
        }

        public static string PathToDateFile(string fileName)
        {
            return Path.Combine(Environment.CurrentDirectory, @"Materials\", fileName);
        }
    }
}