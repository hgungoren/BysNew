using ToksozBysNew.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToksozBysNew.BudgetDistributions;

namespace ToksozBysNew.Web.Pages.BudgetDistributions
{
    public class CreateModalModel : ToksozBysNewPageModel
    {
        [BindProperty]
        public BudgetDistributionCreateViewModel BudgetDistribution { get; set; }

        public List<SelectListItem> DepartmentLookupListRequired { get; set; } = new List<SelectListItem>
        {
        };
        public List<SelectListItem> ProductLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(" — ", "")
        };
        public List<SelectListItem> BudgetLookupListRequired { get; set; } = new List<SelectListItem>
        {
        };
        public List<SelectListItem> AccountGroupLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(" — ", "")
        };
        public List<SelectListItem> AccountLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(" — ", "")
        };
        public List<SelectListItem> IdentityUserLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(" — ", "")
        };

        private readonly IBudgetDistributionsAppService _budgetDistributionsAppService;

        public CreateModalModel(IBudgetDistributionsAppService budgetDistributionsAppService)
        {
            _budgetDistributionsAppService = budgetDistributionsAppService;
        }

        public async Task OnGetAsync()
        {
            BudgetDistribution = new BudgetDistributionCreateViewModel();
            DepartmentLookupListRequired.AddRange((
                                    await _budgetDistributionsAppService.GetDepartmentLookupAsync(new LookupRequestDto
                                    {
                                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                        );
            ProductLookupList.AddRange((
                                    await _budgetDistributionsAppService.GetProductLookupAsync(new LookupRequestDto
                                    {
                                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                        );
            BudgetLookupListRequired.AddRange((
                                    await _budgetDistributionsAppService.GetBudgetLookupAsync(new LookupRequestDto
                                    {
                                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                        );
            AccountGroupLookupList.AddRange((
                                    await _budgetDistributionsAppService.GetAccountGroupLookupAsync(new LookupRequestDto
                                    {
                                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                        );
            AccountLookupList.AddRange((
                                    await _budgetDistributionsAppService.GetAccountLookupAsync(new LookupRequestDto
                                    {
                                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                        );
            IdentityUserLookupList.AddRange((
                                    await _budgetDistributionsAppService.GetIdentityUserLookupAsync(new LookupRequestDto
                                    {
                                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                        );

            await Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostAsync()
        {

            await _budgetDistributionsAppService.CreateAsync(ObjectMapper.Map<BudgetDistributionCreateViewModel, BudgetDistributionCreateDto>(BudgetDistribution));
            return NoContent();
        }
    }

    public class BudgetDistributionCreateViewModel : BudgetDistributionCreateDto
    {
    }
}