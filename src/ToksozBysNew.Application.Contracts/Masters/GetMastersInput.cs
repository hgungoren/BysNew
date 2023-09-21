using Volo.Abp.Application.Dtos;
using System;

namespace ToksozBysNew.Masters
{
    public class GetMastersInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public string InvoiceSerialNo { get; set; }
        public decimal? InvoicePriceMin { get; set; }
        public decimal? InvoicePriceMax { get; set; }
        public DateTime? InvoiceDateMin { get; set; }
        public DateTime? InvoiceDateMax { get; set; }
        public string InvoiceNote { get; set; }
        public Guid? CompanyId { get; set; }

        public GetMastersInput()
        {

        }
    }
}