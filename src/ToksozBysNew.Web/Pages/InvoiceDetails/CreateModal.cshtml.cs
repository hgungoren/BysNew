using ToksozBysNew.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToksozBysNew.InvoiceDetails;
using ToksozBysNew.TaxLists;
using Nito.AsyncEx;
using ToksozBysNew.Invoices;

namespace ToksozBysNew.Web.Pages.InvoiceDetails
{
    public class CreateModalModel : ToksozBysNewPageModel
    {
        [BindProperty]
        public InvoiceDetailCreateViewModel InvoiceDetail { get; set; }

        public List<SelectListItem> TaxLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(" — ", "")
        };

        private readonly IInvoiceDetailsAppService _invoiceDetailsAppService;
        private readonly IInvoicesAppService _invoiceAppService;
        private readonly ITaxListsAppService _taxAppService;

        public CreateModalModel(IInvoiceDetailsAppService invoiceDetailsAppService, ITaxListsAppService taxAppService, IInvoicesAppService invoiceAppService)
        {
            _invoiceDetailsAppService = invoiceDetailsAppService;
            _taxAppService = taxAppService;
            _invoiceAppService = invoiceAppService;
        }

        public async Task OnGetAsync(Guid id)
        {

            var data = _invoiceAppService.GetAsync(id);
            InvoiceDetail = new InvoiceDetailCreateViewModel();
            InvoiceDetail.SerialNo = data.Result.InvoiceSerialNo;
            InvoiceDetail.InvId = data.Result.Id;
            

            TaxLookupList.AddRange((
                                   await _invoiceDetailsAppService.GetTaxListLookupAsync(new LookupRequestDto
                                   {
                                       MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                                   })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                       );

            await Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostAsync()
        {

            InvoiceDetail.InvoiceId = InvoiceDetail.InvId; 


            var data = await _taxAppService.GetAsync(InvoiceDetail.TaxListId);
            InvoiceDetail.Tax = data.TaxName;
            await _invoiceDetailsAppService.CreateAsync(ObjectMapper.Map<InvoiceDetailCreateViewModel, InvoiceDetailCreateDto>(InvoiceDetail));
            return NoContent();
        }
    }

    public class InvoiceDetailCreateViewModel : InvoiceDetailCreateDto
    {
        public string SerialNo { get; set; }
        public Guid InvId { get; set; }
    }
}