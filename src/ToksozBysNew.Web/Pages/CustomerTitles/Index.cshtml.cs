using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using ToksozBysNew.CustomerTitles;
using ToksozBysNew.Shared;

namespace ToksozBysNew.Web.Pages.CustomerTitles
{
    public class IndexModel : AbpPageModel
    {
        public string TitleNameFilter { get; set; }

        private readonly ICustomerTitlesAppService _customerTitlesAppService;

        public IndexModel(ICustomerTitlesAppService customerTitlesAppService)
        {
            _customerTitlesAppService = customerTitlesAppService;
        }

        public async Task OnGetAsync()
        {

            await Task.CompletedTask;
        }
    }
}