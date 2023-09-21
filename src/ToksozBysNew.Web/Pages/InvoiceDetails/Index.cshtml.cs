using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using ToksozBysNew.InvoiceDetails;
using ToksozBysNew.Shared;

namespace ToksozBysNew.Web.Pages.InvoiceDetails
{
    public class IndexModel : AbpPageModel
    {
        public int? InvoiceDetailQuantityFilterMin { get; set; }

        public int? InvoiceDetailQuantityFilterMax { get; set; }
        public decimal? InvoiceDetailPriceFilterMin { get; set; }

        public decimal? InvoiceDetailPriceFilterMax { get; set; }
        public string InvoiceDetailNoteFilter { get; set; }
        public DateTime? InvoiceDetailDateFilterMin { get; set; }

        public DateTime? InvoiceDetailDateFilterMax { get; set; }
        public int? TaxFilterMin { get; set; }

        public int? TaxFilterMax { get; set; }
        [SelectItems(nameof(InvoiceLookupList))]
        public Guid? InvoiceIdFilter { get; set; }
        public List<SelectListItem> InvoiceLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(string.Empty, "")
        };

        private readonly IInvoiceDetailsAppService _invoiceDetailsAppService;

        public IndexModel(IInvoiceDetailsAppService invoiceDetailsAppService)
        {
            _invoiceDetailsAppService = invoiceDetailsAppService;
        }

        public async Task OnGetAsync()
        {
            InvoiceLookupList.AddRange((
                    await _invoiceDetailsAppService.GetInvoiceLookupAsync(new LookupRequestDto
                    {
                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
            );

            await Task.CompletedTask;
        }
    }
}