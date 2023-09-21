using ToksozBysNew.Shared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using ToksozBysNew.Doctors;

namespace ToksozBysNew.Web.Pages.Doctors
{
    public class EditModalModel : ToksozBysNewPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public DoctorUpdateViewModel Doctor { get; set; }

        public List<SelectListItem> PositionLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(" — ", "")
        };
        public List<SelectListItem> SpecLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(" — ", "")
        };
        public List<SelectListItem> CustomerTitleLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(" — ", "")
        };
        public List<SelectListItem> UnitLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(" — ", "")
        };
        public List<SelectListItem> CustomerTypeLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(" — ", "")
        };

        private readonly IDoctorsAppService _doctorsAppService;

        public EditModalModel(IDoctorsAppService doctorsAppService)
        {
            _doctorsAppService = doctorsAppService;
        }

        public async Task OnGetAsync()
        {
            var doctorWithNavigationPropertiesDto = await _doctorsAppService.GetWithNavigationPropertiesAsync(Id);
            Doctor = ObjectMapper.Map<DoctorDto, DoctorUpdateViewModel>(doctorWithNavigationPropertiesDto.Doctor);

            PositionLookupList.AddRange((
                                    await _doctorsAppService.GetPositionLookupAsync(new LookupRequestDto
                                    {
                                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                        );
            SpecLookupList.AddRange((
                                    await _doctorsAppService.GetSpecLookupAsync(new LookupRequestDto
                                    {
                                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                        );
            CustomerTitleLookupList.AddRange((
                                    await _doctorsAppService.GetCustomerTitleLookupAsync(new LookupRequestDto
                                    {
                                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                        );
            UnitLookupList.AddRange((
                                    await _doctorsAppService.GetUnitLookupAsync(new LookupRequestDto
                                    {
                                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                        );
            CustomerTypeLookupList.AddRange((
                                    await _doctorsAppService.GetCustomerTypeLookupAsync(new LookupRequestDto
                                    {
                                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                        );

        }

        public async Task<NoContentResult> OnPostAsync()
        {

            await _doctorsAppService.UpdateAsync(Id, ObjectMapper.Map<DoctorUpdateViewModel, DoctorUpdateDto>(Doctor));
            return NoContent();
        }
    }

    public class DoctorUpdateViewModel : DoctorUpdateDto
    {
    }
}