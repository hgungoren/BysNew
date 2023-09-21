using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace ToksozBysNew.Provinces
{
    public class ProvinceManager : DomainService
    {
        private readonly IProvinceRepository _provinceRepository;

        public ProvinceManager(IProvinceRepository provinceRepository)
        {
            _provinceRepository = provinceRepository;
        }

        public async Task<Province> CreateAsync(
        Guid? countryId, string provinceName)
        {

            var province = new Province(
             GuidGenerator.Create(),
             countryId, provinceName
             );

            return await _provinceRepository.InsertAsync(province);
        }

        public async Task<Province> UpdateAsync(
            Guid id,
            Guid? countryId, string provinceName, [CanBeNull] string concurrencyStamp = null
        )
        {

            var province = await _provinceRepository.GetAsync(id);

            province.CountryId = countryId;
            province.ProvinceName = provinceName;

            province.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _provinceRepository.UpdateAsync(province);
        }

    }
}