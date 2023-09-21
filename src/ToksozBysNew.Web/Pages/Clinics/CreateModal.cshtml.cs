using ToksozBysNew.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToksozBysNew.Clinics;

namespace ToksozBysNew.Web.Pages.Clinics
{
    public class CreateModalModel : ToksozBysNewPageModel
    {
        [BindProperty]
        public ClinicCreateViewModel Clinic { get; set; }

        public List<SelectListItem> UnitLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(" — ", "")
        };
        public List<SelectListItem> SpecLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(" — ", "")
        };

        private readonly IClinicsAppService _clinicsAppService;

        public CreateModalModel(IClinicsAppService clinicsAppService)
        {
            _clinicsAppService = clinicsAppService;
        }

        public async Task OnGetAsync()
        {
            Clinic = new ClinicCreateViewModel();
            UnitLookupList.AddRange((
                                    await _clinicsAppService.GetUnitLookupAsync(new LookupRequestDto
                                    {
                                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                        );
            SpecLookupList.AddRange((
                                    await _clinicsAppService.GetSpecLookupAsync(new LookupRequestDto
                                    {
                                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                        );

            await Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostAsync()
        {

            await _clinicsAppService.CreateAsync(ObjectMapper.Map<ClinicCreateViewModel, ClinicCreateDto>(Clinic));
            return NoContent();
        }
    }

    public class ClinicCreateViewModel : ClinicCreateDto
    {
    }
}