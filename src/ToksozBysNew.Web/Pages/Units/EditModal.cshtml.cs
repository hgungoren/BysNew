using ToksozBysNew.Shared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using ToksozBysNew.Units;

namespace ToksozBysNew.Web.Pages.Units
{
    public class EditModalModel : ToksozBysNewPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public UnitUpdateViewModel Unit { get; set; }

        public List<SelectListItem> BrickLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(" — ", "")
        };

        private readonly IUnitsAppService _unitsAppService;

        public EditModalModel(IUnitsAppService unitsAppService)
        {
            _unitsAppService = unitsAppService;
        }

        public async Task OnGetAsync()
        {
            var unitWithNavigationPropertiesDto = await _unitsAppService.GetWithNavigationPropertiesAsync(Id);
            Unit = ObjectMapper.Map<UnitDto, UnitUpdateViewModel>(unitWithNavigationPropertiesDto.Unit);

            BrickLookupList.AddRange((
                                    await _unitsAppService.GetBrickLookupAsync(new LookupRequestDto
                                    {
                                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                        );

        }

        public async Task<NoContentResult> OnPostAsync()
        {

            await _unitsAppService.UpdateAsync(Id, ObjectMapper.Map<UnitUpdateViewModel, UnitUpdateDto>(Unit));
            return NoContent();
        }
    }

    public class UnitUpdateViewModel : UnitUpdateDto
    {
    }
}