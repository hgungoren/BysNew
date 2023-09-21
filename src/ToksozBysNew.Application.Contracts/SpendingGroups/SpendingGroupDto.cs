using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace ToksozBysNew.SpendingGroups
{
    public class SpendingGroupDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string Name { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}