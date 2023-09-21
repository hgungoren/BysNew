using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace ToksozBysNew.TaxLists
{
    public class TaxListDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string TaxName { get; set; }
        public int TaxValue { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}