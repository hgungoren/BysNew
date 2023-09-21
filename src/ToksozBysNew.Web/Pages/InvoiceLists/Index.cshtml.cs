using AutoMapper.Internal.Mappers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ToksozBysNew.Invoices;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.ObjectMapping;

namespace ToksozBysNew.Web.Pages.InvoiceLists
{
    public class IndexModel : AbpPageModel
    {
        private readonly IInvoicesAppService _invoiceAppService;
        public IndexModel(IInvoicesAppService invoicesAppService)
        {
            _invoiceAppService = invoicesAppService;
        }
        [BindProperty]
        public InvoiceListViewModel Invoice { get; set; }
        public string InvoiceSerialNoFilter { get; set; }
        public DateTime? InvoiceDateFilterMin { get; set; }
        public DateTime? InvoiceDateFilterMax { get; set; }
        public string NotesFilter { get; set; }
        public DateTime? PaymentDateFilterMin { get; set; }
        public DateTime? PaymentDateFilterMax { get; set; }
        public decimal? AmountFilterMin { get; set; }
        public decimal? AmountFilterMax { get; set; }
        public async Task OnGetAsync()
        { 
            await Task.CompletedTask;
        }
    }
    public class InvoiceListViewModel : InvoiceDto
    { }
}
