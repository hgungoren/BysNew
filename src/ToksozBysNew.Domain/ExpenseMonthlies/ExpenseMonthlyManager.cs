using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace ToksozBysNew.ExpenseMonthlies
{
    public class ExpenseMonthlyManager : DomainService
    {
        private readonly IExpenseMonthlyRepository _expenseMonthlyRepository;

        public ExpenseMonthlyManager(IExpenseMonthlyRepository expenseMonthlyRepository)
        {
            _expenseMonthlyRepository = expenseMonthlyRepository;
        }

        public async Task<ExpenseMonthly> CreateAsync(
        string accountId, string accountGroup, string account, string department, string expenseType, string product, string proje, string comment, string month, int year, int unit, float unitValue, float amount, float memo, string invoice, float remain)
        {
            var expenseMonthly = new ExpenseMonthly(
             GuidGenerator.Create(),
             accountId, accountGroup, account, department, expenseType, product, proje, comment, month, year, unit, unitValue, amount, memo, invoice, remain
             );

            return await _expenseMonthlyRepository.InsertAsync(expenseMonthly);
        }

        public async Task<ExpenseMonthly> UpdateAsync(
            Guid id,
            string accountId, string accountGroup, string account, string department, string expenseType, string product, string proje, string comment, string month, int year, int unit, float unitValue, float amount, float memo, string invoice, float remain, [CanBeNull] string concurrencyStamp = null
        )
        {
            var queryable = await _expenseMonthlyRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var expenseMonthly = await AsyncExecuter.FirstOrDefaultAsync(query);

            expenseMonthly.AccountId = accountId;
            expenseMonthly.AccountGroup = accountGroup;
            expenseMonthly.Account = account;
            expenseMonthly.Department = department;
            expenseMonthly.ExpenseType = expenseType;
            expenseMonthly.Product = product;
            expenseMonthly.Proje = proje;
            expenseMonthly.Comment = comment;
            expenseMonthly.Month = month;
            expenseMonthly.Year = year;
            expenseMonthly.Unit = unit;
            expenseMonthly.UnitValue = unitValue;
            expenseMonthly.Amount = amount;
            expenseMonthly.Memo = memo;
            expenseMonthly.Invoice = invoice;
            expenseMonthly.Remain = remain;

            expenseMonthly.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _expenseMonthlyRepository.UpdateAsync(expenseMonthly);
        }

    }
}