using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace ToksozBysNew.InvoiceDetails
{
    public class InvoiceDetailUpdateDto : IHasConcurrencyStamp
    {
        [Range(InvoiceDetailConsts.InvoiceDetailQuantityMinLength, InvoiceDetailConsts.InvoiceDetailQuantityMaxLength)]
        public int InvoiceDetailQuantity { get; set; }
        public decimal InvoiceDetailPrice { get; set; }
        [Required]
        public string InvoiceDetailNote { get; set; }
        public DateTime InvoiceDetailDate { get; set; }
        [Required] 
        public string Tax { get; set; }
        public string TaxName { get; set; }
        public Guid? InvoiceId { get; set; }
        public Guid TaxListId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}