using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace ToksozBysNew.VisitDailyActions
{
    public class VisitDailyActionDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public DateTime VisitDailyDate { get; set; }
        public decimal VisitDaily1 { get; set; }
        public decimal VisitDaily2 { get; set; }
        public decimal VisitDaily3 { get; set; }
        public decimal VisitDaily4 { get; set; }
        public decimal VisitDaily5 { get; set; }
        public decimal VisitDaily6 { get; set; }
        public decimal VisitDaily7 { get; set; }
        public decimal VisitDaily8 { get; set; }
        public decimal VisitDaily9 { get; set; }
        public decimal VisitDaily10 { get; set; }
        public decimal VisitDaily11 { get; set; }
        public decimal VisitDaily12 { get; set; }
        public decimal VisitDaily13 { get; set; }
        public decimal VisitDaily14 { get; set; }
        public decimal VisitDaily15 { get; set; }
        public DateTime VisitDailyCloseDate { get; set; }
        public string VisitDailyNote { get; set; }
        public Guid? IdentityUserId { get; set; }

        public string ConcurrencyStamp { get; set; }
        public DateTime CompanyCalendarDate { get; set; }
        public bool IsHoliday { get; set; }
        public bool IsWeekend { get; set; }
        public int DoctorVisitCount { get; set; }
        public int PharmaVisitCount { get; set; }
    }
}