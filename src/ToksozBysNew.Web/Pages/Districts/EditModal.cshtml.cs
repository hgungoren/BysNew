using ToksozBysNew.Shared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using ToksozBysNew.Districts;

namespace ToksozBysNew.Web.Pages.Districts
{
    public class EditModalModel : ToksozBysNewPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public DistrictUpdateViewModel District { get; set; }

        public List<SelectListItem> CountryLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(" — ", "")
        };
        public List<SelectListItem> ProvinceLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(" — ", "")
        };

        private readonly IDistrictsAppService _districtsAppService;

        public EditModalModel(IDistrictsAppService districtsAppService)
        {
            _districtsAppService = districtsAppService;
        }

        public async Task OnGetAsync()
        {
            var districtWithNavigationPropertiesDto = await _districtsAppService.GetWithNavigationPropertiesAsync(Id);
            District = ObjectMapper.Map<DistrictDto, DistrictUpdateViewModel>(districtWithNavigationPropertiesDto.District);

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

        }

        public async Task<NoContentResult> OnPostAsync()
        {

            await _districtsAppService.UpdateAsync(Id, ObjectMapper.Map<DistrictUpdateViewModel, DistrictUpdateDto>(District));
            return NoContent();
        }
    }

    public class DistrictUpdateViewModel : DistrictUpdateDto
    {
    }
}