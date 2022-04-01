namespace DAl.Entities
{
    using System;

    public class Transaction
    {
        public Int32 id { get; set; }
        public Int32? FromAccount{get;set;}
        public Int32? ToAccount{get;set;}
        public Decimal Value { get; set; }
        public String Notes { get; set; }
        public int TransactionType { get; set; }
    }
    
}