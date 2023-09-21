using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace ToksozBysNew.CustomerTitles
{
    public class CustomerTitleDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string TitleName { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}