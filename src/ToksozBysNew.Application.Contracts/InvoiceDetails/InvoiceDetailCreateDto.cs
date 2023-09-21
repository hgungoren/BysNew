using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ToksozBysNew.InvoiceDetails
{
    public class InvoiceDetailCreateDto
    {
        [Range(InvoiceDetailConsts.InvoiceDetailQuantityMinLength, InvoiceDetailConsts.InvoiceDetailQuantityMaxLength)]
        public int InvoiceDetailQuantity { get; set; }
        [DisplayFormat(DataFormatString = "{0:n}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Currency)]
        public decimal InvoiceDetailPrice { get; set; }
        [Required]
        public string InvoiceDetailNote { get; set; }
        public DateTime InvoiceDetailDate { get; set; }
        [Required] 
        public string Tax { get; set; }
        public string TaxName { get; set; }
        public Guid? InvoiceId { get; set; }
        public Guid TaxListId { get; set; }
    }
}