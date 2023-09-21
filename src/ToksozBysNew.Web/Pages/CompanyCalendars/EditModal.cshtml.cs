using ToksozBysNew.Shared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using ToksozBysNew.CompanyCalendars;

namespace ToksozBysNew.Web.Pages.CompanyCalendars
{
    public class EditModalModel : ToksozBysNewPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public CompanyCalendarUpdateViewModel CompanyCalendar { get; set; }

        private readonly ICompanyCalendarsAppService _companyCalendarsAppService;

        public EditModalModel(ICompanyCalendarsAppService companyCalendarsAppService)
        {
            _companyCalendarsAppService = companyCalendarsAppService;
        }

        public async Task OnGetAsync()
        {
            var companyCalendar = await _companyCalendarsAppService.GetAsync(Id);
            CompanyCalendar = ObjectMapper.Map<CompanyCalendarDto, CompanyCalendarUpdateViewModel>(companyCalendar);

        }

        public async Task<NoContentResult> OnPostAsync()
        {

            await _companyCalendarsAppService.UpdateAsync(Id, ObjectMapper.Map<CompanyCalendarUpdateViewModel, CompanyCalendarUpdateDto>(CompanyCalendar));
            return NoContent();
        }
    }

    public class CompanyCalendarUpdateViewModel : CompanyCalendarUpdateDto
    {
    }
}