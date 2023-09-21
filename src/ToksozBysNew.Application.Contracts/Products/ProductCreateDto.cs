using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ToksozBysNew.Products
{
    public class ProductCreateDto
    {
        [Required]
        [StringLength(ProductConsts.ProductNameMaxLength)]
        public string ProductName { get; set; }
    }
}