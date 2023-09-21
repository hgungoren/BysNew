using ToksozBysNew.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToksozBysNew.Departments;

namespace ToksozBysNew.Web.Pages.Departments
{
    public class CreateModalModel : ToksozBysNewPageModel
    {
        [BindProperty]
        public DepartmentCreateViewModel Department { get; set; }

        public List<SelectListItem> CompanyLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(" — ", "")
        };

        private readonly IDepartmentsAppService _departmentsAppService;

        public CreateModalModel(IDepartmentsAppService departmentsAppService)
        {
            _departmentsAppService = departmentsAppService;
        }

        public async Task OnGetAsync()
        {
            Department = new DepartmentCreateViewModel();
            CompanyLookupList.AddRange((
                                    await _departmentsAppService.GetCompanyLookupAsync(new LookupRequestDto
                                    {
                                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                        );

            await Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostAsync()
        {

            await _departmentsAppService.CreateAsync(ObjectMapper.Map<DepartmentCreateViewModel, DepartmentCreateDto>(Department));
            return NoContent();
        }
    }

    public class DepartmentCreateViewModel : DepartmentCreateDto
    {
    }
}