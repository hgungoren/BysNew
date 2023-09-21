using ToksozBysNew.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToksozBysNew.Budgets;

namespace ToksozBysNew.Web.Pages.Budgets
{
    public class CreateModalModel : ToksozBysNewPageModel
    {
        [BindProperty]
        public BudgetCreateViewModel Budget { get; set; }

        public List<SelectListItem> CompanyLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(" — ", "")
        };

        private readonly IBudgetsAppService _budgetsAppService;

        public CreateModalModel(IBudgetsAppService budgetsAppService)
        {
            _budgetsAppService = budgetsAppService;
        }

        public async Task OnGetAsync()
        {
            Budget = new BudgetCreateViewModel();
            CompanyLookupList.AddRange((
                                    await _budgetsAppService.GetCompanyLookupAsync(new LookupRequestDto
                                    {
                                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                        );

            await Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostAsync()
        {

            await _budgetsAppService.CreateAsync(ObjectMapper.Map<BudgetCreateViewModel, BudgetCreateDto>(Budget));
            return NoContent();
        }
    }

    public class BudgetCreateViewModel : BudgetCreateDto
    {
    }
}