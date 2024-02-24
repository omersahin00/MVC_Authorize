using System;
using Microsoft.EntityFrameworkCore;
using TokenAPITest.Entities;

namespace TokenAPITest.Concrete
{
	public class SqlDbContext : DbContext
	{
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("Server=localhost;Database=tokenDB;User=root;Password=omer237823;Port=3306;",
                                    new MariaDbServerVersion(new Version(8, 0, 36)));
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Token> Tokens { get; set; }
    }
}

