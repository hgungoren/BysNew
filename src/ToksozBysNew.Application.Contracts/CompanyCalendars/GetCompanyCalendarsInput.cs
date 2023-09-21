using Volo.Abp.Application.Dtos;
using System;

namespace ToksozBysNew.CompanyCalendars
{
    public class GetCompanyCalendarsInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public DateTime? CompanyCalendarDateMin { get; set; }
        public DateTime? CompanyCalendarDateMax { get; set; }
        public bool? IsWeekend { get; set; }
        public bool? IsHoliday { get; set; }

        public GetCompanyCalendarsInput()
        {

        }
    }
}