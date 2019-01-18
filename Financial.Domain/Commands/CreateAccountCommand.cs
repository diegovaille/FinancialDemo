using FinancialDemo.Core.Models;
using Flunt.Validations;
using MediatR;

namespace FinancialDemo.Core.Commands
{
    public class CreateAccountCommand : BaseCommand, IRequest<Response>, INotification, IValidatable
    {
        public string Name { get; set; }

        public decimal Balance { get; set; }

        public CreateAccountCommand(string name, decimal balance)
        {
            this.Name = name;
            this.Balance = balance;

            AddNotifications(new Contract()
                .IsNotNullOrEmpty(name, "Name", "O nome nao deve ser nulo ou vazio.")
                   .IsGreaterOrEqualsThan(balance, 0, "Amount", "Valor deve ser maior que zero.")
               );
        }

        public void Validate()
        {
            throw new System.NotImplementedException();
        }
    }
}
