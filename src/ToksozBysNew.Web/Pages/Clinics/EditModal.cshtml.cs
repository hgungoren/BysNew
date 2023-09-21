using ToksozBysNew.Shared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using ToksozBysNew.Clinics;

namespace ToksozBysNew.Web.Pages.Clinics
{
    public class EditModalModel : ToksozBysNewPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public ClinicUpdateViewModel Clinic { get; set; }

        public List<SelectListItem> UnitLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(" — ", "")
        };
        public List<SelectListItem> SpecLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(" — ", "")
        };

        private readonly IClinicsAppService _clinicsAppService;

        public EditModalModel(IClinicsAppService clinicsAppService)
        {
            _clinicsAppService = clinicsAppService;
        }

        public async Task OnGetAsync()
        {
            var clinicWithNavigationPropertiesDto = await _clinicsAppService.GetWithNavigationPropertiesAsync(Id);
            Clinic = ObjectMapper.Map<ClinicDto, ClinicUpdateViewModel>(clinicWithNavigationPropertiesDto.Clinic);

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

        }

        public async Task<NoContentResult> OnPostAsync()
        {

            await _clinicsAppService.UpdateAsync(Id, ObjectMapper.Map<ClinicUpdateViewModel, ClinicUpdateDto>(Clinic));
            return NoContent();
        }
    }

    public class ClinicUpdateViewModel : ClinicUpdateDto
    {
    }
}