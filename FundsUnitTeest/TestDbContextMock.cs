using DAL;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace FundsUnitTeest
{
    public partial class TestDbContextMock : FundsContext
    {
        public TestDbContextMock(IConfiguration _configuration) : base(_configuration)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            /**
             * At this stage, a copy of the actual database is created as a memory database.
             * You do not need to create a separate test database.
             */
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            }
        }
    }
}