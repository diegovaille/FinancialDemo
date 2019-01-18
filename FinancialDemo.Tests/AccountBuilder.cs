using FinancialDemo.Core.Entities;

namespace FinancialDemo.Tests
{
    public class AccountBuilder
    {
        private readonly Account _account = new Account();

        public AccountBuilder Id(int id)
        {
            _account.Id = id;
            return this;
        }

        public AccountBuilder Name(string name)
        {
            _account.Name = name;
            return this;
        }

        public AccountBuilder Balance(decimal balance)
        {
            _account.Balance = balance;
            return this;
        }

        public AccountBuilder Transaction(Transaction transaction)
        {
            _account.AddTransaction(transaction);
            return this;
        }

        public Account Build() => _account;
    }
}
