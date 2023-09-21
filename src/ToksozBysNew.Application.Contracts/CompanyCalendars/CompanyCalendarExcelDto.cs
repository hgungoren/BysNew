using System;

namespace ToksozBysNew.CompanyCalendars
{
    public class CompanyCalendarExcelDto
    {
        public DateTime CompanyCalendarDate { get; set; }
        public bool IsWeekend { get; set; }
        public bool IsHoliday { get; set; }
    }
}