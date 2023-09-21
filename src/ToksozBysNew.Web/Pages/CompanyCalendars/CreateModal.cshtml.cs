using ToksozBysNew.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToksozBysNew.CompanyCalendars;

namespace ToksozBysNew.Web.Pages.CompanyCalendars
{
    public class CreateModalModel : ToksozBysNewPageModel
    {
        [BindProperty]
        public CompanyCalendarCreateViewModel CompanyCalendar { get; set; }

        private readonly ICompanyCalendarsAppService _companyCalendarsAppService;

        public CreateModalModel(ICompanyCalendarsAppService companyCalendarsAppService)
        {
            _companyCalendarsAppService = companyCalendarsAppService;
        }

        public async Task OnGetAsync()
        {
            CompanyCalendar = new CompanyCalendarCreateViewModel();

            await Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostAsync()
        {

            await _companyCalendarsAppService.CreateAsync(ObjectMapper.Map<CompanyCalendarCreateViewModel, CompanyCalendarCreateDto>(CompanyCalendar));
            return NoContent();
        }
    }

    public class CompanyCalendarCreateViewModel : CompanyCalendarCreateDto
    {
    }
}