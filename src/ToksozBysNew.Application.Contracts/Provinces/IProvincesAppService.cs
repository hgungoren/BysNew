using ToksozBysNew.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using ToksozBysNew.Shared;

namespace ToksozBysNew.Provinces
{
    public interface IProvincesAppService : IApplicationService
    {
        Task<PagedResultDto<ProvinceWithNavigationPropertiesDto>> GetListAsync(GetProvincesInput input);

        Task<ProvinceWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<ProvinceDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetCountryLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<ProvinceDto> CreateAsync(ProvinceCreateDto input);

        Task<ProvinceDto> UpdateAsync(Guid id, ProvinceUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(ProvinceExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}