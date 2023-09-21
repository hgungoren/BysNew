using Volo.Abp.Application.Dtos;
using System;

namespace ToksozBysNew.InvoiceDetails
{
    public class InvoiceDetailExcelDownloadDto
    {
        public string DownloadToken { get; set; }

        public string FilterText { get; set; }

        public int? InvoiceDetailQuantityMin { get; set; }
        public int? InvoiceDetailQuantityMax { get; set; }
        public decimal? InvoiceDetailPriceMin { get; set; }
        public decimal? InvoiceDetailPriceMax { get; set; }
        public string InvoiceDetailNote { get; set; }
        public DateTime? InvoiceDetailDateMin { get; set; }
        public DateTime? InvoiceDetailDateMax { get; set; }
        public string TaxMin { get; set; }
        public string TaxMax { get; set; }
        public Guid? InvoiceId { get; set; }

        public InvoiceDetailExcelDownloadDto()
        {

        }
    }
}