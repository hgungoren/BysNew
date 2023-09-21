using ToksozBysNew.Shared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using ToksozBysNew.Departments;

namespace ToksozBysNew.Web.Pages.Departments
{
    public class EditModalModel : ToksozBysNewPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public DepartmentUpdateViewModel Department { get; set; }

        public List<SelectListItem> CompanyLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(" — ", "")
        };

        private readonly IDepartmentsAppService _departmentsAppService;

        public EditModalModel(IDepartmentsAppService departmentsAppService)
        {
            _departmentsAppService = departmentsAppService;
        }

        public async Task OnGetAsync()
        {
            var departmentWithNavigationPropertiesDto = await _departmentsAppService.GetWithNavigationPropertiesAsync(Id);
            Department = ObjectMapper.Map<DepartmentDto, DepartmentUpdateViewModel>(departmentWithNavigationPropertiesDto.Department);

            CompanyLookupList.AddRange((
                                    await _departmentsAppService.GetCompanyLookupAsync(new LookupRequestDto
                                    {
                                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                        );

        }

        public async Task<NoContentResult> OnPostAsync()
        {

            await _departmentsAppService.UpdateAsync(Id, ObjectMapper.Map<DepartmentUpdateViewModel, DepartmentUpdateDto>(Department));
            return NoContent();
        }
    }

    public class DepartmentUpdateViewModel : DepartmentUpdateDto
    {
    }
}