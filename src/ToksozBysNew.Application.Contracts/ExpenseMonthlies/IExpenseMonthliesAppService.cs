using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using ToksozBysNew.Shared;
using System.Collections.Generic;

namespace ToksozBysNew.ExpenseMonthlies
{
    public interface IExpenseMonthliesAppService : IApplicationService
    {
        Task<PagedResultDto<ExpenseMonthlyDto>> GetListAsync(GetExpenseMonthliesInput input);

        Task<ExpenseMonthlyDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<ExpenseMonthlyDto> CreateAsync(ExpenseMonthlyCreateDto input);

        Task<ExpenseMonthlyDto> UpdateAsync(Guid id, ExpenseMonthlyUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(ExpenseMonthlyExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync(); 
    }
}