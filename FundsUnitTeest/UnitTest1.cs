using BusinessLogic;
using DAL;
using DataTransferObjects;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository;
using System;

namespace FundsUnitTeest
{
    [TestClass]
    public class UnitTest1
    {
        #region fields
        private readonly ICustomersManager customersManager;
        private readonly IAccountsManager accountsManager;
        private readonly IRepository repository;
        #endregion


        #region constructors
        public UnitTest1() {

            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json")
.Build();

            var dbContext = new TestDbContextMock(builder);

            

this.repository = new PersistentRepository(dbContext);

            this.customersManager = new BusinessLogic.CustomersManager(this.repository);
            this.accountsManager = new AccountsManager(this.repository);
            
        }
        #endregion

        [TestMethod]
        [DataRow(100,  -1000.0)]
        [DataRow(5,- 5000.0)]
        [DataRow(1,-200.0)]
        [DataRow(150000,-300.0)]
        public void Valid_WhenDepositFund(int customerId, double value) 
        {
            try
            {
                this.accountsManager.Deposit(customerId, new FundsDto() { Funds = (decimal)value });
                Assert.Fail("argument exception must be thrown");
            }
            catch (Exception ex)
            {
            }
                        
        }

        [TestMethod]
        [DataRow(100, 1000)]
        [DataRow(5,5000)]
        [DataRow(1, 200)]
        [DataRow(152000, 300)]
        public void Valid_WhenWithdrawFund(int customerId, double value) 
        {
            try
            {
            this.accountsManager.Withdraw(customerId, new FundsDto() { Funds = (decimal)value });
                Assert.Fail("argument exception must be thrown");
            }
            catch (Exception ex)
            {
            }

        }

        
        [TestMethod]
        [DataRow(100000100, 2, 50)]
        [DataRow(20010120, 5, 5000)]
        [DataRow(20022351, 1, 200)]
        [DataRow(33135352, 1, 300)]
        public void FromExists_whenTransfer(Int32 from, Int32 to, double value) 
        {
            try
            {
                this.accountsManager.Transfer(new TransferDto { From = from, To = to, Funds = (decimal)value });
                Assert.Fail("argument exception must be thrown");
            }
            catch (Exception ex)
            {
            }
        
        }


        [TestMethod]
        [DataRow("Mohamed",null,"123456789")]
        [DataRow("Zaki", "", "13153836")]
        [DataRow("", "Martin", "46862135")]
        [DataRow("12", "23", "")]
        public void InvalidCustomer_ThrowException(string Name,string Surname, string IdCard) 
        {
            try
            {
                this.customersManager.SaveCustomer(new DataTransferObjects.Customer() { Name = Name, Surname = Surname, IdCard = IdCard });
                Assert.Fail("argument exception must be thrown");
            }
            catch (Exception ex)
            {
            }
   
        }

        [TestMethod]
        [DataRow(100, 1000)]
        [DataRow(5, 5000)]
        [DataRow(1, 200)]
        [DataRow(1, 300)]
        public void Disable_WhenBalanceLessThanAmountWithdrawn(int customerId, double value)
        {
            try
            {
                this.accountsManager.Withdraw(customerId, new FundsDto() { Funds = (decimal)value });
                Assert.Fail("argument exception must be thrown");
            }
            catch (Exception ex)
            {
            }
       

        }

        [TestMethod]
        [DataRow(1, 2, 50)]
        [DataRow(2, 5, 5000)]
        [DataRow(2, 1, 200)]
        [DataRow(3, 1, 300)]
        public void Disable_WhenBlanceLessThanAcmountTransfered(Int32 from, Int32 to, double value) 
        {
            try
            {
                this.accountsManager.Transfer(new TransferDto { From = from, To = to, Funds = (decimal)value });
                Assert.Fail("argument exception must be thrown");
            }
            catch (Exception ex)
            {
            }
          
        }


        [TestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        [DataRow(4)]
        public void Transactions_CanReproduceBalance(int customerId) 
        {
            
            decimal blance = this.repository.GetAvailableFunds(customerId);
            decimal TransactionSum = this.repository.GetBalanceFromTransactions(customerId);

            Assert.AreEqual(blance, TransactionSum);
        }









    }
}
