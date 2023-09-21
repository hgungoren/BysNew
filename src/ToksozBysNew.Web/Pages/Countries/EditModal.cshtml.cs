using ToksozBysNew.Shared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using ToksozBysNew.Countries;

namespace ToksozBysNew.Web.Pages.Countries
{
    public class EditModalModel : ToksozBysNewPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public CountryUpdateViewModel Country { get; set; }

        private readonly ICountriesAppService _countriesAppService;

        public EditModalModel(ICountriesAppService countriesAppService)
        {
            _countriesAppService = countriesAppService;
        }

        public async Task OnGetAsync()
        {
            var country = await _countriesAppService.GetAsync(Id);
            Country = ObjectMapper.Map<CountryDto, CountryUpdateViewModel>(country);

        }

        public async Task<NoContentResult> OnPostAsync()
        {

            await _countriesAppService.UpdateAsync(Id, ObjectMapper.Map<CountryUpdateViewModel, CountryUpdateDto>(Country));
            return NoContent();
        }
    }

    public class CountryUpdateViewModel : CountryUpdateDto
    {
    }
}