namespace TechnicalTest.Controllers
{
    using System;
    using System.Collections.Generic;
    using BusinessLogic;
    using DataTransferObjects;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/accounts")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        #region Fields

        /// <summary>
        /// The accounts manager
        /// </summary>
        private readonly IAccountsManager AccountsManager;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountsController" /> class.
        /// </summary>
        /// <param name="accountsManager">The accounts manager.</param>
        public AccountsController(IAccountsManager accountsManager)
        {
            this.AccountsManager = accountsManager;
        }

        #endregion

        #region Methods

        // DELETE api/accounts/5
        [HttpDelete("{id}")]
        public void Delete(Int32 id)
        {
            this.AccountsManager.DeleteAccount(id);
        }

        // GET api/accounts
        [HttpGet]
        public ActionResult<IEnumerable<Account>> Get()
        {
            return new JsonResult(this.AccountsManager.GetAll());
        }

        // GET api/accounts/5
        [HttpGet("{id}")]
        public ActionResult<String> Get(Int32 id)
        {
            Account account = this.AccountsManager.GetAccount(id);
            return this.Ok(account);
        }

        // POST api/accounts
        [HttpPost]
        public void Post([FromBody] Account account)
        {
            this.AccountsManager.SaveAccount(account);
        }

        // PUT api/accounts/5
        [HttpPut("{id}")]
        public void Put([FromBody] Account account)
        {
            this.AccountsManager.SaveAccount(account);
        }



        //POST /api/accounts/1/deposit
        [HttpPost]
        [Route("/api/accounts/{id}/deposit")]
        public ActionResult Deposit(int id, [FromBody]FundsDto dto)
        {
            try
            {
                this.AccountsManager.Deposit(id,dto);
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
          
        }
        //POST /api/accounts/1/withdraw
        [HttpPost]
        [Route("/api/accounts/{id}/withdraw")]
        public ActionResult Withdraw(int id,[FromBody]FundsDto dto)
        {
           
            try
            { 
                this.AccountsManager.Withdraw(id, dto);
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            
        }


        //POST /api/accounts/transfer
        [HttpPost]
        [Route("/api/accounts/transfer")]
        public ActionResult Transfer([FromBody] TransferDto dto)
        {
            try
            {
                this.AccountsManager.Transfer(dto);
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
           
        }


        #endregion
    }
}