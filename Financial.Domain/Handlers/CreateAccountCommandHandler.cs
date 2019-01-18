using System.Threading;
using System.Threading.Tasks;
using FinancialDemo.Core.Commands;
using FinancialDemo.Core.Models;
using MediatR;

namespace FinancialDemo.Core.Handlers
{
    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, Response>
    {
        public Task<Response> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
