using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using ToksozBysNew.EntityFrameworkCore;

namespace ToksozBysNew.Provinces
{
    public class EfCoreProvinceRepository : EfCoreRepository<ToksozBysNewDbContext, Province, Guid>, IProvinceRepository
    {
        public EfCoreProvinceRepository(IDbContextProvider<ToksozBysNewDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<ProvinceWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(province => new ProvinceWithNavigationProperties
                {
                    Province = province,
                    Country = dbContext.Countries.FirstOrDefault(c => c.Id == province.CountryId)
                }).FirstOrDefault();
        }

        public async Task<List<ProvinceWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            string provinceName = null,
            Guid? countryId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, provinceName, countryId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ProvinceConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<ProvinceWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from province in (await GetDbSetAsync())
                   join country in (await GetDbContextAsync()).Countries on province.CountryId equals country.Id into countries
                   from country in countries.DefaultIfEmpty()

                   select new ProvinceWithNavigationProperties
                   {
                       Province = province,
                       Country = country
                   };
        }

        protected virtual IQueryable<ProvinceWithNavigationProperties> ApplyFilter(
            IQueryable<ProvinceWithNavigationProperties> query,
            string filterText,
            string provinceName = null,
            Guid? countryId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Province.ProvinceName.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(provinceName), e => e.Province.ProvinceName.Contains(provinceName))
                    .WhereIf(countryId != null && countryId != Guid.Empty, e => e.Country != null && e.Country.Id == countryId);
        }

        public async Task<List<Province>> GetListAsync(
            string filterText = null,
            string provinceName = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, provinceName);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ProvinceConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string provinceName = null,
            Guid? countryId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, provinceName, countryId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Province> ApplyFilter(
            IQueryable<Province> query,
            string filterText,
            string provinceName = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.ProvinceName.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(provinceName), e => e.ProvinceName.Contains(provinceName));
        }
    }
}