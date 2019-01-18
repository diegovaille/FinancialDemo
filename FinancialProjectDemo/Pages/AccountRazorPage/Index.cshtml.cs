using FinancialDemo.Core.Entities;
using FinancialDemo.Core.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace FinancialDemo.Web.Pages.AccountRazorPage
{
    public class IndexModel : PageModel
    {
        private readonly IRepository _repository;

        public List<Account> Accounts { get; set; }

        public IndexModel(IRepository repository)
        {
            _repository = repository;
        }

        public async void OnGet()
        {
            Accounts = await _repository.List<Account>();
        }
    }
}