using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using DAl.Entities;
using Microsoft.Extensions.Configuration;

namespace DAL
{
    public class FundsContext : DbContext
    {  
        IConfiguration configuration;
        public DbSet<Customer> Customers{get;set;}
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public FundsContext(IConfiguration _configuration)
        {
            configuration = _configuration;
        }




        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            // optionsBuilder.UseSqlServer("data source=.;initial catalog=funds_DB;user id=sa;password=P@ssw0rd;multipleactiveresultsets=True;connect timeout=180");

            optionsBuilder.UseSqlServer(GetConnectionString(), opts => opts.CommandTimeout((int)TimeSpan.FromSeconds(1000).TotalSeconds));
        }

       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
        public string GetConnectionString()
        {
            return configuration.GetSection("FundsConnection").Value;

        }

    }
}
