using ToksozBysNew.Provinces;
using ToksozBysNew.Countries;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using ToksozBysNew.Districts;

namespace ToksozBysNew.Districts
{
    public class DistrictsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IDistrictRepository _districtRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly CountriesDataSeedContributor _countriesDataSeedContributor;

        private readonly ProvincesDataSeedContributor _provincesDataSeedContributor;

        public DistrictsDataSeedContributor(IDistrictRepository districtRepository, IUnitOfWorkManager unitOfWorkManager, CountriesDataSeedContributor countriesDataSeedContributor, ProvincesDataSeedContributor provincesDataSeedContributor)
        {
            _districtRepository = districtRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _countriesDataSeedContributor = countriesDataSeedContributor; _provincesDataSeedContributor = provincesDataSeedContributor;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _countriesDataSeedContributor.SeedAsync(context);
            await _provincesDataSeedContributor.SeedAsync(context);

            await _districtRepository.InsertAsync(new District
            (
                id: Guid.Parse("767a4bd1-8029-45ac-a48d-d0cfac57fc9c"),
                districtName: "da18ad44621742dba57f7be",
                countryId: null,
                provinceId: null
            ));

            await _districtRepository.InsertAsync(new District
            (
                id: Guid.Parse("4aff8faf-7f79-4c95-b2ef-1c7710dcf324"),
                districtName: "20554e626f694a528a1720302c879083ea0ea0f9b71349798bff7168acce4bf2731402d209db42b",
                countryId: null,
                provinceId: null
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}