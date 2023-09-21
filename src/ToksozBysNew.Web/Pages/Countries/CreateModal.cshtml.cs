using ToksozBysNew.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToksozBysNew.Countries;

namespace ToksozBysNew.Web.Pages.Countries
{
    public class CreateModalModel : ToksozBysNewPageModel
    {
        [BindProperty]
        public CountryCreateViewModel Country { get; set; }

        private readonly ICountriesAppService _countriesAppService;

        public CreateModalModel(ICountriesAppService countriesAppService)
        {
            _countriesAppService = countriesAppService;
        }

        public async Task OnGetAsync()
        {
            Country = new CountryCreateViewModel();

            await Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostAsync()
        {

            await _countriesAppService.CreateAsync(ObjectMapper.Map<CountryCreateViewModel, CountryCreateDto>(Country));
            return NoContent();
        }
    }

    public class CountryCreateViewModel : CountryCreateDto
    {
    }
}