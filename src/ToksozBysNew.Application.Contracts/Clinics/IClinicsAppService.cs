using ToksozBysNew.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using ToksozBysNew.Shared;

namespace ToksozBysNew.Clinics
{
    public interface IClinicsAppService : IApplicationService
    {
        Task<PagedResultDto<ClinicWithNavigationPropertiesDto>> GetListAsync(GetClinicsInput input);

        Task<ClinicWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<ClinicDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetUnitLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid>>> GetSpecLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<ClinicDto> CreateAsync(ClinicCreateDto input);

        Task<ClinicDto> UpdateAsync(Guid id, ClinicUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(ClinicExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}