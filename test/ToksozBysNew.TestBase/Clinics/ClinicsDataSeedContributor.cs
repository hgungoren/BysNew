using ToksozBysNew.Specs;
using ToksozBysNew.Units;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using ToksozBysNew.Clinics;

namespace ToksozBysNew.Clinics
{
    public class ClinicsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IClinicRepository _clinicRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly UnitsDataSeedContributor _unitsDataSeedContributor;

        private readonly SpecsDataSeedContributor _specsDataSeedContributor;

        public ClinicsDataSeedContributor(IClinicRepository clinicRepository, IUnitOfWorkManager unitOfWorkManager, UnitsDataSeedContributor unitsDataSeedContributor, SpecsDataSeedContributor specsDataSeedContributor)
        {
            _clinicRepository = clinicRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _unitsDataSeedContributor = unitsDataSeedContributor; _specsDataSeedContributor = specsDataSeedContributor;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _unitsDataSeedContributor.SeedAsync(context);
            await _specsDataSeedContributor.SeedAsync(context);

            await _clinicRepository.InsertAsync(new Clinic
            (
                id: Guid.Parse("66b66471-2b93-4826-bdd3-f7ac1cec9a93"),
                clinicName: "460559137ebd4471a4e9cb5a0a4cc15354113f22311c49babbe85e3cb59d40db9f29b16b55d64231a6b665619d",
                unitId: null,
                specId: null
            ));

            await _clinicRepository.InsertAsync(new Clinic
            (
                id: Guid.Parse("2bbd8c96-482b-4815-861a-9a592588ba23"),
                clinicName: "f55b6aca14c04ee3a0ad511b4feb69a94ac0c5fa24db47d98cdc80e1f0b82d4a0685",
                unitId: null,
                specId: null
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}