using FinancialDemo.Core.Entities;

namespace FinancialDemo.Core.DTOs
{
    public class AccountDTO
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public decimal Balance { get; private set; }

        public AccountDTO(int id, string name, decimal balance)
        {
            this.Id = id;
            this.Name = name;
            this.Balance = balance;
        }

        public static implicit operator AccountDTO(Account account)
        {
            return new AccountDTO(account.Id, account.Name, account.Balance);
        }
    }
}