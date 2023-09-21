using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace ToksozBysNew.Districts
{
    public class DistrictDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string DistrictName { get; set; }
        public Guid? CountryId { get; set; }
        public Guid? ProvinceId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}