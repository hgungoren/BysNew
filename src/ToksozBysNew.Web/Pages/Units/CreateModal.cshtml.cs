using ToksozBysNew.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToksozBysNew.Units;

namespace ToksozBysNew.Web.Pages.Units
{
    public class CreateModalModel : ToksozBysNewPageModel
    {
        [BindProperty]
        public UnitCreateViewModel Unit { get; set; }

        public List<SelectListItem> BrickLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(" — ", "")
        };

        private readonly IUnitsAppService _unitsAppService;

        public CreateModalModel(IUnitsAppService unitsAppService)
        {
            _unitsAppService = unitsAppService;
        }

        public async Task OnGetAsync()
        {
            Unit = new UnitCreateViewModel();
            BrickLookupList.AddRange((
                                    await _unitsAppService.GetBrickLookupAsync(new LookupRequestDto
                                    {
                                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                        );

            await Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostAsync()
        {

            await _unitsAppService.CreateAsync(ObjectMapper.Map<UnitCreateViewModel, UnitCreateDto>(Unit));
            return NoContent();
        }
    }

    public class UnitCreateViewModel : UnitCreateDto
    {
    }
}