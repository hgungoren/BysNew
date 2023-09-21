using Volo.Abp.Application.Dtos;
using System;

namespace ToksozBysNew.Invoices
{
    public class GetInvoicesInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public string InvoiceSerialNo { get; set; }
        public DateTime? InvoiceDateMin { get; set; }
        public DateTime? InvoiceDateMax { get; set; }
        public string Notes { get; set; }
        public DateTime? PaymentDateMin { get; set; }
        public DateTime? PaymentDateMax { get; set; }
        public decimal? AmountMin { get; set; }
        public decimal? AmountMax { get; set; }
        public int? ApprovalStatusMin { get; set; }
        public int? ApprovalStatusMax { get; set; }

        public GetInvoicesInput()
        {

        }
    }
}