using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using DataTools.DefaultData;

namespace ConvertCsvDb
{
    internal class BinaryTools
    {
        public static Encoding DefaultEncoding = Encoding.UTF8;
        public static bool Compression = true;
        public static void SaveTransactionsToBinary(Transaction[] transactions, string pathToTransactionFile)
        {
            string newFileName = Path.GetFileNameWithoutExtension(pathToTransactionFile) + ".dat";
            string newFilePath = DataFromCsv.PathToDateFile(newFileName);
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryWriter bw = new BinaryWriter(stream,DefaultEncoding);
                for (int i = 0; i < transactions.Length; i++)
                {
                        bw.Write(transactions[i].BankId);
                        bw.Write(transactions[i].Day);
                        bw.Write(transactions[i].Hour);
                        bw.Write(transactions[i].Minute);
                        bw.Write(transactions[i].Second);
                        bw.Write(transactions[i].MccCode);
                        bw.Write(transactions[i].TransactionType);
                        bw.Write(transactions[i].Amount);
                        bw.Write(transactions[i].TermId);
                }

                byte[] bytes = stream.ToArray();
                using (FileStream file = File.Open(newFilePath,FileMode.Create,FileAccess.Write) )
                {
                    if (Compression)
                    {
                        using (GZipStream zipStream = new GZipStream(file, CompressionMode.Compress, true))
                            zipStream.Write(bytes, 0, bytes.Length);
                    }
                    else 
                        file.Write(bytes,0,bytes.Length);
                }
            }

        }

        public static Transaction[] LoadTransactionsFromBinary(string pathToTransactionFile)
        {
            List<Transaction> transactionsList = new List<Transaction>(500000);

            using (MemoryStream stream = new MemoryStream())
            {

                using (new OperationInfo("Radading and decoding",1))
                {
                    using (FileStream file = File.Open(pathToTransactionFile, FileMode.Open, FileAccess.Read))
                    {
                        if (Compression)
                        {
                            using (GZipStream zipStream = new GZipStream(file, CompressionMode.Decompress, true))
                                zipStream.CopyTo(stream);
                        }
                        else 
                            file.CopyTo(stream);
                    }
                    
                }
                stream.Position = 0;
                BinaryReader br = new BinaryReader(stream,DefaultEncoding);

                while (stream.Position < stream.Length)
                {
                        Transaction transaction = new Transaction();
                        transaction.BankId = br.ReadInt32();
                        transaction.Day = br.ReadUInt16();
                        transaction.Hour = br.ReadByte();
                        transaction.Minute = br.ReadByte();
                        transaction.Second = br.ReadByte();
                        transaction.MccCode = br.ReadInt16();
                        transaction.TransactionType = br.ReadInt16();
                        transaction.Amount = br.ReadSingle();
                        transaction.TermId = br.ReadString();
                        transactionsList.Add(transaction);
                }
            }

            return transactionsList.ToArray();
        }

    }
}