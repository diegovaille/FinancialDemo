using FinancialDemo.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinancialDemo.Infrastructure.Data.Context
{
    public class FinancialDbContext : DbContext
    {

        public FinancialDbContext(DbContextOptions<FinancialDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<DebitTransaction>();
            builder.Entity<CreditTransaction>();

            builder.Entity<Account>().HasMany(s => s.Transactions);

            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

    }
}
