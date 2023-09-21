using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace ToksozBysNew.Countries
{
    public class CountryManager : DomainService
    {
        private readonly ICountryRepository _countryRepository;

        public CountryManager(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public async Task<Country> CreateAsync(
        string countryName)
        {

            var country = new Country(
             GuidGenerator.Create(),
             countryName
             );

            return await _countryRepository.InsertAsync(country);
        }

        public async Task<Country> UpdateAsync(
            Guid id,
            string countryName, [CanBeNull] string concurrencyStamp = null
        )
        {

            var country = await _countryRepository.GetAsync(id);

            country.CountryName = countryName;

            country.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _countryRepository.UpdateAsync(country);
        }

    }
}