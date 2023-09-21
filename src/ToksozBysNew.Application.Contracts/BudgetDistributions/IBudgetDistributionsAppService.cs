using ToksozBysNew.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using ToksozBysNew.Shared;

namespace ToksozBysNew.BudgetDistributions
{
    public interface IBudgetDistributionsAppService : IApplicationService
    {
        Task<PagedResultDto<BudgetDistributionWithNavigationPropertiesDto>> GetListAsync(GetBudgetDistributionsInput input);

        Task<BudgetDistributionWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<BudgetDistributionDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetDepartmentLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid?>>> GetProductLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid>>> GetBudgetLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid?>>> GetAccountGroupLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid?>>> GetAccountLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid?>>> GetIdentityUserLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<BudgetDistributionDto> CreateAsync(BudgetDistributionCreateDto input);

        Task<BudgetDistributionDto> UpdateAsync(Guid id, BudgetDistributionUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(BudgetDistributionExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}