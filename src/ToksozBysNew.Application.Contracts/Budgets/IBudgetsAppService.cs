using ToksozBysNew.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using ToksozBysNew.Shared;

namespace ToksozBysNew.Budgets
{
    public interface IBudgetsAppService : IApplicationService
    {
        Task<PagedResultDto<BudgetWithNavigationPropertiesDto>> GetListAsync(GetBudgetsInput input);

        Task<BudgetWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<BudgetDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid?>>> GetCompanyLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<BudgetDto> CreateAsync(BudgetCreateDto input);

        Task<BudgetDto> UpdateAsync(Guid id, BudgetUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(BudgetExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}