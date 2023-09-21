using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace ToksozBysNew.CompanyCalendars
{
    public class CompanyCalendarDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public DateTime CompanyCalendarDate { get; set; }
        public bool IsWeekend { get; set; }
        public bool IsHoliday { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}