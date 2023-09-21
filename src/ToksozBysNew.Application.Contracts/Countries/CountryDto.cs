using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace ToksozBysNew.Countries
{
    public class CountryDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string CountryName { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}