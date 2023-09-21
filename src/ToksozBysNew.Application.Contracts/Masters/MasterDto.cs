using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace ToksozBysNew.Masters
{
    public class MasterDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string InvoiceSerialNo { get; set; }
        public decimal InvoicePrice { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public string InvoiceNote { get; set; }
        public Guid? CompanyId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}