using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using ToksozBysNew.BudgetDistributions;
using ToksozBysNew.Shared;

namespace ToksozBysNew.Web.Pages.BudgetDistributions
{
    public class IndexModel : AbpPageModel
    {
        public string CostCenterFilter { get; set; }
        public string ExpenseTypeFilter { get; set; }
        public int? ProjectItemFilterMin { get; set; }

        public int? ProjectItemFilterMax { get; set; }
        public int? TypeFilterMin { get; set; }

        public int? TypeFilterMax { get; set; }
        public int? UnitFilterMin { get; set; }

        public int? UnitFilterMax { get; set; }
        public float? UnitValueFilterMin { get; set; }

        public float? UnitValueFilterMax { get; set; }
        public int? MonthFilterMin { get; set; }

        public int? MonthFilterMax { get; set; }
        public int? YearFilterMin { get; set; }

        public int? YearFilterMax { get; set; }
        public float? RatioFilterMin { get; set; }

        public float? RatioFilterMax { get; set; }
        public float? AmountFilterMin { get; set; }

        public float? AmountFilterMax { get; set; }
        public float? MemoFilterMin { get; set; }

        public float? MemoFilterMax { get; set; }
        public float? InvoiceFilterMin { get; set; }

        public float? InvoiceFilterMax { get; set; }
        public int? CurrencyFilterMin { get; set; }

        public int? CurrencyFilterMax { get; set; }
        public float? CurrencyAmountFilterMin { get; set; }

        public float? CurrencyAmountFilterMax { get; set; }
        public int? ExpenseCategoryFilterMin { get; set; }

        public int? ExpenseCategoryFilterMax { get; set; }
        public int? ExpenseNecessityFilterMin { get; set; }

        public int? ExpenseNecessityFilterMax { get; set; }
        public string CommentFilter { get; set; }
        public string StatusFilter { get; set; }
        public int? ApprovalFilterMin { get; set; }

        public int? ApprovalFilterMax { get; set; }
        [SelectItems(nameof(IsActiveBoolFilterItems))]
        public string IsActiveFilter { get; set; }

        public List<SelectListItem> IsActiveBoolFilterItems { get; set; } =
            new List<SelectListItem>
            {
                new SelectListItem("", ""),
                new SelectListItem("Yes", "true"),
                new SelectListItem("No", "false"),
            };
        [SelectItems(nameof(DepartmentLookupList))]
        public Guid DepartmentIdFilter { get; set; }
        public List<SelectListItem> DepartmentLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(string.Empty, "")
        };

        [SelectItems(nameof(ProductLookupList))]
        public Guid? ProductIdFilter { get; set; }
        public List<SelectListItem> ProductLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(string.Empty, "")
        };

        [SelectItems(nameof(BudgetLookupList))]
        public Guid BudgetIdFilter { get; set; }
        public List<SelectListItem> BudgetLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(string.Empty, "")
        };

        [SelectItems(nameof(AccountGroupLookupList))]
        public Guid? AccountGroupIdFilter { get; set; }
        public List<SelectListItem> AccountGroupLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(string.Empty, "")
        };

        [SelectItems(nameof(AccountLookupList))]
        public Guid? AccountIdFilter { get; set; }
        public List<SelectListItem> AccountLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(string.Empty, "")
        };

        [SelectItems(nameof(IdentityUserLookupList))]
        public Guid? IdentityUserIdFilter { get; set; }
        public List<SelectListItem> IdentityUserLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(string.Empty, "")
        };

        private readonly IBudgetDistributionsAppService _budgetDistributionsAppService;

        public IndexModel(IBudgetDistributionsAppService budgetDistributionsAppService)
        {
            _budgetDistributionsAppService = budgetDistributionsAppService;
        }

        public async Task OnGetAsync()
        {
            DepartmentLookupList.AddRange((
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

            BudgetLookupList.AddRange((
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
    }
}