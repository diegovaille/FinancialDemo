using FinancialDemo.Core.Commands;
using FinancialDemo.Core.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FinancialDemo.Web.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransferController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IRepository _repository;

        public TransferController(IMediator mediator, IRepository repository)
        {
            _mediator = mediator;
            _repository = repository;
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] TransferCommand transfer)
        {

            var response = await _mediator.Send(transfer);
            if (response.HasError)
            {
                return BadRequest(response.Data);
            }

            return Ok(response.Data);
        }
    }
}
