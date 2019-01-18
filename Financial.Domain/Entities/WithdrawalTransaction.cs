using System;
using System.Collections.Generic;
using System.Text;

namespace FinancialDemo.Core.Entities
{
    public class WithdrawalTransaction : Transaction
    {
        public WithdrawalTransaction()
        {
            Type = TransactionTypes.Withdrawal;
        }
    }
}
