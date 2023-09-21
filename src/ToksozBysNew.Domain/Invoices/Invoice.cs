using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace ToksozBysNew.Invoices
{
    public class Invoice : FullAuditedAggregateRoot<Guid>
    {
        [CanBeNull]
        public virtual string InvoiceSerialNo { get; set; }

        public virtual DateTime InvoiceDate { get; set; }

        [CanBeNull]
        public virtual string Notes { get; set; }

        public virtual DateTime PaymentDate { get; set; }

        public virtual decimal Amount { get; set; }

        public virtual int? ApprovalStatus { get; set; }

        public Invoice()
        {

        }

        public Invoice(Guid id, string invoiceSerialNo, DateTime invoiceDate, string notes, DateTime paymentDate, decimal amount, int? approvalStatus = null)
        {

            Id = id;
            InvoiceSerialNo = invoiceSerialNo;
            InvoiceDate = invoiceDate;
            Notes = notes;
            PaymentDate = paymentDate;
            Amount = amount;
            ApprovalStatus = approvalStatus;
        }

    }
}