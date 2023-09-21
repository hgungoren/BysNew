using ToksozBysNew.Shared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using ToksozBysNew.ExpenseMonthlies;

namespace ToksozBysNew.Web.Pages.ExpenseMonthlies
{
    public class EditModalModel : ToksozBysNewPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public ExpenseMonthlyUpdateViewModel ExpenseMonthly { get; set; }

        private readonly IExpenseMonthliesAppService _expenseMonthliesAppService;

        public EditModalModel(IExpenseMonthliesAppService expenseMonthliesAppService)
        {
            _expenseMonthliesAppService = expenseMonthliesAppService;
        }

        public async Task OnGetAsync()
        {
            var expenseMonthly = await _expenseMonthliesAppService.GetAsync(Id);
            ExpenseMonthly = ObjectMapper.Map<ExpenseMonthlyDto, ExpenseMonthlyUpdateViewModel>(expenseMonthly);

        }

        public async Task<NoContentResult> OnPostAsync()
        {

            await _expenseMonthliesAppService.UpdateAsync(Id, ObjectMapper.Map<ExpenseMonthlyUpdateViewModel, ExpenseMonthlyUpdateDto>(ExpenseMonthly));
            return NoContent();
        }
    }

    public class ExpenseMonthlyUpdateViewModel : ExpenseMonthlyUpdateDto
    {
    }
}