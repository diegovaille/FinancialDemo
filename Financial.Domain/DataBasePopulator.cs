using FinancialDemo.Core.Entities;
using FinancialDemo.Core.Interfaces;
using System.Linq;

namespace FinancialDemo.Core
{
    public static class DatabasePopulator
    {
        public static int PopulateDatabase(IRepository financialRepository)
        {
            if (financialRepository.List<Account>().GetAwaiter().GetResult().Any()) return 0;

            financialRepository.Add(new Account
            {
                Name = "Diego",
                Balance = 1000
            });
            financialRepository.Add(new Account
            {
                Name = "Carlos",
                Balance = 500
            });
            financialRepository.Add(new Account
            {
                Name = "Eduardo",
                Balance = 600
            });

            return financialRepository.List<Account>().GetAwaiter().GetResult().Count;
        }
    }
}
