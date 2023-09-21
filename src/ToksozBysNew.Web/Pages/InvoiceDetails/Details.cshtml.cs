using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToksozBysNew.InvoiceDetails;
using ToksozBysNew.Invoices;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using static ToksozBysNew.Web.Pages.InvoiceDetails.IndexModel;

namespace ToksozBysNew.Web.Pages.InvoiceDetails
{
    public class DetailsModel : AbpPageModel
    {
        private readonly IInvoiceDetailsAppService _invoiceDetailsAppService;
        private readonly IInvoicesAppService _invoicesAppService;
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }
        [BindProperty]
        public List<DetailViewModel> InvoiceDetail { get; set; }
        [BindProperty]
        public InvoiceDetailsViewModel Invoice { get; set; }
        public string InvoiceSerialNoFilter { get; set; }
        public DateTime? InvoiceDateFilterMin { get; set; }
        public string NotesFilter { get; set; }
        public DateTime? PaymentDateFilterMin { get; set; }
        public DateTime? PaymentDateFilterMax { get; set; }
        public decimal? AmountFilterMin { get; set; }
        public decimal? AmountFilterMax { get; set; }
        public int? InvoiceDetailQuantityFilterMin { get; set; }

        public int? InvoiceDetailQuantityFilterMax { get; set; }
        public decimal? InvoiceDetailPriceFilterMin { get; set; }

        public decimal? InvoiceDetailPriceFilterMax { get; set; }
        public string InvoiceDetailNoteFilter { get; set; }

        public DateTime? InvoiceDateFilterMax { get; set; }
        public string InvoiceNotesFilter { get; set; }
        public DateTime? InvoicePaymentDateFilterMin { get; set; }

        public DateTime? InvoicePaymentDateFilterMax { get; set; }
        public decimal? InvoiceAmountFilterMin { get; set; }

        public decimal? InvoiceAmountFilterMax { get; set; }

        public DetailsModel(IInvoiceDetailsAppService invoiceDetailsAppService, IInvoicesAppService invoicesAppService)
        {
            _invoiceDetailsAppService = invoiceDetailsAppService;
            _invoicesAppService = invoicesAppService;
        }
        public async Task OnGetAsync(Guid id)
        { 
            var invoice = await _invoicesAppService.GetAsync(id);
            Invoice = ObjectMapper.Map<InvoiceDto, InvoiceDetailsViewModel>(invoice);

            await Task.CompletedTask;
        }
        public class DetailViewModel : InvoiceDetailDto
        {
        }
        public class InvoiceDetailsViewModel : InvoiceDto
        {
        }
    }
}
