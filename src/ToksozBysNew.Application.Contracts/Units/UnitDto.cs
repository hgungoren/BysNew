using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace ToksozBysNew.Units
{
    public class UnitDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string UnitName { get; set; }
        public Guid? BrickId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}