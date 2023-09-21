using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace ToksozBysNew.CustomerTypes
{
    public class CustomerTypeDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string TypeName { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}