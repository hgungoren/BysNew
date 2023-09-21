using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using ToksozBysNew.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace ToksozBysNew.ExpenseMonthlies
{
    public class EfCoreExpenseMonthlyRepository : EfCoreRepository<ToksozBysNewDbContext, ExpenseMonthly, Guid>, IExpenseMonthlyRepository
    {
        public EfCoreExpenseMonthlyRepository(IDbContextProvider<ToksozBysNewDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }
        #region GetListAsync
        public async Task<List<ExpenseMonthly>> GetListAsync(
            string filterText = null,
            string accountId = null,
            string accountGroup = null,
            string account = null,
            string department = null,
            string expenseType = null,
            string product = null,
            string proje = null,
            string comment = null,
            string month = null,
            int? yearMin = null,
            int? yearMax = null,
            int? unitMin = null,
            int? unitMax = null,
            float? unitValueMin = null,
            float? unitValueMax = null,
            float? amountMin = null,
            float? amountMax = null,
            float? memoMin = null,
            float? memoMax = null,
            string invoice = null,
            float? remainMin = null,
            float? remainMax = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, accountId, accountGroup, account, department, expenseType, product, proje, comment, month, yearMin, yearMax, unitMin, unitMax, unitValueMin, unitValueMax, amountMin, amountMax, memoMin, memoMax, invoice, remainMin, remainMax);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ExpenseMonthlyConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }
        #endregion

        #region GetCountAsync
        public async Task<long> GetCountAsync(
            string filterText = null,
            string accountId = null,
            string accountGroup = null,
            string account = null,
            string department = null,
            string expenseType = null,
            string product = null,
            string proje = null,
            string comment = null,
            string month = null,
            int? yearMin = null,
            int? yearMax = null,
            int? unitMin = null,
            int? unitMax = null,
            float? unitValueMin = null,
            float? unitValueMax = null,
            float? amountMin = null,
            float? amountMax = null,
            float? memoMin = null,
            float? memoMax = null,
            string invoice = null,
            float? remainMin = null,
            float? remainMax = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, accountId, accountGroup, account, department, expenseType, product, proje, comment, month, yearMin, yearMax, unitMin, unitMax, unitValueMin, unitValueMax, amountMin, amountMax, memoMin, memoMax, invoice, remainMin, remainMax);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }
        #endregion

        #region ApplyFilter
        protected virtual IQueryable<ExpenseMonthly> ApplyFilter(
            IQueryable<ExpenseMonthly> query,
            string filterText,
            string accountId = null,
            string accountGroup = null,
            string account = null,
            string department = null,
            string expenseType = null,
            string product = null,
            string proje = null,
            string comment = null,
            string month = null,
            int? yearMin = null,
            int? yearMax = null,
            int? unitMin = null,
            int? unitMax = null,
            float? unitValueMin = null,
            float? unitValueMax = null,
            float? amountMin = null,
            float? amountMax = null,
            float? memoMin = null,
            float? memoMax = null,
            string invoice = null,
            float? remainMin = null,
            float? remainMax = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.AccountId.Contains(filterText) || e.AccountGroup.Contains(filterText) || e.Account.Contains(filterText) || e.Department.Contains(filterText) || e.ExpenseType.Contains(filterText) || e.Product.Contains(filterText) || e.Proje.Contains(filterText) || e.Comment.Contains(filterText) || e.Month.Contains(filterText) || e.Invoice.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(accountId), e => e.AccountId.Contains(accountId))
                    .WhereIf(!string.IsNullOrWhiteSpace(accountGroup), e => e.AccountGroup.Contains(accountGroup))
                    .WhereIf(!string.IsNullOrWhiteSpace(account), e => e.Account.Contains(account))
                    .WhereIf(!string.IsNullOrWhiteSpace(department), e => e.Department.Contains(department))
                    .WhereIf(!string.IsNullOrWhiteSpace(expenseType), e => e.ExpenseType.Contains(expenseType))
                    .WhereIf(!string.IsNullOrWhiteSpace(product), e => e.Product.Contains(product))
                    .WhereIf(!string.IsNullOrWhiteSpace(proje), e => e.Proje.Contains(proje))
                    .WhereIf(!string.IsNullOrWhiteSpace(comment), e => e.Comment.Contains(comment))
                    .WhereIf(!string.IsNullOrWhiteSpace(month), e => e.Month.Contains(month))
                    .WhereIf(yearMin.HasValue, e => e.Year >= yearMin.Value)
                    .WhereIf(yearMax.HasValue, e => e.Year <= yearMax.Value)
                    .WhereIf(unitMin.HasValue, e => e.Unit >= unitMin.Value)
                    .WhereIf(unitMax.HasValue, e => e.Unit <= unitMax.Value)
                    .WhereIf(unitValueMin.HasValue, e => e.UnitValue >= unitValueMin.Value)
                    .WhereIf(unitValueMax.HasValue, e => e.UnitValue <= unitValueMax.Value)
                    .WhereIf(amountMin.HasValue, e => e.Amount >= amountMin.Value)
                    .WhereIf(amountMax.HasValue, e => e.Amount <= amountMax.Value)
                    .WhereIf(memoMin.HasValue, e => e.Memo >= memoMin.Value)
                    .WhereIf(memoMax.HasValue, e => e.Memo <= memoMax.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(invoice), e => e.Invoice.Contains(invoice))
                    .WhereIf(remainMin.HasValue, e => e.Remain >= remainMin.Value)
                    .WhereIf(remainMax.HasValue, e => e.Remain <= remainMax.Value);
        }
        #endregion

        private async Task EnsureConnectionOpenAsync()
        {
            var connection = DbContext.Database.GetDbConnection();

            if (connection.State != ConnectionState.Open)
            {
                await connection.OpenAsync();
            }
        }
        private async Task EnsureConnectionCloseAsync()
        {
            var connection = DbContext.Database.GetDbConnection();

            if (connection.State == ConnectionState.Open)
            {
                await connection.CloseAsync();
            }
        }
        private DbCommand CreateCommand(string commandText, CommandType commandType, params SqlParameter[] parameters)
        {
            var command = DbContext.Database.GetDbConnection().CreateCommand();

            command.CommandText = commandText;
            command.CommandType = commandType;
            command.Transaction = DbContext.Database.CurrentTransaction?.GetDbTransaction();

            foreach (var parameter in parameters)
            {
                command.Parameters.Add(parameter);
            }

            return command;
        }
        public async Task<List<ExpenseMonthly>> GetExpensesMonthly()
        {
            await EnsureConnectionOpenAsync();
            using (var command = CreateCommand("GetExpensesMonthly", CommandType.StoredProcedure))
            {
                using (var dataReader = await command.ExecuteReaderAsync())
                {
                    var result = new List<ExpenseMonthly>();

                    while (await dataReader.ReadAsync())
                    {
                        ExpenseMonthly status = new ExpenseMonthly();
                        status.Account = dataReader["Account"].ToString();
                        status.AccountGroup = dataReader["AccountGroup"].ToString();
                        status.AccountId = dataReader["AccountID"].ToString();
                        status.Amount = float.Parse(dataReader["Amount"] == DBNull.Value ? "0" : dataReader["Amount"].ToString(), CultureInfo.InvariantCulture.NumberFormat);
                        status.Comment = dataReader["Comment"].ToString();
                        status.Department = dataReader["Department"].ToString();
                        status.ExpenseType = dataReader["ExpenseType"].ToString();
                        status.Invoice = dataReader["Invoice"] == DBNull.Value ? "0" : dataReader["Invoice"].ToString();
                        status.Memo = float.Parse(dataReader["Memo"] == DBNull.Value ? "0" : dataReader["Memo"].ToString(), CultureInfo.InvariantCulture.NumberFormat);
                        status.Month = dataReader["Month"].ToString();
                        status.Product = dataReader["Product"].ToString();
                        status.Proje = dataReader["Proje"].ToString();
                        status.Remain = float.Parse(dataReader["Remain"] == DBNull.Value ? "0" : dataReader["Remain"].ToString(), CultureInfo.InvariantCulture.NumberFormat);
                        status.Unit = int.Parse(dataReader["Unit"] == DBNull.Value ? "0" : dataReader["Unit"].ToString(), CultureInfo.InvariantCulture.NumberFormat);
                        status.UnitValue = float.Parse(dataReader["UnitValue"] == DBNull.Value ? "0" : dataReader["UnitValue"].ToString(), CultureInfo.InvariantCulture.NumberFormat);
                        status.Year = int.Parse(dataReader["Year"] == DBNull.Value ? "0" : dataReader["Year"].ToString(), CultureInfo.InvariantCulture.NumberFormat);
                        result.Add(status);
                    }

                    var totalCount = result.Count; 

                    await EnsureConnectionCloseAsync();

                    return result;
                }
            }
        }
    }
}