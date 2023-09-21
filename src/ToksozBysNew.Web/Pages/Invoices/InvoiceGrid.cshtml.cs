using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ToksozBysNew.Invoices;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using static ToksozBysNew.Web.Pages.Invoices.IndexModel;

namespace ToksozBysNew.Web.Pages.Invoices
{
    public class InvoiceGridModel : AbpPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }
        public string InvoiceSerialNo { get; set; }
        public string IsApproved { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public string Notes { get; set; } 
        public string Approval { get; set; } 
        public DateTime? PaymentDate { get; set; }
        public decimal? Amount { get; set; }
        [BindProperty]
        public InvoiceViewModel Invoice { get; set; }

        [BindProperty]
        public DetailViewModel InvoiceDetail { get; set; }

        private readonly IInvoicesAppService _invoicesAppService;

        public InvoiceGridModel(IInvoicesAppService invoicesAppService)
        {
            _invoicesAppService = invoicesAppService;
        }
        public async Task OnGetAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                await Task.CompletedTask;
            }
            else
            {
                var invoice = await _invoicesAppService.GetAsync(id);
                Invoice = ObjectMapper.Map<InvoiceDto, InvoiceViewModel>(invoice);
                if (Invoice.ApprovalStatus==1)
                {
                    Approval = "1. Onay";
                }
                if (Invoice.ApprovalStatus==2)
                {
                    Approval = "Reddedildi";
                }
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var id = await _invoicesAppService.CreateAndGetIdAsync(ObjectMapper.Map<InvoiceViewModel, InvoiceCreateDto>(Invoice));
            Invoice.Id = id;
            IsApproved = "";

            return RedirectToPage("/Invoices/Index", new { id = id });
        }
    }
}
