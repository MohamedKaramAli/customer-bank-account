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
using System.Collections.Generic;
using System.Linq;

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
            this.customersManager.SaveCustomer(new Customer() { Name = "n1", Surname = "n2", IdCard = "id1" });
            this.customersManager.SaveCustomer(new Customer() { Name = "n3", Surname = "n4", IdCard = "id2" });

        }
        #endregion

        [TestMethod]
        [DataRow(100, -1000.0)]
        [DataRow(5, -5000.0)]
        [DataRow(15, -200.0)]
        [DataRow(150000, -300.0)]
        public void Exception_WhenDepositFund_NotExisted(int customerId, double value)
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
        [DataRow(5, 5000)]
        [DataRow(7, 200)]
        [DataRow(152000, 300)]
        public void Exception_WhenWithdrawFund_NotExisted(int customerId, double value)
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
        [DataRow(100000100, 200, 50)]
        [DataRow(20010120, 500, 5000)]
        [DataRow(20022351, 100, 200)]
        [DataRow(33135352, 100, 300)]
        public void Exception_FromExists_whenTransfer_NotExistedinvalid(Int32 from, Int32 to, double value)
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
        [DataRow("Mohamed", null, "123456789")]
        [DataRow("Zaki", "", "13153836")]
        [DataRow("", "Martin", "46862135")]
        [DataRow("12", "23", "")]
        public void ThrowException_InvalidCustomer(string Name, string Surname, string IdCard)
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
        [DataRow(50, 5000)]
        [DataRow(30, 200)]
        [DataRow(40, 300)]
        public void Exception_WhenBalanceLessThanAmountWithdrawn(double initialBalance, double value)
        {
            try
            {
                this.accountsManager.Deposit(1, new FundsDto() { Funds = (decimal)initialBalance });
                this.accountsManager.Withdraw(1, new FundsDto() { Funds = (decimal)value });
                Assert.Fail("argument exception must be thrown");
            }
            catch (Exception ex)
            {
            }


        }

        [TestMethod]
        [DataRow( 20, 50)]
        [DataRow(50, 5000)]
        [DataRow( 70, 200)]
        [DataRow( 200, 300)]
        public void Exception_WhenBlanceLessThanAcmountTransfered(double fromCustomerInitialBalance,double value)
        {
            try
            {
                this.accountsManager.Deposit(1, new FundsDto() { Funds = (decimal)fromCustomerInitialBalance });
                this.accountsManager.Transfer(new TransferDto { From = 1, To = 2, Funds = (decimal)value });
                Assert.Fail("argument exception must be thrown");
            }
            catch (Exception ex)
            {
            }

        }


        [TestMethod]
        [DataRow(new double[] { 1.1, 1.2, 1.3, 1.2, 1.9, -1, 8, -2.2 })]
        [DataRow( new double[] { 1.1, 1.8, 1.9, 1.4, 1.9, -1, 3, -2.8})]
        [DataRow( new double[] { 1.1, 1.4, 1.3, 1.4, 1.1, -1, 91, -2.2 })]
        [DataRow( new double[] { 1.1, 1.2, 1.7, 1.4, 1.4, -1, 6, -2.2 })]
        public void Transactions_CanReproduceBalance( double[] operations) 
        {
            operations.ToList().ForEach((op)=> {
                if (op > 0)
                    this.accountsManager.Deposit(1,new FundsDto() { Funds=(decimal)op});
                else
                    this.accountsManager.Withdraw(1, new FundsDto() { Funds = -(decimal)op });
            });


            decimal blance = this.repository.GetAvailableFunds(1);
            decimal TransactionSum = this.repository.GetBalanceFromTransactions(1);

            Assert.AreEqual(blance, TransactionSum);
        }









    }
}
