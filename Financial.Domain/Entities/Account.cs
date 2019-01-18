using System.Collections.Generic;

namespace FinancialDemo.Core.Entities
{
    public class Account : BaseEntity
    {
        public string Name { get; set; }

        public decimal Balance { get; set; }

        public virtual List<Transaction> Transactions { get; set; }

        public void AddTransaction(Transaction transaction)
        {
            if (Transactions == null)
                Transactions = new List<Transaction>();

            Transactions.Add(transaction);
        }
    }
}
