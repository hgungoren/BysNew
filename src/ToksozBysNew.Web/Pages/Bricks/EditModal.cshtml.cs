using ToksozBysNew.Shared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using ToksozBysNew.Bricks;

namespace ToksozBysNew.Web.Pages.Bricks
{
    public class EditModalModel : ToksozBysNewPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public BrickUpdateViewModel Brick { get; set; }

        private readonly IBricksAppService _bricksAppService;

        public EditModalModel(IBricksAppService bricksAppService)
        {
            _bricksAppService = bricksAppService;
        }

        public async Task OnGetAsync()
        {
            var brick = await _bricksAppService.GetAsync(Id);
            Brick = ObjectMapper.Map<BrickDto, BrickUpdateViewModel>(brick);

        }

        public async Task<NoContentResult> OnPostAsync()
        {

            await _bricksAppService.UpdateAsync(Id, ObjectMapper.Map<BrickUpdateViewModel, BrickUpdateDto>(Brick));
            return NoContent();
        }
    }

    public class BrickUpdateViewModel : BrickUpdateDto
    {
    }
}