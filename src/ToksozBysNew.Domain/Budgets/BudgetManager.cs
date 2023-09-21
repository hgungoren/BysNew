using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace ToksozBysNew.Budgets
{
    public class BudgetManager : DomainService
    {
        private readonly IBudgetRepository _budgetRepository;

        public BudgetManager(IBudgetRepository budgetRepository)
        {
            _budgetRepository = budgetRepository;
        }

        public async Task<Budget> CreateAsync(
        Guid? companyId, string budgetName, int year, string comment, bool isActive, DateTime? openUntil = null)
        {
            var budget = new Budget(
             GuidGenerator.Create(),
             companyId, budgetName, year, comment, isActive, openUntil
             );

            return await _budgetRepository.InsertAsync(budget);
        }

        public async Task<Budget> UpdateAsync(
            Guid id,
            Guid? companyId, string budgetName, int year, string comment, bool isActive, DateTime? openUntil = null, [CanBeNull] string concurrencyStamp = null
        )
        {
            var queryable = await _budgetRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var budget = await AsyncExecuter.FirstOrDefaultAsync(query);

            budget.CompanyId = companyId;
            budget.BudgetName = budgetName;
            budget.Year = year;
            budget.Comment = comment;
            budget.IsActive = isActive;
            budget.OpenUntil = openUntil;

            budget.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _budgetRepository.UpdateAsync(budget);
        }

    }
}