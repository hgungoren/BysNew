using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace ToksozBysNew.TaxLists
{
    public class TaxList : FullAuditedAggregateRoot<Guid>
    {
        [CanBeNull]
        public virtual string TaxName { get; set; }

        public virtual int TaxValue { get; set; }

        public TaxList()
        {

        }

        public TaxList(Guid id, string taxName, int taxValue)
        {

            Id = id;
            if (taxValue < TaxListConsts.TaxValueMinLength)
            {
                throw new ArgumentOutOfRangeException(nameof(taxValue), taxValue, "The value of 'taxValue' cannot be lower than " + TaxListConsts.TaxValueMinLength);
            }

            if (taxValue > TaxListConsts.TaxValueMaxLength)
            {
                throw new ArgumentOutOfRangeException(nameof(taxValue), taxValue, "The value of 'taxValue' cannot be greater than " + TaxListConsts.TaxValueMaxLength);
            }

            TaxName = taxName;
            TaxValue = taxValue;
        }

    }
}