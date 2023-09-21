using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace ToksozBysNew.CompanyCalendars
{
    public class CompanyCalendarUpdateDto : IHasConcurrencyStamp
    {
        public DateTime CompanyCalendarDate { get; set; }
        public bool IsWeekend { get; set; }
        public bool IsHoliday { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}