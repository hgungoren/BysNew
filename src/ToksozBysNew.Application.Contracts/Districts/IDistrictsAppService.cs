using ToksozBysNew.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using ToksozBysNew.Shared;

namespace ToksozBysNew.Districts
{
    public interface IDistrictsAppService : IApplicationService
    {
        Task<PagedResultDto<DistrictWithNavigationPropertiesDto>> GetListAsync(GetDistrictsInput input);

        Task<DistrictWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<DistrictDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetCountryLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid>>> GetProvinceLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<DistrictDto> CreateAsync(DistrictCreateDto input);

        Task<DistrictDto> UpdateAsync(Guid id, DistrictUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(DistrictExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}