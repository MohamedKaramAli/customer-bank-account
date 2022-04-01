namespace BusinessLogic
{
    using System;
    using System.Collections.Generic;
    using DataTransferObjects;

    public interface IAccountsManager
    {
        #region Methods

        /// <summary>
        /// Deletes the account.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void DeleteAccount(Int32 id);

        /// <summary>
        /// Gets the account.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Account GetAccount(Int32 id);

        /// <summary>
        /// Adds the account.
        /// </summary>
        /// <param name="account">The account.</param>
        void SaveAccount(Account account);
       IEnumerable<Account> GetAll();
        void Deposit(int id, FundsDto dto);
        void Withdraw(int id, FundsDto dto);
        void Transfer(TransferDto dto);

        #endregion
    }
}