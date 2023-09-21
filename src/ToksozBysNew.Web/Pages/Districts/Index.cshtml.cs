using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using ToksozBysNew.Districts;
using ToksozBysNew.Shared;

namespace ToksozBysNew.Web.Pages.Districts
{
    public class IndexModel : AbpPageModel
    {
        public string DistrictNameFilter { get; set; }
        [SelectItems(nameof(CountryLookupList))]
        public Guid? CountryIdFilter { get; set; }
        public List<SelectListItem> CountryLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(string.Empty, "")
        };

        [SelectItems(nameof(ProvinceLookupList))]
        public Guid? ProvinceIdFilter { get; set; }
        public List<SelectListItem> ProvinceLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(string.Empty, "")
        };

        private readonly IDistrictsAppService _districtsAppService;

        public IndexModel(IDistrictsAppService districtsAppService)
        {
            _districtsAppService = districtsAppService;
        }

        public async Task OnGetAsync()
        {
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
    }
}