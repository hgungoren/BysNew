using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace ToksozBysNew.Products
{
    public class ProductUpdateDto : IHasConcurrencyStamp
    {
        [Required]
        [StringLength(ProductConsts.ProductNameMaxLength)]
        public string ProductName { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}