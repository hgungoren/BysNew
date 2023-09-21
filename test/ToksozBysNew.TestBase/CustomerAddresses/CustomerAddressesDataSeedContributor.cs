using ToksozBysNew.Provinces;
using ToksozBysNew.Countries;
using ToksozBysNew.Districts;
using ToksozBysNew.Bricks;
using ToksozBysNew.Doctors;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using ToksozBysNew.CustomerAddresses;

namespace ToksozBysNew.CustomerAddresses
{
    public class CustomerAddressesDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly ICustomerAddressRepository _customerAddressRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly DoctorsDataSeedContributor _doctorsDataSeedContributor;

        private readonly BricksDataSeedContributor _bricksDataSeedContributor;

        private readonly DistrictsDataSeedContributor _districtsDataSeedContributor;

        private readonly CountriesDataSeedContributor _countriesDataSeedContributor;

        private readonly ProvincesDataSeedContributor _provincesDataSeedContributor;

        public CustomerAddressesDataSeedContributor(ICustomerAddressRepository customerAddressRepository, IUnitOfWorkManager unitOfWorkManager, DoctorsDataSeedContributor doctorsDataSeedContributor, BricksDataSeedContributor bricksDataSeedContributor, DistrictsDataSeedContributor districtsDataSeedContributor, CountriesDataSeedContributor countriesDataSeedContributor, ProvincesDataSeedContributor provincesDataSeedContributor)
        {
            _customerAddressRepository = customerAddressRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _doctorsDataSeedContributor = doctorsDataSeedContributor; _bricksDataSeedContributor = bricksDataSeedContributor; _districtsDataSeedContributor = districtsDataSeedContributor; _countriesDataSeedContributor = countriesDataSeedContributor; _provincesDataSeedContributor = provincesDataSeedContributor;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _doctorsDataSeedContributor.SeedAsync(context);
            await _bricksDataSeedContributor.SeedAsync(context);
            await _districtsDataSeedContributor.SeedAsync(context);
            await _countriesDataSeedContributor.SeedAsync(context);
            await _provincesDataSeedContributor.SeedAsync(context);

            await _customerAddressRepository.InsertAsync(new CustomerAddress
            (
                id: Guid.Parse("b3ab3ed9-bc4e-4af2-8969-60ef731c3aa5"),
                address: "cba3211337284fc0ac5c1f7d6fc2d2ac",
                doctorId: null,
                brickId: null,
                districtId: null,
                countryId: null,
                provinceId: null
            ));

            await _customerAddressRepository.InsertAsync(new CustomerAddress
            (
                id: Guid.Parse("f9e6fef0-e413-4534-bba6-f107df3dbb68"),
                address: "87ab8c25559",
                doctorId: null,
                brickId: null,
                districtId: null,
                countryId: null,
                provinceId: null
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}