using ToksozBysNew.Countries;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using ToksozBysNew.Provinces;

namespace ToksozBysNew.Provinces
{
    public class ProvincesDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IProvinceRepository _provinceRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly CountriesDataSeedContributor _countriesDataSeedContributor;

        public ProvincesDataSeedContributor(IProvinceRepository provinceRepository, IUnitOfWorkManager unitOfWorkManager, CountriesDataSeedContributor countriesDataSeedContributor)
        {
            _provinceRepository = provinceRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _countriesDataSeedContributor = countriesDataSeedContributor;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _countriesDataSeedContributor.SeedAsync(context);

            await _provinceRepository.InsertAsync(new Province
            (
                id: Guid.Parse("2733dc16-33e4-46b3-980f-a904ea0b38f5"),
                provinceName: "0d6804024686465eba04072d1d81",
                countryId: null
            ));

            await _provinceRepository.InsertAsync(new Province
            (
                id: Guid.Parse("ddbc210d-7497-4970-a91b-8c4f113aaf2d"),
                provinceName: "32ae2136716f4693a25548928096c0abb285264272e94d658ad98bea9ec13b153e54a6108f964e948fa4343a7e6e4",
                countryId: null
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}