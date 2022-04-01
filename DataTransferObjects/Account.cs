namespace DataTransferObjects
{
    using System;

    public class Account
    {
        public Account() { }
        public Int32 Id { get; set; }
        public int AccountType { get; set; }
        public Int32 CustomerId { get; set; }
        public Decimal Balance { get; set; }

        public static Account MapToDto(DAl.Entities.Account account)
        {
            return new Account()
            {
                Id = account.Id,

                CustomerId = account.CustomerId,
                Balance = account.Balance
            };
        }


            public static DAl.Entities.Account MapToEntity(Account account,DAl.Entities.Account entity)
            {

                entity.Id = account.Id;

                entity.CustomerId = account.CustomerId;
                entity.Balance = account.Balance;
            return entity;
               
            }
    }
    
}