using ToksozBysNew.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using ToksozBysNew.Shared;

namespace ToksozBysNew.Masters
{
    public interface IMastersAppService : IApplicationService
    {
        Task<PagedResultDto<MasterWithNavigationPropertiesDto>> GetListAsync(GetMastersInput input);

        Task<MasterWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<MasterDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetCompanyLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<MasterDto> CreateAsync(MasterCreateDto input);

        Task<MasterDto> UpdateAsync(Guid id, MasterUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(MasterExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}