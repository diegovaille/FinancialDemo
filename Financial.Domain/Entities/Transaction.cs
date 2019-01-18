using System;
using System.ComponentModel;
using System.Reflection;

namespace FinancialDemo.Core.Entities
{
    public abstract class Transaction : BaseEntity
    {
        public int AccountId { get; set; }

        public decimal Amount { get; set; }

        public string Operation => this.GetType().Name;

        public TransactionType Type { get; set; }

        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }
    }

    public enum TransactionType
    {
        Transfer = 1,
        Withdrawal = 2,
        Deposit = 3
    }
}