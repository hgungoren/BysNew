using Volo.Abp.Application.Dtos;
using System;

namespace ToksozBysNew.InvoiceDetails
{
    public class GetInvoiceDetailsInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public int? InvoiceDetailQuantityMin { get; set; }
        public int? InvoiceDetailQuantityMax { get; set; }
        public decimal? InvoiceDetailPriceMin { get; set; }
        public decimal? InvoiceDetailPriceMax { get; set; }
        public string InvoiceDetailNote { get; set; }
        public DateTime? InvoiceDetailDateMin { get; set; }
        public DateTime? InvoiceDetailDateMax { get; set; }
        public string Tax { get; set; } 
        public string TaxName { get; set; }
        public Guid? InvoiceId { get; set; }
        public Guid? TaxListId { get; set; }

        public GetInvoiceDetailsInput()
        {

        }
    }
}