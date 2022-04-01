namespace DataTransferObjects
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    public class Customer
    {
        #region Fields

        /// <summary>
        /// Gets or sets the identifier card.
        /// </summary>
        /// <value>
        /// The identifier card.
        /// </value>
        public String IdCard { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public String Name { get; set; }

        /// <summary>
        /// Gets or sets the surname.
        /// </summary>
        /// <value>
        /// The surname.
        /// </value>
        public String Surname { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Int32 Id { get; set; }

        public static Customer MapToDto(DAl.Entities.Customer customer)
        {
            return new Customer()
            {
                Id = customer.Id,

                Name = customer.Name
            ,
                IdCard = customer.IdCard,
                Surname = customer.Surname
            };

        }

        public static DAl.Entities.Customer MapToEntity(Customer customer, DAl.Entities.Customer entity)
        {

            entity.Id = customer.Id;

            entity.Name = customer.Name;

            entity.IdCard = customer.IdCard;
            entity.Surname = customer.Surname;

            return entity;

        }
        #endregion
    } }
    
