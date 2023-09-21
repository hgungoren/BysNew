using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using ToksozBysNew.Departments;
using ToksozBysNew.Shared;

namespace ToksozBysNew.Web.Pages.Departments
{
    public class IndexModel : AbpPageModel
    {
        public string DepartmentNameFilter { get; set; }
        [SelectItems(nameof(CompanyLookupList))]
        public Guid? CompanyIdFilter { get; set; }
        public List<SelectListItem> CompanyLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(string.Empty, "")
        };

        private readonly IDepartmentsAppService _departmentsAppService;

        public IndexModel(IDepartmentsAppService departmentsAppService)
        {
            _departmentsAppService = departmentsAppService;
        }

        public async Task OnGetAsync()
        {
            CompanyLookupList.AddRange((
                    await _departmentsAppService.GetCompanyLookupAsync(new LookupRequestDto
                    {
                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
            );

            await Task.CompletedTask;
        }
    }
}