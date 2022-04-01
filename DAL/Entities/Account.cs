namespace DAl.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Account
    {
        public Account() { }
        public Int32 Id { get; set; }
        [ForeignKey("Customer")]
        public Int32 CustomerId { get; set; }
        public Customer Customer { get; set; }
        public Decimal Balance { get; set; }
    }
    
}