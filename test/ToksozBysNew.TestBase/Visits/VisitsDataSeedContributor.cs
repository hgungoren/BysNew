using ToksozBysNew.Specs;
using ToksozBysNew.Bricks;
using ToksozBysNew.Clinics;
using ToksozBysNew.Units;
using ToksozBysNew.Doctors;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using ToksozBysNew.Visits;

namespace ToksozBysNew.Visits
{
    public class VisitsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IVisitRepository _visitRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly DoctorsDataSeedContributor _doctorsDataSeedContributor;

        private readonly UnitsDataSeedContributor _unitsDataSeedContributor;

        private readonly ClinicsDataSeedContributor _clinicsDataSeedContributor;

        private readonly BricksDataSeedContributor _bricksDataSeedContributor;

        private readonly SpecsDataSeedContributor _specsDataSeedContributor;

        public VisitsDataSeedContributor(IVisitRepository visitRepository, IUnitOfWorkManager unitOfWorkManager, DoctorsDataSeedContributor doctorsDataSeedContributor, UnitsDataSeedContributor unitsDataSeedContributor, ClinicsDataSeedContributor clinicsDataSeedContributor, BricksDataSeedContributor bricksDataSeedContributor, SpecsDataSeedContributor specsDataSeedContributor)
        {
            _visitRepository = visitRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _doctorsDataSeedContributor = doctorsDataSeedContributor; _unitsDataSeedContributor = unitsDataSeedContributor; _clinicsDataSeedContributor = clinicsDataSeedContributor; _bricksDataSeedContributor = bricksDataSeedContributor; _specsDataSeedContributor = specsDataSeedContributor;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _doctorsDataSeedContributor.SeedAsync(context);
            await _unitsDataSeedContributor.SeedAsync(context);
            await _clinicsDataSeedContributor.SeedAsync(context);
            await _bricksDataSeedContributor.SeedAsync(context);
            await _specsDataSeedContributor.SeedAsync(context);

            await _visitRepository.InsertAsync(new Visit
            (
                id: Guid.Parse("a0fd5129-a0d6-411e-b509-9371bf807aa0"),
                visitDate: new DateTime(2019, 7, 2),
                visitNotes: "df83fd091b8c45b28c70a3a49",
                doctorId: null,
                unitId: null,
                clinicId: null,
                brickId: null,
                identityUserId: null,
                specId: null
            ));

            await _visitRepository.InsertAsync(new Visit
            (
                id: Guid.Parse("7475bd8f-50df-4075-ace7-9f8e659be9a5"),
                visitDate: new DateTime(2014, 11, 13),
                visitNotes: "0b63b6b6c4cf4fe292bc11f841090f2e15a26ac7fe",
                doctorId: null,
                unitId: null,
                clinicId: null,
                brickId: null,
                identityUserId: null,
                specId: null
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}