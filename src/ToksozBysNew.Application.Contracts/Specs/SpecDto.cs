using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace ToksozBysNew.Specs
{
    public class SpecDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string SpecCode { get; set; }
        public string SpecName { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}