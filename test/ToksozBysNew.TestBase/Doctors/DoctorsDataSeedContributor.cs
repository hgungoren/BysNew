using ToksozBysNew.CustomerTypes;
using ToksozBysNew.Units;
using ToksozBysNew.CustomerTitles;
using ToksozBysNew.Specs;
using ToksozBysNew.Positions;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using ToksozBysNew.Doctors;

namespace ToksozBysNew.Doctors
{
    public class DoctorsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly PositionsDataSeedContributor _positionsDataSeedContributor;

        private readonly SpecsDataSeedContributor _specsDataSeedContributor;

        private readonly CustomerTitlesDataSeedContributor _customerTitlesDataSeedContributor;

        private readonly UnitsDataSeedContributor _unitsDataSeedContributor;

        private readonly CustomerTypesDataSeedContributor _customerTypesDataSeedContributor;

        public DoctorsDataSeedContributor(IDoctorRepository doctorRepository, IUnitOfWorkManager unitOfWorkManager, PositionsDataSeedContributor positionsDataSeedContributor, SpecsDataSeedContributor specsDataSeedContributor, CustomerTitlesDataSeedContributor customerTitlesDataSeedContributor, UnitsDataSeedContributor unitsDataSeedContributor, CustomerTypesDataSeedContributor customerTypesDataSeedContributor)
        {
            _doctorRepository = doctorRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _positionsDataSeedContributor = positionsDataSeedContributor; _specsDataSeedContributor = specsDataSeedContributor; _customerTitlesDataSeedContributor = customerTitlesDataSeedContributor; _unitsDataSeedContributor = unitsDataSeedContributor; _customerTypesDataSeedContributor = customerTypesDataSeedContributor;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _positionsDataSeedContributor.SeedAsync(context);
            await _specsDataSeedContributor.SeedAsync(context);
            await _customerTitlesDataSeedContributor.SeedAsync(context);
            await _unitsDataSeedContributor.SeedAsync(context);
            await _customerTypesDataSeedContributor.SeedAsync(context);

            await _doctorRepository.InsertAsync(new Doctor
            (
                id: Guid.Parse("a35e966e-23d5-425b-af6f-ad30e3181f8e"),
                isActive: true,
                nameSurname: "03ff46938aa54fbea1c44a353",
                pharmacyName: "2b7cc7a157d44206b8653c47e5bbeba2a375ec5b804547d997e7cefc65aa22e14b023a4",
                positionId: null,
                specId: null,
                customerTitleId: null,
                unitId: null,
                customerTypeId: null
            ));

            await _doctorRepository.InsertAsync(new Doctor
            (
                id: Guid.Parse("7ee8c5ac-96d4-495a-871c-b0bb9f003a62"),
                isActive: true,
                nameSurname: "b69f86757e1e42598",
                pharmacyName: "2b9db55a853d488f8bef841283d1dafb9b1e02bdab7c47618eb171ee5876c65db2984c8f6201464395354e2896d5cacd",
                positionId: null,
                specId: null,
                customerTitleId: null,
                unitId: null,
                customerTypeId: null
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}