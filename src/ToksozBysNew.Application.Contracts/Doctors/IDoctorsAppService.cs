using ToksozBysNew.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using ToksozBysNew.Shared;
using ToksozBysNew.Specs;

namespace ToksozBysNew.Doctors
{
    public interface IDoctorsAppService : IApplicationService
    {
        Task<PagedResultDto<DoctorWithNavigationPropertiesDto>> GetListAsync(GetDoctorsInput input);
        Task<PagedResultDto<DoctorDto>> GetListWithBrickNameByDoctorIdAsync(Guid id);

        Task<DoctorWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<DoctorDto> GetAsync(Guid id);
        Task<SpecDto> GetSpecById(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetPositionLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid>>> GetSpecLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid>>> GetCustomerTitleLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid>>> GetUnitLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid>>> GetCustomerTypeLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<DoctorDto> CreateAsync(DoctorCreateDto input);

        Task<DoctorDto> UpdateAsync(Guid id, DoctorUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(DoctorExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}