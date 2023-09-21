using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using ToksozBysNew.Shared;

namespace ToksozBysNew.Positions
{
    public interface IPositionsAppService : IApplicationService
    {
        Task<PagedResultDto<PositionDto>> GetListAsync(GetPositionsInput input);

        Task<PositionDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<PositionDto> CreateAsync(PositionCreateDto input);

        Task<PositionDto> UpdateAsync(Guid id, PositionUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(PositionExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}