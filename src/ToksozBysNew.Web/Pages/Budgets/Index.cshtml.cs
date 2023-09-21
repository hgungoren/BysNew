using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using ToksozBysNew.Budgets;
using ToksozBysNew.Shared;

namespace ToksozBysNew.Web.Pages.Budgets
{
    public class IndexModel : AbpPageModel
    {
        public string BudgetNameFilter { get; set; }
        public int? YearFilterMin { get; set; }

        public int? YearFilterMax { get; set; }
        public string CommentFilter { get; set; }
        [SelectItems(nameof(IsActiveBoolFilterItems))]
        public string IsActiveFilter { get; set; }

        public List<SelectListItem> IsActiveBoolFilterItems { get; set; } =
            new List<SelectListItem>
            {
                new SelectListItem("", ""),
                new SelectListItem("Yes", "true"),
                new SelectListItem("No", "false"),
            };
        public DateTime? OpenUntilFilterMin { get; set; }

        public DateTime? OpenUntilFilterMax { get; set; }
        [SelectItems(nameof(CompanyLookupList))]
        public Guid? CompanyIdFilter { get; set; }
        public List<SelectListItem> CompanyLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(string.Empty, "")
        };

        private readonly IBudgetsAppService _budgetsAppService;

        public IndexModel(IBudgetsAppService budgetsAppService)
        {
            _budgetsAppService = budgetsAppService;
        }

        public async Task OnGetAsync()
        {
            CompanyLookupList.AddRange((
                    await _budgetsAppService.GetCompanyLookupAsync(new LookupRequestDto
                    {
                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
            );

            await Task.CompletedTask;
        }
    }
}