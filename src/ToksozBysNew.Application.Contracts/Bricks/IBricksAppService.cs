using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using ToksozBysNew.Shared;

namespace ToksozBysNew.Bricks
{
    public interface IBricksAppService : IApplicationService
    {
        Task<PagedResultDto<BrickDto>> GetListAsync(GetBricksInput input);

        Task<BrickDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<BrickDto> CreateAsync(BrickCreateDto input);

        Task<BrickDto> UpdateAsync(Guid id, BrickUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(BrickExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}