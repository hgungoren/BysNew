using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace ToksozBysNew.Products
{
    public class Product : FullAuditedAggregateRoot<Guid>
    {
        [NotNull]
        public virtual string ProductName { get; set; }

        public Product()
        {

        }

        public Product(Guid id, string productName)
        {

            Id = id;
            Check.NotNull(productName, nameof(productName));
            Check.Length(productName, nameof(productName), ProductConsts.ProductNameMaxLength, 0);
            ProductName = productName;
        }

    }
}