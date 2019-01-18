using FinancialDemo.Core.Commands;
using FinancialDemo.Core.DTOs;
using FinancialDemo.Core.Entities;
using FinancialDemo.Core.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialDemo.Web.Pages.AccountRazorPage
{
    public class TransferModel : PageModel
    {
        [Display(Name = "Name")]
        public IEnumerable<AccountDTO> Accounts { get; set; }

        [Display(Name = "Conta Origem")]
        [Required]
        [BindProperty]
        public int SelectedSourceAccount { get; set; }

        [Display(Name = "Conta Destino")]
        [Required]
        [BindProperty]
        public int SelectedDestinationAccount { get; set; }

        [Display(Name = "Valor (R$)")]
        [Required]
        [BindProperty]
        public decimal Amount { get; set; }

        private readonly IRepository _repository;
        private readonly IMediator _mediator;

        public TransferModel(IRepository repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        public void OnGet()
        {
            Accounts = _repository.List<Account>().GetAwaiter().GetResult().Select<Account, AccountDTO>(x => x);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var response = await _mediator.Send(new TransferCommand(SelectedSourceAccount, SelectedDestinationAccount, Amount));
            if (response.HasError)
            {
                return BadRequest(response.Data);
            }

            return RedirectToPage();
        }
    }
}