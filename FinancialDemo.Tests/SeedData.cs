using FinancialDemo.Core.Entities;
using FinancialDemo.Infrastructure.Data.Context;

namespace FinancialDemo.Tests
{
    public static class SeedData
    {
        public static void PopulateTestData(FinancialDbContext dbContext)
        {
            var accounts = dbContext.Accounts;
            foreach (var acc in accounts)
            {
                dbContext.Remove(acc);
            }
            dbContext.SaveChanges();
            dbContext.Accounts.Add(new Account()
            {
                Name = "Test Acc 1",
                Balance = 500
            });
            dbContext.Accounts.Add(new Account()
            {
                Name = "Test Acc 2",
                Balance = 99999
            });
            dbContext.SaveChanges();
        }

    }
}
