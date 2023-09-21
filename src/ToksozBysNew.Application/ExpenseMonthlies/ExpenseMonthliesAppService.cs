using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using ToksozBysNew.Permissions;
using ToksozBysNew.ExpenseMonthlies;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using ToksozBysNew.Shared;
using ToksozBysNew.Budgets;

namespace ToksozBysNew.ExpenseMonthlies
{

    [Authorize(ToksozBysNewPermissions.ExpenseMonthlies.Default)]
    public class ExpenseMonthliesAppService : ApplicationService, IExpenseMonthliesAppService
    {
        private readonly IDistributedCache<ExpenseMonthlyExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly IExpenseMonthlyRepository _expenseMonthlyRepository;
        private readonly ExpenseMonthlyManager _expenseMonthlyManager;

        public ExpenseMonthliesAppService(IExpenseMonthlyRepository expenseMonthlyRepository, ExpenseMonthlyManager expenseMonthlyManager, IDistributedCache<ExpenseMonthlyExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _expenseMonthlyRepository = expenseMonthlyRepository;
            _expenseMonthlyManager = expenseMonthlyManager;
        }

        public virtual async Task<PagedResultDto<ExpenseMonthlyDto>> GetListAsync(GetExpenseMonthliesInput input)
        {
            var totalCount = await _expenseMonthlyRepository.GetCountAsync(input.FilterText, input.AccountId, input.AccountGroup, input.Account, input.Department, input.ExpenseType, input.Product, input.Proje, input.Comment, input.Month, input.YearMin, input.YearMax, input.UnitMin, input.UnitMax, input.UnitValueMin, input.UnitValueMax, input.AmountMin, input.AmountMax, input.MemoMin, input.MemoMax, input.Invoice, input.RemainMin, input.RemainMax);
            var items = await _expenseMonthlyRepository.GetListAsync(input.FilterText, input.AccountId, input.AccountGroup, input.Account, input.Department, input.ExpenseType, input.Product, input.Proje, input.Comment, input.Month, input.YearMin, input.YearMax, input.UnitMin, input.UnitMax, input.UnitValueMin, input.UnitValueMax, input.AmountMin, input.AmountMax, input.MemoMin, input.MemoMax, input.Invoice, input.RemainMin, input.RemainMax, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<ExpenseMonthlyDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<ExpenseMonthly>, List<ExpenseMonthlyDto>>(items)
            };
        }

        public virtual async Task<ExpenseMonthlyDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<ExpenseMonthly, ExpenseMonthlyDto>(await _expenseMonthlyRepository.GetAsync(id));
        }

        [Authorize(ToksozBysNewPermissions.ExpenseMonthlies.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _expenseMonthlyRepository.DeleteAsync(id);
        }

        [Authorize(ToksozBysNewPermissions.ExpenseMonthlies.Create)]
        public virtual async Task<ExpenseMonthlyDto> CreateAsync(ExpenseMonthlyCreateDto input)
        {

            var expenseMonthly = await _expenseMonthlyManager.CreateAsync(
            input.AccountId, input.AccountGroup, input.Account, input.Department, input.ExpenseType, input.Product, input.Proje, input.Comment, input.Month, input.Year, input.Unit, input.UnitValue, input.Amount, input.Memo, input.Invoice, input.Remain
            );

            return ObjectMapper.Map<ExpenseMonthly, ExpenseMonthlyDto>(expenseMonthly);
        }

        [Authorize(ToksozBysNewPermissions.ExpenseMonthlies.Edit)]
        public virtual async Task<ExpenseMonthlyDto> UpdateAsync(Guid id, ExpenseMonthlyUpdateDto input)
        {

            var expenseMonthly = await _expenseMonthlyManager.UpdateAsync(
            id,
            input.AccountId, input.AccountGroup, input.Account, input.Department, input.ExpenseType, input.Product, input.Proje, input.Comment, input.Month, input.Year, input.Unit, input.UnitValue, input.Amount, input.Memo, input.Invoice, input.Remain, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<ExpenseMonthly, ExpenseMonthlyDto>(expenseMonthly);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(ExpenseMonthlyExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _expenseMonthlyRepository.GetListAsync(input.FilterText, input.AccountId, input.AccountGroup, input.Account, input.Department, input.ExpenseType, input.Product, input.Proje, input.Comment, input.Month, input.YearMin, input.YearMax, input.UnitMin, input.UnitMax, input.UnitValueMin, input.UnitValueMax, input.AmountMin, input.AmountMax, input.MemoMin, input.MemoMax, input.Invoice, input.RemainMin, input.RemainMax);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<ExpenseMonthly>, List<ExpenseMonthlyExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "ExpenseMonthlies.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new ExpenseMonthlyExcelDownloadTokenCacheItem { Token = token },
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
                });

            return new DownloadTokenResultDto
            {
                Token = token
            };
        }

        public List<ExpenseMonthly> GetExpensesByMonth()
        {
            var data = _expenseMonthlyRepository.GetExpensesMonthly().Result.ToList();

            
            return data;
        }
    }
}