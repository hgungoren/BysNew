using ToksozBysNew.Shared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using ToksozBysNew.Invoices;

namespace ToksozBysNew.Web.Pages.Invoices
{
    public class EditModalModel : ToksozBysNewPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public InvoiceUpdateViewModel Invoice { get; set; }

        private readonly IInvoicesAppService _invoicesAppService;

        public EditModalModel(IInvoicesAppService invoicesAppService)
        {
            _invoicesAppService = invoicesAppService;
        }

        public async Task OnGetAsync()
        {
            var invoice = await _invoicesAppService.GetAsync(Id);
            Invoice = ObjectMapper.Map<InvoiceDto, InvoiceUpdateViewModel>(invoice);

        }

        public async Task<NoContentResult> OnPostAsync()
        {

            await _invoicesAppService.UpdateAsync(Id, ObjectMapper.Map<InvoiceUpdateViewModel, InvoiceUpdateDto>(Invoice));
            return NoContent();
        }
    }

    public class InvoiceUpdateViewModel : InvoiceUpdateDto
    {
    }
}