using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using ToksozBysNew.Countries;
using ToksozBysNew.Shared;

namespace ToksozBysNew.Web.Pages.Countries
{
    public class IndexModel : AbpPageModel
    {
        public string CountryNameFilter { get; set; }

        private readonly ICountriesAppService _countriesAppService;

        public IndexModel(ICountriesAppService countriesAppService)
        {
            _countriesAppService = countriesAppService;
        }

        public async Task OnGetAsync()
        {

            await Task.CompletedTask;
        }
    }
}