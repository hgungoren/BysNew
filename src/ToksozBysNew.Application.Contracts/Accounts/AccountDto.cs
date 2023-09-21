using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace ToksozBysNew.Accounts
{
    public class AccountDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string AccountCode { get; set; }
        public string AccountName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}