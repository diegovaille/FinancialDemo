using FinancialDemo.Core;
using FinancialDemo.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FinancialDemo.Web.Controllers
{
    [Route("[controller]")]
    public class AccountsController : Controller
    {
        private readonly IRepository _repository;

        public AccountsController(IRepository repository)
        {
            _repository = repository;
        }

        protected async Task<IActionResult> Index()
        {
            return View(await ListAccounts());
        }

        protected IActionResult Populate()
        {
            int recordsAdded = DatabasePopulator.PopulateDatabase(_repository);
            return Ok(recordsAdded);
        }

        [HttpGet]
        public async Task<IActionResult> ListAccounts()
        {
            var accounts = await _repository.GetAccounts();

            return Ok(accounts);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAccountById(int id)
        {
            var item = await _repository.GetAccountById(id);

            return Ok(item);
        }

    }
}
