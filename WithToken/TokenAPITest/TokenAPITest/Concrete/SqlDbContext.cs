using System;
using Microsoft.EntityFrameworkCore;
using TokenAPITest.Entities;

namespace TokenAPITest.Concrete
{
	public class SqlDbContext : DbContext
	{
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost,1433;Initial Catalog=tokenTestDb;User ID=SA;Password=reallyStrongPwd123;TrustServerCertificate=true;");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Token> Tokens { get; set; }
    }
}

