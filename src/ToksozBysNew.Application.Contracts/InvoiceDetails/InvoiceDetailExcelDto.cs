using System;

namespace ToksozBysNew.InvoiceDetails
{
    public class InvoiceDetailExcelDto
    {
        public int InvoiceDetailQuantity { get; set; }
        public decimal InvoiceDetailPrice { get; set; }
        public string InvoiceDetailNote { get; set; }
        public DateTime InvoiceDetailDate { get; set; }
        public string Tax { get; set; }
    }
}