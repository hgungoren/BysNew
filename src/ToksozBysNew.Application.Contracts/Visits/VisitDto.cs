using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace ToksozBysNew.Visits
{
    public class VisitDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public DateTime VisitDate { get; set; }
        public string VisitNotes { get; set; }
        public Guid? DoctorId { get; set; }
        public Guid? UnitId { get; set; }
        public Guid? ClinicId { get; set; }
        public Guid? BrickId { get; set; }
        public Guid? IdentityUserId { get; set; }
        public Guid? SpecId { get; set; }

        public string ConcurrencyStamp { get; set; }
        public string CustomerName { get; set; }
        public string SpecCode { get; set; }
        public string UnitName { get; set; }
        public Guid? CustomerTypeId { get; set; }
    }
}