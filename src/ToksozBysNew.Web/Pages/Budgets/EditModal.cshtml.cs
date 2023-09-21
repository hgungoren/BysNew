using ToksozBysNew.Shared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using ToksozBysNew.Budgets;

namespace ToksozBysNew.Web.Pages.Budgets
{
    public class EditModalModel : ToksozBysNewPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public BudgetUpdateViewModel Budget { get; set; }

        public List<SelectListItem> CompanyLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(" — ", "")
        };

        private readonly IBudgetsAppService _budgetsAppService;

        public EditModalModel(IBudgetsAppService budgetsAppService)
        {
            _budgetsAppService = budgetsAppService;
        }

        public async Task OnGetAsync()
        {
            var budgetWithNavigationPropertiesDto = await _budgetsAppService.GetWithNavigationPropertiesAsync(Id);
            Budget = ObjectMapper.Map<BudgetDto, BudgetUpdateViewModel>(budgetWithNavigationPropertiesDto.Budget);

            CompanyLookupList.AddRange((
                                    await _budgetsAppService.GetCompanyLookupAsync(new LookupRequestDto
                                    {
                                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                        );

        }

        public async Task<NoContentResult> OnPostAsync()
        {

            await _budgetsAppService.UpdateAsync(Id, ObjectMapper.Map<BudgetUpdateViewModel, BudgetUpdateDto>(Budget));
            return NoContent();
        }
    }

    public class BudgetUpdateViewModel : BudgetUpdateDto
    {
    }
}