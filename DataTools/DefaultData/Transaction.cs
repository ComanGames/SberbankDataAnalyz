using System;
using System.ComponentModel.DataAnnotations;

namespace DataTools.DefaultData
{
    [Serializable]
    public class Transaction
    {
        public int Id { get; set; }
        public short CustomerId { get; set; }
        public int BankId { get; set; }
        public ushort Day { get; set; }
        public byte Hour { get; set; }
        public byte Minute { get; set; }
        public byte Second { get; set; }
        public short MccCode { get; set; }
        public byte MccCodeId { get; set; }
        public int TransactionType { get; set; }
        public byte TransactionTypeId { get; set; }
        public float Amount { get; set; }
        [MaxLength(16)]
        public string Terminal { get; set; }
        public int TerminalId { get; set; }

        public override string ToString()
        {
            return $"{nameof(BankId)}: {BankId}, {nameof(Day)}: {Day}, {nameof(Hour)}: {Hour}, {nameof(Minute)}: {Minute}, {nameof(Second)}: {Second}, {nameof(MccCode)}: {MccCode}, {nameof(TransactionType)}: {TransactionType}, {nameof(Amount)}: {Amount}, {nameof(Terminal)}: {Terminal}";
        }
    }

}