using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace ToksozBysNew.Clinics
{
    public class ClinicDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string ClinicName { get; set; }
        public Guid? UnitId { get; set; }
        public Guid? SpecId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}