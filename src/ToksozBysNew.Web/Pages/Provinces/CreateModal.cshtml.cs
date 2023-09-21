using ToksozBysNew.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToksozBysNew.Provinces;

namespace ToksozBysNew.Web.Pages.Provinces
{
    public class CreateModalModel : ToksozBysNewPageModel
    {
        [BindProperty]
        public ProvinceCreateViewModel Province { get; set; }

        public List<SelectListItem> CountryLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(" — ", "")
        };

        private readonly IProvincesAppService _provincesAppService;

        public CreateModalModel(IProvincesAppService provincesAppService)
        {
            _provincesAppService = provincesAppService;
        }

        public async Task OnGetAsync()
        {
            Province = new ProvinceCreateViewModel();
            CountryLookupList.AddRange((
                                    await _provincesAppService.GetCountryLookupAsync(new LookupRequestDto
                                    {
                                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                        );

            await Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostAsync()
        {

            await _provincesAppService.CreateAsync(ObjectMapper.Map<ProvinceCreateViewModel, ProvinceCreateDto>(Province));
            return NoContent();
        }
    }

    public class ProvinceCreateViewModel : ProvinceCreateDto
    {
    }
}