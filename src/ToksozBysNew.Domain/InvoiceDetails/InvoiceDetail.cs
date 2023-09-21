using ToksozBysNew.Invoices;
using ToksozBysNew.TaxLists;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace ToksozBysNew.InvoiceDetails
{
    public class InvoiceDetail : FullAuditedAggregateRoot<Guid>
    {
        public virtual int InvoiceDetailQuantity { get; set; }

        public virtual decimal InvoiceDetailPrice { get; set; }

        [NotNull]
        public virtual string InvoiceDetailNote { get; set; }

        public virtual DateTime InvoiceDetailDate { get; set; }

        public virtual string Tax { get; set; }

        [CanBeNull]
        public virtual string TaxName { get; set; }
        public Guid? InvoiceId { get; set; }
        public Guid TaxListId { get; set; }

        public InvoiceDetail()
        {

        }

        public InvoiceDetail(Guid id, Guid? invoiceId, Guid taxListId, int invoiceDetailQuantity, decimal invoiceDetailPrice, string invoiceDetailNote, DateTime invoiceDetailDate, string taxName, string tax = null)
        {

            Id = id;
            if (invoiceDetailQuantity < InvoiceDetailConsts.InvoiceDetailQuantityMinLength)
            {
                throw new ArgumentOutOfRangeException(nameof(invoiceDetailQuantity), invoiceDetailQuantity, "The value of 'invoiceDetailQuantity' cannot be lower than " + InvoiceDetailConsts.InvoiceDetailQuantityMinLength);
            }

            if (invoiceDetailQuantity > InvoiceDetailConsts.InvoiceDetailQuantityMaxLength)
            {
                throw new ArgumentOutOfRangeException(nameof(invoiceDetailQuantity), invoiceDetailQuantity, "The value of 'invoiceDetailQuantity' cannot be greater than " + InvoiceDetailConsts.InvoiceDetailQuantityMaxLength);
            }

            if (invoiceDetailPrice < InvoiceDetailConsts.InvoiceDetailPriceMinLength)
            {
                throw new ArgumentOutOfRangeException(nameof(invoiceDetailPrice), invoiceDetailPrice, "The value of 'invoiceDetailPrice' cannot be lower than " + InvoiceDetailConsts.InvoiceDetailPriceMinLength);
            }

            if (invoiceDetailPrice > InvoiceDetailConsts.InvoiceDetailPriceMaxLength)
            {
                throw new ArgumentOutOfRangeException(nameof(invoiceDetailPrice), invoiceDetailPrice, "The value of 'invoiceDetailPrice' cannot be greater than " + InvoiceDetailConsts.InvoiceDetailPriceMaxLength);
            }

            Check.NotNull(invoiceDetailNote, nameof(invoiceDetailNote));
            Check.NotNull(tax, nameof(tax));
             

            InvoiceDetailQuantity = invoiceDetailQuantity;
            InvoiceDetailPrice = invoiceDetailPrice;
            InvoiceDetailNote = invoiceDetailNote;
            InvoiceDetailDate = invoiceDetailDate;
            TaxName = taxName;
            Tax = tax;
            InvoiceId = invoiceId;
            TaxListId = taxListId;
        }

    }
}