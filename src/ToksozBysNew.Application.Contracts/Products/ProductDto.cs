using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace ToksozBysNew.Products
{
    public class ProductDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string ProductName { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}