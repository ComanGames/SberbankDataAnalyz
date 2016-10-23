using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using DataTools.DefaultData;

namespace DataTools.LocalData
{
    public class BinaryTools
    {
        public static Encoding DefaultEncoding = Encoding.UTF8;
        public static bool Compression = true;
        public static void SaveTransactionsToBinary(Transaction[] transactions, string pathToTransactionFile)
        {
            string newFileName = Path.GetFileNameWithoutExtension(pathToTransactionFile) + ".dat";
            string newFilePath = LocalData.PathToDateFile(newFileName);
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
                        bw.Write(transactions[i].Terminal);
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

        public static void SaveTransactionsToBinaryCut(Transaction[] transactions, string pathToTransactionFile)
        {
            string newFileName = Path.GetFileNameWithoutExtension(pathToTransactionFile) + "Cut.dat";
            string newFilePath = LocalData.PathToDateFile(newFileName);
            List<byte> bytes = new List<byte>();

                for (int i = 0; i < transactions.Length; i++)
                {
                    bytes.AddRange(BitConverter.GetBytes(transactions[i].CustomerId));
                    bytes.AddRange(BitConverter.GetBytes(transactions[i].Day));
                    bytes.Add(transactions[i].Hour);
                    bytes.Add((transactions[i].Minute));
                    bytes.Add((transactions[i].Second));
                    bytes.Add((transactions[i].MccCodeId));
                    bytes.Add((transactions[i].TransactionTypeId));
                    bytes.AddRange(BitConverter.GetBytes((transactions[i].Amount)));
                    bytes.AddRange(BitConverter.GetBytes(transactions[i].TerminalId));
                }

                using (FileStream file = File.Open(newFilePath,FileMode.Create,FileAccess.Write) )
                {
                    if (Compression)
                    {
                        using (GZipStream zipStream = new GZipStream(file, CompressionMode.Compress, true))
                            zipStream.Write(bytes.ToArray(), 0, bytes.Count);
                    }
                    else 
                        file.Write(bytes.ToArray(),0,bytes.Count);
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
                        transaction.Day = br.ReadUInt16();
                        transaction.Hour = br.ReadByte();
                        transaction.Minute = br.ReadByte();
                        transaction.Second = br.ReadByte();
                        transaction.Amount = br.ReadSingle();
                        transactionsList.Add(transaction);
                }
            }

            return transactionsList.ToArray();
        }

        public static Transaction[] LoadTransactionsFromBinaryCut(string pathToTransactionFile)
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
                        transaction.CustomerId = br.ReadInt16();
                        transaction.Day = br.ReadUInt16();
                        transaction.Hour = br.ReadByte();
                        transaction.Minute = br.ReadByte();
                        transaction.Second = br.ReadByte();
                        transaction.MccCodeId = br.ReadByte();
                        transaction.TransactionTypeId = br.ReadByte();
                        transaction.Amount = br.ReadSingle();
                        transaction.TerminalId = br.ReadInt32();
                        transactionsList.Add(transaction);
                }
            }

            return transactionsList.ToArray();
        }

        public static string[] LoadTerminalsFromBinary(string terminalsFile)
        {

            List<string> terminalsList = new List<string>();

            using (MemoryStream stream = new MemoryStream())
            {

                using (new OperationInfo("Radading and decoding", 1))
                {
                    using (FileStream file = File.Open(terminalsFile, FileMode.Open, FileAccess.Read))
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
                BinaryReader br = new BinaryReader(stream, DefaultEncoding);

                while (stream.Position < stream.Length)
                {
                    string terminal = br.ReadString();
                    terminalsList.Add(terminal);
                }
            }
            return terminalsList.ToArray();
        }

        public static void SaveTerminalsToBinary(string[] terminals, string pathToTerminalsFile)
        {
            string newFileName = Path.GetFileNameWithoutExtension(pathToTerminalsFile) + ".dat";
            string newFilePath = LocalData.PathToDateFile(newFileName);
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryWriter bw = new BinaryWriter(stream, DefaultEncoding);
                for (int i = 0; i < terminals.Length; i++)
                {
                    bw.Write(terminals[i]);
                }

                byte[] bytes = stream.ToArray();
                using (FileStream file = File.Open(newFilePath, FileMode.Create, FileAccess.Write))
                {
                    if (Compression)
                    {
                        using (GZipStream zipStream = new GZipStream(file, CompressionMode.Compress, true))
                            zipStream.Write(bytes, 0, bytes.Length);
                    }
                    else
                        file.Write(bytes, 0, bytes.Length);
                }
            }


        }

    }
}