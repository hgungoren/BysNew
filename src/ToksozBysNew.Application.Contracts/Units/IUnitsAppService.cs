using ToksozBysNew.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using ToksozBysNew.Shared;

namespace ToksozBysNew.Units
{
    public interface IUnitsAppService : IApplicationService
    {
        Task<PagedResultDto<UnitWithNavigationPropertiesDto>> GetListAsync(GetUnitsInput input);

        Task<UnitWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<UnitDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetBrickLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<UnitDto> CreateAsync(UnitCreateDto input);

        Task<UnitDto> UpdateAsync(Guid id, UnitUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(UnitExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}