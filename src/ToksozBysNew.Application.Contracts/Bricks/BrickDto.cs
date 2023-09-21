using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace ToksozBysNew.Bricks
{
    public class BrickDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string BrickName { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}