using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using ToksozBysNew.Shared;

namespace ToksozBysNew.Specs
{
    public interface ISpecsAppService : IApplicationService
    {
        Task<PagedResultDto<SpecDto>> GetListAsync(GetSpecsInput input);

        Task<SpecDto> GetAsync(Guid id); 

        Task DeleteAsync(Guid id);

        Task<SpecDto> CreateAsync(SpecCreateDto input);

        Task<SpecDto> UpdateAsync(Guid id, SpecUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(SpecExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}