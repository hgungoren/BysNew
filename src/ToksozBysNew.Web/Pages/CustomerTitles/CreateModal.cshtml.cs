using ToksozBysNew.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToksozBysNew.CustomerTitles;

namespace ToksozBysNew.Web.Pages.CustomerTitles
{
    public class CreateModalModel : ToksozBysNewPageModel
    {
        [BindProperty]
        public CustomerTitleCreateViewModel CustomerTitle { get; set; }

        private readonly ICustomerTitlesAppService _customerTitlesAppService;

        public CreateModalModel(ICustomerTitlesAppService customerTitlesAppService)
        {
            _customerTitlesAppService = customerTitlesAppService;
        }

        public async Task OnGetAsync()
        {
            CustomerTitle = new CustomerTitleCreateViewModel();

            await Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostAsync()
        {

            await _customerTitlesAppService.CreateAsync(ObjectMapper.Map<CustomerTitleCreateViewModel, CustomerTitleCreateDto>(CustomerTitle));
            return NoContent();
        }
    }

    public class CustomerTitleCreateViewModel : CustomerTitleCreateDto
    {
    }
}