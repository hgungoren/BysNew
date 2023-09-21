using ToksozBysNew.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using ToksozBysNew.Shared;

namespace ToksozBysNew.VisitDailyActions
{
    public interface IVisitDailyActionsAppService : IApplicationService
    {
        Task<PagedResultDto<VisitDailyActionWithNavigationPropertiesDto>> GetListAsync(GetVisitDailyActionsInput input);
        Task<PagedResultDto<VisitDailyActionDto>> GetCustomListAsync(Guid userId);

        Task<VisitDailyActionWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<VisitDailyActionDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetIdentityUserLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<VisitDailyActionDto> CreateAsync(VisitDailyActionCreateDto input);

        Task<VisitDailyActionDto> UpdateAsync(Guid id, VisitDailyActionUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(VisitDailyActionExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
        //int GetVisitCountByType(Guid typeId,Guid userId,DateTime date);
    }
}