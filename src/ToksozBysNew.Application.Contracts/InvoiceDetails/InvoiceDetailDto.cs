using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace ToksozBysNew.InvoiceDetails
{
    public class InvoiceDetailDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public int InvoiceDetailQuantity { get; set; }
        public decimal InvoiceDetailPrice { get; set; }
        public string InvoiceDetailNote { get; set; }
        public DateTime InvoiceDetailDate { get; set; }
        public string Tax { get; set; }
        public string TaxName { get; set; }
        public Guid? InvoiceId { get; set; }
        public Guid TaxListId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}