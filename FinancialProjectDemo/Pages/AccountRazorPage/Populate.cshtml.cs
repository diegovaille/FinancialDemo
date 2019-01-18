using FinancialDemo.Core;
using FinancialDemo.Core.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinancialDemo.Web.Pages.AccountRazorPage
{
    public class PopulateModel : PageModel
    {
        private readonly IRepository _repository;

        public PopulateModel(IRepository repository)
        {
            _repository = repository;
        }

        public int RecordsAdded { get; set; }

        public void OnGet()
        {
            RecordsAdded = DatabasePopulator.PopulateDatabase(_repository);
        }
    }
}