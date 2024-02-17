using System;
using Microsoft.EntityFrameworkCore;
using SessionTestProject.Entities;

namespace SessionTestProject.Concrete
{
	public class SqlDbContext : DbContext
	{
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost,1433;Initial Catalog=sql;User ID=SA;Password=reallyStrongPwd123;TrustServerCertificate=true;");
        }

        public DbSet<Account> Accounts { get; set; }
    }
}

