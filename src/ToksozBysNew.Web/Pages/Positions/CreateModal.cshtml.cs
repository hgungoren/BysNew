using ToksozBysNew.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToksozBysNew.Positions;

namespace ToksozBysNew.Web.Pages.Positions
{
    public class CreateModalModel : ToksozBysNewPageModel
    {
        [BindProperty]
        public PositionCreateViewModel Position { get; set; }

        private readonly IPositionsAppService _positionsAppService;

        public CreateModalModel(IPositionsAppService positionsAppService)
        {
            _positionsAppService = positionsAppService;
        }

        public async Task OnGetAsync()
        {
            Position = new PositionCreateViewModel();

            await Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostAsync()
        {

            await _positionsAppService.CreateAsync(ObjectMapper.Map<PositionCreateViewModel, PositionCreateDto>(Position));
            return NoContent();
        }
    }

    public class PositionCreateViewModel : PositionCreateDto
    {
    }
}