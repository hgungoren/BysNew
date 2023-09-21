using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using ToksozBysNew.Provinces;
using ToksozBysNew.Shared;

namespace ToksozBysNew.Web.Pages.Provinces
{
    public class IndexModel : AbpPageModel
    {
        public string ProvinceNameFilter { get; set; }
        [SelectItems(nameof(CountryLookupList))]
        public Guid? CountryIdFilter { get; set; }
        public List<SelectListItem> CountryLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(string.Empty, "")
        };

        private readonly IProvincesAppService _provincesAppService;

        public IndexModel(IProvincesAppService provincesAppService)
        {
            _provincesAppService = provincesAppService;
        }

        public async Task OnGetAsync()
        {
            CountryLookupList.AddRange((
                    await _provincesAppService.GetCountryLookupAsync(new LookupRequestDto
                    {
                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
            );

            await Task.CompletedTask;
        }
    }
}