using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ToksozBysNew.CompanyCalendars
{
    public class CompanyCalendarCreateDto
    {
        public DateTime CompanyCalendarDate { get; set; }
        public bool IsWeekend { get; set; }
        public bool IsHoliday { get; set; }
    }
}