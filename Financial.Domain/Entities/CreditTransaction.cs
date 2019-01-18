namespace FinancialDemo.Core.Entities
{
    public class CreditTransaction : Transaction
    {
        public int? OriginAccountId { get; set; }
        
        public CreditTransaction() { }

        public CreditTransaction(TransactionType transactionType)
        {
            Type = transactionType;
        }
    }
}
