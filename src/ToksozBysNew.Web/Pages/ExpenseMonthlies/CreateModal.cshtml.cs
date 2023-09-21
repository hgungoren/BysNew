using ToksozBysNew.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToksozBysNew.ExpenseMonthlies;

namespace ToksozBysNew.Web.Pages.ExpenseMonthlies
{
    public class CreateModalModel : ToksozBysNewPageModel
    {
        [BindProperty]
        public ExpenseMonthlyCreateViewModel ExpenseMonthly { get; set; }

        private readonly IExpenseMonthliesAppService _expenseMonthliesAppService;

        public CreateModalModel(IExpenseMonthliesAppService expenseMonthliesAppService)
        {
            _expenseMonthliesAppService = expenseMonthliesAppService;
        }

        public async Task OnGetAsync()
        {
            ExpenseMonthly = new ExpenseMonthlyCreateViewModel();

            await Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostAsync()
        {

            await _expenseMonthliesAppService.CreateAsync(ObjectMapper.Map<ExpenseMonthlyCreateViewModel, ExpenseMonthlyCreateDto>(ExpenseMonthly));
            return NoContent();
        }
    }

    public class ExpenseMonthlyCreateViewModel : ExpenseMonthlyCreateDto
    {
    }
}