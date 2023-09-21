using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace ToksozBysNew.Positions
{
    public class PositionDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string PositionCode { get; set; }
        public string PositionName { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}