using FinancialDemo.Core.Models;
using Flunt.Validations;
using MediatR;

namespace FinancialDemo.Core.Commands
{
    public class TransferCommand : BaseCommand, IRequest<Response>, INotification, IValidatable
    {
        public int AccountOriginId { get; set; }
        public int AccountDestinationId { get; set; }
        public decimal Amount { get; set; }

        public TransferCommand(int accountOriginId, int accountDestinationId, decimal amount)
        {
            this.AccountOriginId = accountOriginId;
            this.AccountDestinationId = accountDestinationId;
            this.Amount = amount;

            AddNotifications(new Contract()
                   .IsGreaterThan(accountOriginId, 0, "AccountOriginId", "O id da conta origem deve ser maior que zero.")
                   .IsGreaterThan(accountDestinationId, 0, "AccountDestinationId", "O id da conta destino deve ser maior que zero.")
                   .AreNotEquals(accountOriginId, accountDestinationId, "Conta", "As contas para transferencia nao podem ser iguais.")
                   .IsGreaterThan(amount, 0, "Amount", "Valor deve ser maior que zero.")
               );
        }

        public void Validate()
        {
            throw new System.NotImplementedException();
        }
    }
}
