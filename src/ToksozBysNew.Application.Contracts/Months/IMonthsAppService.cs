using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace ToksozBysNew.Months
{
    public interface IMonthsAppService : IApplicationService
    {
        Task<PagedResultDto<MonthDto>> GetListAsync(GetMonthsInput input);

        Task<MonthDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<MonthDto> CreateAsync(MonthCreateDto input);

        Task<MonthDto> UpdateAsync(Guid id, MonthUpdateDto input);
    }
}