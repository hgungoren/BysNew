using ToksozBysNew.Shared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using ToksozBysNew.Visits;

namespace ToksozBysNew.Web.Pages.Visits
{
    public class EditModalModel : ToksozBysNewPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public VisitUpdateViewModel Visit { get; set; }

        public List<SelectListItem> DoctorLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(" — ", "")
        };
        public List<SelectListItem> UnitLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(" — ", "")
        };
        public List<SelectListItem> ClinicLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(" — ", "")
        };
        public List<SelectListItem> BrickLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(" — ", "")
        };
        public List<SelectListItem> IdentityUserLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(" — ", "")
        };
        public List<SelectListItem> SpecLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(" — ", "")
        };

        private readonly IVisitsAppService _visitsAppService;

        public EditModalModel(IVisitsAppService visitsAppService)
        {
            _visitsAppService = visitsAppService;
        }

        public async Task OnGetAsync()
        {
            var visitWithNavigationPropertiesDto = await _visitsAppService.GetWithNavigationPropertiesAsync(Id);
            Visit = ObjectMapper.Map<VisitDto, VisitUpdateViewModel>(visitWithNavigationPropertiesDto.Visit);

            DoctorLookupList.AddRange((
                                    await _visitsAppService.GetDoctorLookupAsync(new LookupRequestDto
                                    {
                                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                        );
            UnitLookupList.AddRange((
                                    await _visitsAppService.GetUnitLookupAsync(new LookupRequestDto
                                    {
                                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                        );
            ClinicLookupList.AddRange((
                                    await _visitsAppService.GetClinicLookupAsync(new LookupRequestDto
                                    {
                                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                        );
            BrickLookupList.AddRange((
                                    await _visitsAppService.GetBrickLookupAsync(new LookupRequestDto
                                    {
                                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                        );
            IdentityUserLookupList.AddRange((
                                    await _visitsAppService.GetIdentityUserLookupAsync(new LookupRequestDto
                                    {
                                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                        );
            SpecLookupList.AddRange((
                                    await _visitsAppService.GetSpecLookupAsync(new LookupRequestDto
                                    {
                                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                        );

        }

        public async Task<NoContentResult> OnPostAsync()
        {

            await _visitsAppService.UpdateAsync(Id, ObjectMapper.Map<VisitUpdateViewModel, VisitUpdateDto>(Visit));
            return NoContent();
        }
    }

    public class VisitUpdateViewModel : VisitUpdateDto
    {
    }
}