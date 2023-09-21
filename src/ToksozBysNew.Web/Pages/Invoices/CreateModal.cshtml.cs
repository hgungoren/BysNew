using ToksozBysNew.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToksozBysNew.Invoices;

namespace ToksozBysNew.Web.Pages.Invoices
{
    public class CreateModalModel : ToksozBysNewPageModel
    {
        [BindProperty]
        public InvoiceCreateViewModel Invoice { get; set; }

        private readonly IInvoicesAppService _invoicesAppService;

        public CreateModalModel(IInvoicesAppService invoicesAppService)
        {
            _invoicesAppService = invoicesAppService;
        }

        public async Task OnGetAsync()
        {
            Invoice = new InvoiceCreateViewModel();

            await Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostAsync()
        {

            await _invoicesAppService.CreateAsync(ObjectMapper.Map<InvoiceCreateViewModel, InvoiceCreateDto>(Invoice));
            return NoContent();
        }
    }

    public class InvoiceCreateViewModel : InvoiceCreateDto
    {
    }
}