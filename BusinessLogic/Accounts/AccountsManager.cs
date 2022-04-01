namespace BusinessLogic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DataTransferObjects;
    using Repository;

    public class AccountsManager : IAccountsManager
    {
        #region Fields

        /// <summary>
        /// The repository
        /// </summary>
        private readonly IRepository Repository;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountsManager"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public AccountsManager(IRepository repository)
        {
            this.Repository = repository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Deletes the account.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void DeleteAccount(Int32 id)
        {
            this.Repository.DeleteAccount(id);
        }

        /// <summary>
        /// Gets the account.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Account GetAccount(Int32 id)
        {
            return this.Repository.GetAccount(id);
        }

        /// <summary>
        /// Adds the account.
        /// </summary>
        /// <param name="account">The account.</param>
        public void SaveAccount(Account account)
        {
            this.Repository.SaveAccount(account);
        }


        /// <summary>
        /// Get All the account.
        /// </summary>
        public IEnumerable<Account> GetAll()
        {
            return this.Repository.GetAllAccounts();
        }




        public void Deposit(int id, FundsDto dto)
        {
            this.Repository.DepositFunds(id, dto.Funds);

        }

        public void Withdraw(int id, FundsDto dto)
        {
            this.Repository.WithdrawFunds(id, dto.Funds);
        }

        public void Transfer( TransferDto dto)
        {
            this.Repository.Transfer(dto);
            

        }
        #endregion
    }
}