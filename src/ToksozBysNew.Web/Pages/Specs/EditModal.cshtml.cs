using ToksozBysNew.Shared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using ToksozBysNew.Specs;

namespace ToksozBysNew.Web.Pages.Specs
{
    public class EditModalModel : ToksozBysNewPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public SpecUpdateViewModel Spec { get; set; }

        private readonly ISpecsAppService _specsAppService;

        public EditModalModel(ISpecsAppService specsAppService)
        {
            _specsAppService = specsAppService;
        }

        public async Task OnGetAsync()
        {
            var spec = await _specsAppService.GetAsync(Id);
            Spec = ObjectMapper.Map<SpecDto, SpecUpdateViewModel>(spec);

        }

        public async Task<NoContentResult> OnPostAsync()
        {

            await _specsAppService.UpdateAsync(Id, ObjectMapper.Map<SpecUpdateViewModel, SpecUpdateDto>(Spec));
            return NoContent();
        }
    }

    public class SpecUpdateViewModel : SpecUpdateDto
    {
    }
}