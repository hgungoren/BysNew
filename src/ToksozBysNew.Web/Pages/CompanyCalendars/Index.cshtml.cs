using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using ToksozBysNew.CompanyCalendars;
using ToksozBysNew.Shared;

namespace ToksozBysNew.Web.Pages.CompanyCalendars
{
    public class IndexModel : AbpPageModel
    {
        public DateTime? CompanyCalendarDateFilterMin { get; set; }

        public DateTime? CompanyCalendarDateFilterMax { get; set; }
        [SelectItems(nameof(IsWeekendBoolFilterItems))]
        public string IsWeekendFilter { get; set; }

        public List<SelectListItem> IsWeekendBoolFilterItems { get; set; } =
            new List<SelectListItem>
            {
                new SelectListItem("", ""),
                new SelectListItem("Yes", "true"),
                new SelectListItem("No", "false"),
            };
        [SelectItems(nameof(IsHolidayBoolFilterItems))]
        public string IsHolidayFilter { get; set; }

        public List<SelectListItem> IsHolidayBoolFilterItems { get; set; } =
            new List<SelectListItem>
            {
                new SelectListItem("", ""),
                new SelectListItem("Yes", "true"),
                new SelectListItem("No", "false"),
            };

        private readonly ICompanyCalendarsAppService _companyCalendarsAppService;

        public IndexModel(ICompanyCalendarsAppService companyCalendarsAppService)
        {
            _companyCalendarsAppService = companyCalendarsAppService;
        }

        public async Task OnGetAsync()
        {

            await Task.CompletedTask;
        }
    }
}