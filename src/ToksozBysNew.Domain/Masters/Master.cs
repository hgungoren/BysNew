using ToksozBysNew.Companies;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace ToksozBysNew.Masters
{
    public class Master : FullAuditedAggregateRoot<Guid>
    {
        [CanBeNull]
        public virtual string InvoiceSerialNo { get; set; }

        public virtual decimal InvoicePrice { get; set; }

        public virtual DateTime? InvoiceDate { get; set; }

        [CanBeNull]
        public virtual string InvoiceNote { get; set; }
        public Guid? CompanyId { get; set; }

        public Master()
        {

        }

        public Master(Guid id, Guid? companyId, string invoiceSerialNo, decimal invoicePrice, string invoiceNote, DateTime? invoiceDate = null)
        {

            Id = id;
            InvoiceSerialNo = invoiceSerialNo;
            InvoicePrice = invoicePrice;
            InvoiceNote = invoiceNote;
            InvoiceDate = invoiceDate;
            CompanyId = companyId;
        }

    }
}