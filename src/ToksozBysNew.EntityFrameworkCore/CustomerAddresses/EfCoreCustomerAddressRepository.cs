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

namespace ToksozBysNew.CustomerAddresses
{
    public class EfCoreCustomerAddressRepository : EfCoreRepository<ToksozBysNewDbContext, CustomerAddress, Guid>, ICustomerAddressRepository
    {
        public EfCoreCustomerAddressRepository(IDbContextProvider<ToksozBysNewDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<CustomerAddressWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(customerAddress => new CustomerAddressWithNavigationProperties
                {
                    CustomerAddress = customerAddress,
                    Doctor = dbContext.Doctors.FirstOrDefault(c => c.Id == customerAddress.DoctorId),
                    Brick = dbContext.Bricks.FirstOrDefault(c => c.Id == customerAddress.BrickId),
                    District = dbContext.Districts.FirstOrDefault(c => c.Id == customerAddress.DistrictId),
                    Country = dbContext.Countries.FirstOrDefault(c => c.Id == customerAddress.CountryId),
                    Province = dbContext.Provinces.FirstOrDefault(c => c.Id == customerAddress.ProvinceId)
                }).FirstOrDefault();
        }

        public async Task<List<CustomerAddressWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            string address = null,
            Guid? doctorId = null,
            Guid? brickId = null,
            Guid? districtId = null,
            Guid? countryId = null,
            Guid? provinceId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, address, doctorId, brickId, districtId, countryId, provinceId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? CustomerAddressConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<CustomerAddressWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from customerAddress in (await GetDbSetAsync())
                   join doctor in (await GetDbContextAsync()).Doctors on customerAddress.DoctorId equals doctor.Id into doctors
                   from doctor in doctors.DefaultIfEmpty()
                   join brick in (await GetDbContextAsync()).Bricks on customerAddress.BrickId equals brick.Id into bricks
                   from brick in bricks.DefaultIfEmpty()
                   join district in (await GetDbContextAsync()).Districts on customerAddress.DistrictId equals district.Id into districts
                   from district in districts.DefaultIfEmpty()
                   join country in (await GetDbContextAsync()).Countries on customerAddress.CountryId equals country.Id into countries
                   from country in countries.DefaultIfEmpty()
                   join province in (await GetDbContextAsync()).Provinces on customerAddress.ProvinceId equals province.Id into provinces
                   from province in provinces.DefaultIfEmpty()

                   select new CustomerAddressWithNavigationProperties
                   {
                       CustomerAddress = customerAddress,
                       Doctor = doctor,
                       Brick = brick,
                       District = district,
                       Country = country,
                       Province = province
                   };
        }

        protected virtual IQueryable<CustomerAddressWithNavigationProperties> ApplyFilter(
            IQueryable<CustomerAddressWithNavigationProperties> query,
            string filterText,
            string address = null,
            Guid? doctorId = null,
            Guid? brickId = null,
            Guid? districtId = null,
            Guid? countryId = null,
            Guid? provinceId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.CustomerAddress.Address.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(address), e => e.CustomerAddress.Address.Contains(address))
                    .WhereIf(doctorId != null && doctorId != Guid.Empty, e => e.Doctor != null && e.Doctor.Id == doctorId)
                    .WhereIf(brickId != null && brickId != Guid.Empty, e => e.Brick != null && e.Brick.Id == brickId)
                    .WhereIf(districtId != null && districtId != Guid.Empty, e => e.District != null && e.District.Id == districtId)
                    .WhereIf(countryId != null && countryId != Guid.Empty, e => e.Country != null && e.Country.Id == countryId)
                    .WhereIf(provinceId != null && provinceId != Guid.Empty, e => e.Province != null && e.Province.Id == provinceId);
        }

        public async Task<List<CustomerAddress>> GetListAsync(
            string filterText = null,
            string address = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, address);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? CustomerAddressConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string address = null,
            Guid? doctorId = null,
            Guid? brickId = null,
            Guid? districtId = null,
            Guid? countryId = null,
            Guid? provinceId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, address, doctorId, brickId, districtId, countryId, provinceId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<CustomerAddress> ApplyFilter(
            IQueryable<CustomerAddress> query,
            string filterText,
            string address = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Address.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(address), e => e.Address.Contains(address));
        }
    }
}