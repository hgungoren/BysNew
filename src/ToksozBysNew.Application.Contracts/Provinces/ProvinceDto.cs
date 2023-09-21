using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace ToksozBysNew.Provinces
{
    public class ProvinceDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string ProvinceName { get; set; }
        public Guid? CountryId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}