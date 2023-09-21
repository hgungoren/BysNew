using ToksozBysNew.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToksozBysNew.Specs;

namespace ToksozBysNew.Web.Pages.Specs
{
    public class CreateModalModel : ToksozBysNewPageModel
    {
        [BindProperty]
        public SpecCreateViewModel Spec { get; set; }

        private readonly ISpecsAppService _specsAppService;

        public CreateModalModel(ISpecsAppService specsAppService)
        {
            _specsAppService = specsAppService;
        }

        public async Task OnGetAsync()
        {
            Spec = new SpecCreateViewModel();

            await Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostAsync()
        {

            await _specsAppService.CreateAsync(ObjectMapper.Map<SpecCreateViewModel, SpecCreateDto>(Spec));
            return NoContent();
        }
    }

    public class SpecCreateViewModel : SpecCreateDto
    {
    }
}