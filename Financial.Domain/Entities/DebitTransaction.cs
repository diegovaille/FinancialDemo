namespace FinancialDemo.Core.Entities
{
    public class DebitTransaction : Transaction
    {
        public int? DestinationAccountId { get; set; }

        public DebitTransaction() { }

        public DebitTransaction(TransactionType transactionType)
        {
            Type = transactionType;
        }
    }
}