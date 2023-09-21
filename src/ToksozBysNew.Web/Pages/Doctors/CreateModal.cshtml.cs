using ToksozBysNew.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToksozBysNew.Doctors;

namespace ToksozBysNew.Web.Pages.Doctors
{
    public class CreateModalModel : ToksozBysNewPageModel
    {
        [BindProperty]
        public DoctorCreateViewModel Doctor { get; set; }

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

        public CreateModalModel(IDoctorsAppService doctorsAppService)
        {
            _doctorsAppService = doctorsAppService;
        }

        public async Task OnGetAsync()
        {
            Doctor = new DoctorCreateViewModel();
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

            await Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostAsync()
        {

            await _doctorsAppService.CreateAsync(ObjectMapper.Map<DoctorCreateViewModel, DoctorCreateDto>(Doctor));
            return NoContent();
        }
    }

    public class DoctorCreateViewModel : DoctorCreateDto
    {
    }
}