using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using ToksozBysNew.Shared;

namespace ToksozBysNew.CustomerTitles
{
    public interface ICustomerTitlesAppService : IApplicationService
    {
        Task<PagedResultDto<CustomerTitleDto>> GetListAsync(GetCustomerTitlesInput input);

        Task<CustomerTitleDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<CustomerTitleDto> CreateAsync(CustomerTitleCreateDto input);

        Task<CustomerTitleDto> UpdateAsync(Guid id, CustomerTitleUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(CustomerTitleExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}