using ToksozBysNew.Shared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using ToksozBysNew.Positions;

namespace ToksozBysNew.Web.Pages.Positions
{
    public class EditModalModel : ToksozBysNewPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public PositionUpdateViewModel Position { get; set; }

        private readonly IPositionsAppService _positionsAppService;

        public EditModalModel(IPositionsAppService positionsAppService)
        {
            _positionsAppService = positionsAppService;
        }

        public async Task OnGetAsync()
        {
            var position = await _positionsAppService.GetAsync(Id);
            Position = ObjectMapper.Map<PositionDto, PositionUpdateViewModel>(position);

        }

        public async Task<NoContentResult> OnPostAsync()
        {

            await _positionsAppService.UpdateAsync(Id, ObjectMapper.Map<PositionUpdateViewModel, PositionUpdateDto>(Position));
            return NoContent();
        }
    }

    public class PositionUpdateViewModel : PositionUpdateDto
    {
    }
}