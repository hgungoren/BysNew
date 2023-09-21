using ToksozBysNew.Shared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using ToksozBysNew.CustomerTitles;

namespace ToksozBysNew.Web.Pages.CustomerTitles
{
    public class EditModalModel : ToksozBysNewPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public CustomerTitleUpdateViewModel CustomerTitle { get; set; }

        private readonly ICustomerTitlesAppService _customerTitlesAppService;

        public EditModalModel(ICustomerTitlesAppService customerTitlesAppService)
        {
            _customerTitlesAppService = customerTitlesAppService;
        }

        public async Task OnGetAsync()
        {
            var customerTitle = await _customerTitlesAppService.GetAsync(Id);
            CustomerTitle = ObjectMapper.Map<CustomerTitleDto, CustomerTitleUpdateViewModel>(customerTitle);

        }

        public async Task<NoContentResult> OnPostAsync()
        {

            await _customerTitlesAppService.UpdateAsync(Id, ObjectMapper.Map<CustomerTitleUpdateViewModel, CustomerTitleUpdateDto>(CustomerTitle));
            return NoContent();
        }
    }

    public class CustomerTitleUpdateViewModel : CustomerTitleUpdateDto
    {
    }
}