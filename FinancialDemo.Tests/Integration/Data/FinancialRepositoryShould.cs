using FinancialDemo.Infrastructure.Data.Context;
using FinancialDemo.Infrastructure.Data.Repository;
using FinancialDemo.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Xunit;

namespace FinancialDemo.Tests.Integration.Data
{
    public class FinancialRepositoryShould
    {
        private FinancialDbContext _dbContext;

        private static DbContextOptions<FinancialDbContext> CreateNewContextOptions()
        {
            // Create a fresh service provider, and therefore a fresh
            // InMemory database instance.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            // Create a new options instance telling the context to use an
            // InMemory database and the new service provider.
            var builder = new DbContextOptionsBuilder<FinancialDbContext>();
            builder.UseInMemoryDatabase("financialdemo")
                   .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }

        [Fact]
        public async void AddItemAndSetId()
        {
            var repository = GetRepository();
            var acc = new AccountBuilder().Name("Teste").Balance(500).Build();

            await repository.Add(acc);

            var newAcc = repository.GetAccounts().GetAwaiter().GetResult().FirstOrDefault();

            Assert.Equal(acc, newAcc);
            Assert.True(newAcc?.Id > 0);
        }

        [Fact]
        public async void UpdateAccountAfterAddingIt()
        {
            // add an account
            var repository = GetRepository();
            var initialName = "Teste";
            var account = new AccountBuilder().Name(initialName).Balance(500).Build();

            await repository.Add(account);

            // detach the account so we get a different instance
            _dbContext.Entry(account).State = EntityState.Detached;

            // fetch the account and change its name
            var newAccount = repository.GetAccounts().GetAwaiter().GetResult()
                .FirstOrDefault(a => a.Name == initialName);

            Assert.NotNull(newAccount);
            Assert.NotSame(account, newAccount);

            var newName = Guid.NewGuid().ToString();
            newAccount.Name = newName;

            // Update the name
            await repository.Update(newAccount);

            var updatedItem = repository.GetAccounts().GetAwaiter().GetResult()
                .FirstOrDefault(a => a.Name == newName);

            Assert.NotNull(updatedItem);
            Assert.NotEqual(account.Name, updatedItem.Name);
            Assert.Equal(newAccount.Id, updatedItem.Id);
        }

        [Fact]
        public async void DeleteAccountAfterAddingIt()
        {
            // add an item
            var repository = GetRepository();
            var initialName = Guid.NewGuid().ToString();
            var account = new AccountBuilder().Name(initialName).Balance(300).Build();
            await repository.Add(account);

            // Validate account was saved
            var newAccount = repository.GetAccounts().GetAwaiter().GetResult()
               .FirstOrDefault(a => a.Name == initialName);

            Assert.NotNull(newAccount);

            // delete the item
            await repository.Delete(account);

            // verify it's no longer there
            Assert.DoesNotContain(repository.GetAccounts().GetAwaiter().GetResult(),
                a => a.Name == initialName);
        }

        [Fact]
        public async void AddTransactionWithAccount()
        {
            var repository = GetRepository();
            var initialName = Guid.NewGuid().ToString();
            var account = new AccountBuilder().Name(initialName).Balance(300).Build();
            await repository.Add(account);

            // Validate account was saved
            var newAccount = repository.GetAccounts().GetAwaiter().GetResult()
               .FirstOrDefault(a => a.Name == initialName);

            Assert.NotNull(newAccount);
            newAccount.AddTransaction(new WithdrawalTransaction { AccountId = newAccount.Id, Amount = 30 });

            await repository.Update(newAccount);

            var transaction = repository.List<WithdrawalTransaction>().GetAwaiter().GetResult()
                .FirstOrDefault(t => t.AccountId == newAccount.Id);

            Assert.NotNull(transaction);
        }

        private FinancialRepository GetRepository()
        {
            var options = CreateNewContextOptions();

            _dbContext = new FinancialDbContext(options);
            return new FinancialRepository(_dbContext);
        }
    }
}
