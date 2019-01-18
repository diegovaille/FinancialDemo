using FinancialDemo.Core.Commands;
using FinancialDemo.Core.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FinancialDemo.Core.Services
{
    public class ValidationPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : BaseCommand where TResponse : Response
    {

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (!((BaseCommand)request).Valid)
            {
                var response = (TResponse)new Response(((BaseCommand)request).Notifications, true);
                return response;
            }

            var result = await next();
            return result;
        }
    }
}
