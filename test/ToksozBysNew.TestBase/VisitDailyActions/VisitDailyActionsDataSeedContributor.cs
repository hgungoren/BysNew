using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using ToksozBysNew.VisitDailyActions;

namespace ToksozBysNew.VisitDailyActions
{
    public class VisitDailyActionsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IVisitDailyActionRepository _visitDailyActionRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public VisitDailyActionsDataSeedContributor(IVisitDailyActionRepository visitDailyActionRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _visitDailyActionRepository = visitDailyActionRepository;
            _unitOfWorkManager = unitOfWorkManager;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _visitDailyActionRepository.InsertAsync(new VisitDailyAction
            (
                id: Guid.Parse("ea7d8753-4ba8-40a2-8475-20fe3d2c231e"),
                visitDailyDate: new DateTime(2000, 10, 23),
                visitDaily1: 1539976965,
                visitDaily2: 1215168945,
                visitDaily3: 323378482,
                visitDaily4: 2072954033,
                visitDaily5: 862087448,
                visitDaily6: 1400899605,
                visitDaily7: 358084163,
                visitDaily8: 579060601,
                visitDaily9: 841944461,
                visitDaily10: 1052401004,
                visitDaily11: 936137976,
                visitDaily12: 1233541774,
                visitDaily13: 1078191977,
                visitDaily14: 2020782782,
                visitDaily15: 1782477995,
                visitDailyCloseDate: new DateTime(2009, 2, 13),
                visitDailyNote: "120e2371216c4",
                identityUserId: null
            ));

            await _visitDailyActionRepository.InsertAsync(new VisitDailyAction
            (
                id: Guid.Parse("abfef905-3608-46fd-a801-2707eb789efa"),
                visitDailyDate: new DateTime(2022, 5, 7),
                visitDaily1: 1353523483,
                visitDaily2: 1973141972,
                visitDaily3: 1743224597,
                visitDaily4: 1854765236,
                visitDaily5: 2036805486,
                visitDaily6: 1535739778,
                visitDaily7: 1563324653,
                visitDaily8: 498544046,
                visitDaily9: 284568218,
                visitDaily10: 466102542,
                visitDaily11: 37097310,
                visitDaily12: 1680423981,
                visitDaily13: 1583337602,
                visitDaily14: 511596579,
                visitDaily15: 1202905937,
                visitDailyCloseDate: new DateTime(2001, 8, 11),
                visitDailyNote: "07be081911",
                identityUserId: null
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}