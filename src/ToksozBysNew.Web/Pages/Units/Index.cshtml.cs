using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using ToksozBysNew.Units;
using ToksozBysNew.Shared;

namespace ToksozBysNew.Web.Pages.Units
{
    public class IndexModel : AbpPageModel
    {
        public string UnitNameFilter { get; set; }
        [SelectItems(nameof(BrickLookupList))]
        public Guid? BrickIdFilter { get; set; }
        public List<SelectListItem> BrickLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(string.Empty, "")
        };

        private readonly IUnitsAppService _unitsAppService;

        public IndexModel(IUnitsAppService unitsAppService)
        {
            _unitsAppService = unitsAppService;
        }

        public async Task OnGetAsync()
        {
            BrickLookupList.AddRange((
                    await _unitsAppService.GetBrickLookupAsync(new LookupRequestDto
                    {
                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
            );

            await Task.CompletedTask;
        }
    }
}