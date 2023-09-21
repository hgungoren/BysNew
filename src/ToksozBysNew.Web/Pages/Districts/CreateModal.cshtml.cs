using ToksozBysNew.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToksozBysNew.Districts;

namespace ToksozBysNew.Web.Pages.Districts
{
    public class CreateModalModel : ToksozBysNewPageModel
    {
        [BindProperty]
        public DistrictCreateViewModel District { get; set; }

        public List<SelectListItem> CountryLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(" — ", "")
        };
        public List<SelectListItem> ProvinceLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(" — ", "")
        };

        private readonly IDistrictsAppService _districtsAppService;

        public CreateModalModel(IDistrictsAppService districtsAppService)
        {
            _districtsAppService = districtsAppService;
        }

        public async Task OnGetAsync()
        {
            District = new DistrictCreateViewModel();
            CountryLookupList.AddRange((
                                    await _districtsAppService.GetCountryLookupAsync(new LookupRequestDto
                                    {
                                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                        );
            ProvinceLookupList.AddRange((
                                    await _districtsAppService.GetProvinceLookupAsync(new LookupRequestDto
                                    {
                                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                        );

            await Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostAsync()
        {

            await _districtsAppService.CreateAsync(ObjectMapper.Map<DistrictCreateViewModel, DistrictCreateDto>(District));
            return NoContent();
        }
    }

    public class DistrictCreateViewModel : DistrictCreateDto
    {
    }
}