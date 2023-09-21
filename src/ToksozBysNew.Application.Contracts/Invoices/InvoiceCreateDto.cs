using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ToksozBysNew.Invoices
{
    public class InvoiceCreateDto
    {
        public string InvoiceSerialNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string Notes { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }
        public int? ApprovalStatus { get; set; }
    }
}