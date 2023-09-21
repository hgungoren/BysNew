using System;

namespace ToksozBysNew.Invoices
{
    public class InvoiceExcelDto
    {
        public string InvoiceSerialNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string Notes { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }
    }
}