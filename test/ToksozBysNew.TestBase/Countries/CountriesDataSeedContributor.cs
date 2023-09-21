using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using ToksozBysNew.Countries;

namespace ToksozBysNew.Countries
{
    public class CountriesDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly ICountryRepository _countryRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public CountriesDataSeedContributor(ICountryRepository countryRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _countryRepository = countryRepository;
            _unitOfWorkManager = unitOfWorkManager;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _countryRepository.InsertAsync(new Country
            (
                id: Guid.Parse("f8fd88e5-a5ef-4908-a8e7-d9b43426521c"),
                countryName: "2fa9d369e48a411bb4cb96e82a68bc1"
            ));

            await _countryRepository.InsertAsync(new Country
            (
                id: Guid.Parse("b1414ea5-7f82-4879-a0c0-a55d06d4376b"),
                countryName: "128d502983dd4c9b94b347589770acdc16fada87e4a34fba90f1c"
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}