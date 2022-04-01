namespace TechnicalTest.Controllers
{
    using System;
    using System.Collections.Generic;
    using BusinessLogic;
    using DataTransferObjects;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/customers")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        #region Fields

        /// <summary>
        /// The customers manager
        /// </summary>
        private readonly ICustomersManager CustomersManager;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomersController" /> class.
        /// </summary>
        /// <param name="customersManager">The customers manager.</param>
        public CustomersController(ICustomersManager customersManager)
        {
            this.CustomersManager = customersManager;
        }

        #endregion

        #region Methods

        // DELETE api/customers/5
        [HttpDelete("{id}")]
        public ActionResult Delete(Int32 id)
        {

            
                try
                {
                    
                this.CustomersManager.DeleteCustomer(id);
                    return this.Ok("Success");
                }
                catch (Exception ex)
                {

                    return BadRequest(ex.Message);
                }
            
            }

            // GET api/customers
            [HttpGet]
        public ActionResult<IEnumerable<Customer>> Get()
        {
           return new JsonResult( this.CustomersManager.GetAll());
        }

        // GET api/customers/5
        [HttpGet("{id}")]
        public ActionResult<String> Get(Int32 id)
        {
            try
            {
                Customer customer = this.CustomersManager.GetCustomer(id);
                return this.Ok(customer);
            }
            catch (Exception ex)
            {

                return BadRequest("Message");
            }
          
        }

        // POST api/customers
        [HttpPost]
        public ActionResult Post([FromBody] Customer customer)
        {
            try
            {
                this.CustomersManager.SaveCustomer(customer);
                return Ok("success");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
           
        
        }

        // PUT api/customers/5
        [HttpPut("{id}")]
        public ActionResult Put([FromBody] Customer customer)
        {
            try
            {
                this.CustomersManager.SaveCustomer(customer);
                return Ok("success");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            
        }

        #endregion
    }
}