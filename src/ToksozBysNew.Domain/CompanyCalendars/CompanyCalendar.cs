using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace ToksozBysNew.CompanyCalendars
{
    public class CompanyCalendar : FullAuditedAggregateRoot<Guid>
    {
        public virtual DateTime CompanyCalendarDate { get; set; }

        public virtual bool IsWeekend { get; set; }

        public virtual bool IsHoliday { get; set; }

        public CompanyCalendar()
        {

        }

        public CompanyCalendar(Guid id, DateTime companyCalendarDate, bool isWeekend, bool isHoliday)
        {

            Id = id;
            CompanyCalendarDate = companyCalendarDate;
            IsWeekend = isWeekend;
            IsHoliday = isHoliday;
        }

    }
}