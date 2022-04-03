namespace BusinessLogic
{
    using System;
    using System.Collections.Generic;
    using DataTransferObjects;
    using Repository;

    public class CustomersManager : ICustomersManager
    {
        #region Fields

        /// <summary>
        /// The repository
        /// </summary>
        private readonly IRepository Repository;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomersManager"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public CustomersManager(IRepository repository)
        {
            this.Repository = repository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Deletes the customer.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void DeleteCustomer(Int32 id)
        {
            this.Repository.DeleteCustomer(id);
        }

        /// <summary>
        /// Gets the customer.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Customer GetCustomer(Int32 id)
        {
            return this.Repository.GetCustomer(id);
        }

        /// <summary>
        /// Adds the customer.
        /// </summary>
        /// <param name="customer">The customer.</param>
        public void SaveCustomer(Customer customer)
        {
        


            if (string.IsNullOrEmpty(customer.Name) || string.IsNullOrEmpty(customer.Surname) || string.IsNullOrEmpty(customer.IdCard))
                throw new ArgumentException("Data Can not be empty");

            this.Repository.SaveCustomer(customer);
        }


        /// <summary>
        /// Get All the customer.
        /// </summary>
        public IEnumerable<Customer> GetAll()
        {
            return this.Repository.GetAll();
        }
        #endregion
    }
}