using FinancialDemo.Core.Commands;
using FinancialDemo.Core.Entities;
using FinancialDemo.Core.Interfaces;
using FinancialDemo.Core.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace FinancialDemo.Web.Application.DomainEventHandlers
{
    public class TransferCommandHandler : IRequestHandler<TransferCommand, Response>
    {
        private readonly IMediator _mediatr;
        private readonly IRepository _accountRepository;

        public TransferCommandHandler(IMediator mediatr, IRepository accountRepository)
        {
            _mediatr = mediatr;
            _accountRepository = accountRepository;
        }

        public async Task<Response> Handle(TransferCommand request, CancellationToken cancellationToken)
        {
            var sourceAccount = await _accountRepository.GetById<Account>(request.AccountOriginId);
            var destinationAccount = await _accountRepository.GetById<Account>(request.AccountDestinationId);

            if (sourceAccount == null)
                return new Response("Conta origem nao encontrada", true);
            if (destinationAccount == null)
                return new Response("Conta destino nao encontrada", true);
            if (sourceAccount.Balance < request.Amount)
                return new Response("Saldo insuficiente na conta origem", true);

            using (var scope = new TransactionScope())
            {
                await CreateDebitTransfer(request, sourceAccount, destinationAccount);
                await CreateCreditTransfer(request, sourceAccount, destinationAccount);
            }

            return new Response("Transferencia concluida com sucesso");
        }

        private async Task CreateCreditTransfer(TransferCommand request, Account sourceAccount, Account destinationAccount)
        {
            CreditTransaction creditTransaction = new CreditTransaction(TransactionType.Transfer) { AccountId = destinationAccount.Id, OriginAccountId = sourceAccount.Id, Amount = request.Amount };
            destinationAccount.Balance += request.Amount;
            destinationAccount.AddTransaction(creditTransaction);
            await _accountRepository.Update(destinationAccount);
        }

        private async Task CreateDebitTransfer(TransferCommand request, Account sourceAccount, Account destinationAccount)
        {
            DebitTransaction debitTransaction = new DebitTransaction(TransactionType.Transfer) { AccountId = sourceAccount.Id, DestinationAccountId = destinationAccount.Id, Amount = request.Amount };
            sourceAccount.Balance -= request.Amount;
            sourceAccount.AddTransaction(debitTransaction);
            await _accountRepository.Update(sourceAccount);
        }
    }
}
