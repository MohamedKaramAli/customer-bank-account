using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    using System.Linq;
    using DAL;
    using DataTransferObjects;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Repository.IRepository" />
    public class PersistentRepository : IRepository
    {

        FundsContext context;
        public PersistentRepository(FundsContext _context)
        {
            context = _context ;
        }

        /// <summary>
        /// Deletes the customer.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void DeleteCustomer(Int32 id)
        {
            var customer = this.context.Customers.First(item=>item.Id==id);
            this.context.Customers.Remove(customer);
            this.context.SaveChanges();
        }

        /// <summary>
        /// Deposits the funds.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="funds">The funds.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void DepositFunds(Int32 customerId,
                                 Decimal funds)
        {


            var account =  this.context.Accounts.FirstOrDefault(item => item.CustomerId == customerId);
            if(account != null) { 
                account.Balance += funds;
            }
            else
            {
                this.context.Accounts.Add(new DAl.Entities.Account() {CustomerId= customerId, Balance = funds });
            }
            this.CreateTransaction(null,customerId,funds);
            this.context.SaveChanges();
        }



        /// <summary>
        /// Gets the available funds.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <returns></returns>
        public Decimal GetAvailableFunds(Int32 customerId)
        {
            return this.context.Accounts.FirstOrDefault(item=>item.CustomerId == customerId)?.Balance ?? 0;
        }

        /// <summary>
        /// Gets the customer.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Customer GetCustomer(Int32 id)
        {
            return Customer.MapToDto (this.context.Customers.FirstOrDefault(c => c.Id == id));
        }

        /// <summary>
        /// Saves the customer.
        /// </summary>
        /// <param name="customer">The customer.</param>
        public void SaveCustomer(Customer customer)
        {

           
             //Customer.MapToEntity(customer);
            
            if (customer.Id>0)
            {
                var entity = this.context.Customers.FirstOrDefault(item => item.Id == customer.Id);
                Customer.MapToEntity(customer,entity);
                this.context.Customers.Update(entity);
            }
            else
            {
                this.context.Customers.Add(Customer.MapToEntity(customer,new DAl.Entities.Customer()));
            }
            this.context.SaveChanges();
        }



        /// <summary>
        /// Withdraws the funds.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="funds">The funds.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void WithdrawFunds(Int32 customerId,
                                  Decimal funds)
        {



            var account = this.context.Accounts.FirstOrDefault(item=>item.CustomerId == customerId);

            if (account!=null)
            {
                account.Balance -= funds;
                this.CreateTransaction(customerId,null,funds);
            }
            this.context.SaveChanges();
        }

        public void CreateTransaction(int? from, int? to, decimal value)
        {

            this.context.Transactions.Add(new DAl.Entities.Transaction() { 
            FromAccount = from,
            ToAccount = to,
            Value = value
            });
            
        }

        /// <summary>
        /// Does the customer exist.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Boolean DoesCustomerExist(Int32 id)
        {
            return this.context.Customers.Any(c => c.Id == id);
        }

        public IEnumerable<Customer> GetAll()
        {
            return this.context.Customers.ToList().Select(item=> Customer.MapToDto(item));
        }

        public void DeleteAccount(int id)
        {
            throw new NotImplementedException();
        }



        public void SaveAccount(Account account)
        {
            if (!DoesCustomerExist(account.CustomerId))
                throw new ArgumentException("Customer Must Be Existed");
            
            var entity = this.context.Accounts.FirstOrDefault(item => item.CustomerId == account.CustomerId);

            if (entity.Id > 0)
            {
            }
            else 
            {
                this.context.Accounts.Add(Account.MapToEntity(account,new DAl.Entities.Account()));
            }
            this.context.SaveChanges();
        }

        public IEnumerable<Account> GetAllAccounts()
        {
            return this.context.Accounts.ToList().Select(v=>Account.MapToDto(v));
        }
        public Decimal GetAccountBalance(Int32 customerId) {
            return this.context.Accounts.Where(a => a.CustomerId == customerId)?.Sum(a=>a.Balance)??0;
        }

        public Account GetAccount(int id)
        {
            return Account.MapToDto(this.context.Accounts.Find(id));
        }

        public void Transfer(TransferDto dto)
        {



            this.WithdrawFunds(dto.From, dto.Funds);
            this.DepositFunds(dto.To, dto.Funds);
          
        }

        public decimal GetBalanceFromTransactions(int customerId)
        {
            var transactions = this.context.Transactions.Where(item => item.FromAccount == customerId || item.ToAccount == customerId);
            var balance = transactions.Where(item => item.ToAccount == customerId).Sum(item => item.Value) - transactions.Where(item => item.FromAccount == customerId).Sum(item => item.Value);
       return balance;
                }
    }
}
