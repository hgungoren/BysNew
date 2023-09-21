using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace ToksozBysNew.Districts
{
    public class DistrictManager : DomainService
    {
        private readonly IDistrictRepository _districtRepository;

        public DistrictManager(IDistrictRepository districtRepository)
        {
            _districtRepository = districtRepository;
        }

        public async Task<District> CreateAsync(
        Guid? countryId, Guid? provinceId, string districtName)
        {

            var district = new District(
             GuidGenerator.Create(),
             countryId, provinceId, districtName
             );

            return await _districtRepository.InsertAsync(district);
        }

        public async Task<District> UpdateAsync(
            Guid id,
            Guid? countryId, Guid? provinceId, string districtName, [CanBeNull] string concurrencyStamp = null
        )
        {

            var district = await _districtRepository.GetAsync(id);

            district.CountryId = countryId;
            district.ProvinceId = provinceId;
            district.DistrictName = districtName;

            district.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _districtRepository.UpdateAsync(district);
        }

    }
}