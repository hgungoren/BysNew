using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace ToksozBysNew.Invoices
{
    public class InvoiceUpdateDto : IHasConcurrencyStamp
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