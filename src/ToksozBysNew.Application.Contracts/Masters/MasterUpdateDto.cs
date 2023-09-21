using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace ToksozBysNew.Masters
{
    public class MasterUpdateDto : IHasConcurrencyStamp
    {
        public string InvoiceSerialNo { get; set; }
        public decimal InvoicePrice { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public string InvoiceNote { get; set; }
        public Guid? CompanyId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}