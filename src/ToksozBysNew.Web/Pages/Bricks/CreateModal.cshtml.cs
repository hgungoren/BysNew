using ToksozBysNew.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToksozBysNew.Bricks;

namespace ToksozBysNew.Web.Pages.Bricks
{
    public class CreateModalModel : ToksozBysNewPageModel
    {
        [BindProperty]
        public BrickCreateViewModel Brick { get; set; }

        private readonly IBricksAppService _bricksAppService;

        public CreateModalModel(IBricksAppService bricksAppService)
        {
            _bricksAppService = bricksAppService;
        }

        public async Task OnGetAsync()
        {
            Brick = new BrickCreateViewModel();

            await Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostAsync()
        {

            await _bricksAppService.CreateAsync(ObjectMapper.Map<BrickCreateViewModel, BrickCreateDto>(Brick));
            return NoContent();
        }
    }

    public class BrickCreateViewModel : BrickCreateDto
    {
    }
}