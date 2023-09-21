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

namespace ToksozBysNew.Districts
{
    public class EfCoreDistrictRepository : EfCoreRepository<ToksozBysNewDbContext, District, Guid>, IDistrictRepository
    {
        public EfCoreDistrictRepository(IDbContextProvider<ToksozBysNewDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<DistrictWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(district => new DistrictWithNavigationProperties
                {
                    District = district,
                    Country = dbContext.Countries.FirstOrDefault(c => c.Id == district.CountryId),
                    Province = dbContext.Provinces.FirstOrDefault(c => c.Id == district.ProvinceId)
                }).FirstOrDefault();
        }

        public async Task<List<DistrictWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            string districtName = null,
            Guid? countryId = null,
            Guid? provinceId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, districtName, countryId, provinceId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? DistrictConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<DistrictWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from district in (await GetDbSetAsync())
                   join country in (await GetDbContextAsync()).Countries on district.CountryId equals country.Id into countries
                   from country in countries.DefaultIfEmpty()
                   join province in (await GetDbContextAsync()).Provinces on district.ProvinceId equals province.Id into provinces
                   from province in provinces.DefaultIfEmpty()

                   select new DistrictWithNavigationProperties
                   {
                       District = district,
                       Country = country,
                       Province = province
                   };
        }

        protected virtual IQueryable<DistrictWithNavigationProperties> ApplyFilter(
            IQueryable<DistrictWithNavigationProperties> query,
            string filterText,
            string districtName = null,
            Guid? countryId = null,
            Guid? provinceId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.District.DistrictName.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(districtName), e => e.District.DistrictName.Contains(districtName))
                    .WhereIf(countryId != null && countryId != Guid.Empty, e => e.Country != null && e.Country.Id == countryId)
                    .WhereIf(provinceId != null && provinceId != Guid.Empty, e => e.Province != null && e.Province.Id == provinceId);
        }

        public async Task<List<District>> GetListAsync(
            string filterText = null,
            string districtName = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, districtName);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? DistrictConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string districtName = null,
            Guid? countryId = null,
            Guid? provinceId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, districtName, countryId, provinceId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<District> ApplyFilter(
            IQueryable<District> query,
            string filterText,
            string districtName = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.DistrictName.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(districtName), e => e.DistrictName.Contains(districtName));
        }
    }
}