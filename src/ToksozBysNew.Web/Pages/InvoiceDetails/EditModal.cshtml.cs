using ToksozBysNew.Shared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using ToksozBysNew.InvoiceDetails;
using ToksozBysNew.TaxLists;

namespace ToksozBysNew.Web.Pages.InvoiceDetails
{
    public class EditModalModel : ToksozBysNewPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public InvoiceDetailUpdateViewModel InvoiceDetail { get; set; } 

        public List<SelectListItem> TaxLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(" — ", "")
        };

        public List<SelectListItem> InvoiceLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(" — ", "")
        };

        private readonly IInvoiceDetailsAppService _invoiceDetailsAppService;
        private readonly ITaxListsAppService _taxAppService;

        public EditModalModel(IInvoiceDetailsAppService invoiceDetailsAppService, ITaxListsAppService taxAppService)
        {
            _invoiceDetailsAppService = invoiceDetailsAppService;
            _taxAppService = taxAppService;
        }

        public async Task OnGetAsync()
        { 

            var invoiceDetailWithNavigationPropertiesDto = await _invoiceDetailsAppService.GetWithNavigationPropertiesAsync(Id);
            InvoiceDetail = ObjectMapper.Map<InvoiceDetailDto, InvoiceDetailUpdateViewModel>(invoiceDetailWithNavigationPropertiesDto.InvoiceDetail);

            InvoiceLookupList.AddRange((
                                    await _invoiceDetailsAppService.GetInvoiceLookupAsync(new LookupRequestDto
                                    {
                                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                        );
            TaxLookupList.AddRange((
                                  await _invoiceDetailsAppService.GetTaxListLookupAsync(new LookupRequestDto
                                  {
                                      MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                                  })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                      );

        }

        public async Task<NoContentResult> OnPostAsync()
        { 

            var data = await _taxAppService.GetAsync(InvoiceDetail.TaxListId);
            InvoiceDetail.Tax = data.TaxName;

            await _invoiceDetailsAppService.UpdateAsync(Id, ObjectMapper.Map<InvoiceDetailUpdateViewModel, InvoiceDetailUpdateDto>(InvoiceDetail));
            return NoContent();
        }
    }

    public class InvoiceDetailUpdateViewModel : InvoiceDetailUpdateDto
    { 
    }
}