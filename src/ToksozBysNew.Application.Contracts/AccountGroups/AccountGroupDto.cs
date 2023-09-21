using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace ToksozBysNew.AccountGroups
{
    public class AccountGroupDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string AccountGroupName { get; set; }
        public bool IsUnitEnterable { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}