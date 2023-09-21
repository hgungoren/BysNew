using ToksozBysNew.Shared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using ToksozBysNew.Provinces;

namespace ToksozBysNew.Web.Pages.Provinces
{
    public class EditModalModel : ToksozBysNewPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public ProvinceUpdateViewModel Province { get; set; }

        public List<SelectListItem> CountryLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(" — ", "")
        };

        private readonly IProvincesAppService _provincesAppService;

        public EditModalModel(IProvincesAppService provincesAppService)
        {
            _provincesAppService = provincesAppService;
        }

        public async Task OnGetAsync()
        {
            var provinceWithNavigationPropertiesDto = await _provincesAppService.GetWithNavigationPropertiesAsync(Id);
            Province = ObjectMapper.Map<ProvinceDto, ProvinceUpdateViewModel>(provinceWithNavigationPropertiesDto.Province);

            CountryLookupList.AddRange((
                                    await _provincesAppService.GetCountryLookupAsync(new LookupRequestDto
                                    {
                                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                        );

        }

        public async Task<NoContentResult> OnPostAsync()
        {

            await _provincesAppService.UpdateAsync(Id, ObjectMapper.Map<ProvinceUpdateViewModel, ProvinceUpdateDto>(Province));
            return NoContent();
        }
    }

    public class ProvinceUpdateViewModel : ProvinceUpdateDto
    {
    }
}