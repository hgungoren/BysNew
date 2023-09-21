using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace ToksozBysNew.Invoices
{
    public class InvoiceDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string InvoiceSerialNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string Notes { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }
        public int? ApprovalStatus { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}