namespace FinancialDemo.Core.Entities
{
    public class TransferTransaction : Transaction
    {
        //[ForeignKey("DestinationAccount")]
        public int DestinationAccountId { get; set; }

        public TransferTransaction()
        {
            Type = TransactionTypes.Transfer;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}