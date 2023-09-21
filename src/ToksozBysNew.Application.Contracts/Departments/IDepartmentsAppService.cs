using ToksozBysNew.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using ToksozBysNew.Shared;

namespace ToksozBysNew.Departments
{
    public interface IDepartmentsAppService : IApplicationService
    {
        Task<PagedResultDto<DepartmentWithNavigationPropertiesDto>> GetListAsync(GetDepartmentsInput input);

        Task<DepartmentWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<DepartmentDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid?>>> GetCompanyLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<DepartmentDto> CreateAsync(DepartmentCreateDto input);

        Task<DepartmentDto> UpdateAsync(Guid id, DepartmentUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(DepartmentExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}