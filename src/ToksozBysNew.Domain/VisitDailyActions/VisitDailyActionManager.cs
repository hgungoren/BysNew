using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace ToksozBysNew.VisitDailyActions
{
    public class VisitDailyActionManager : DomainService
    {
        private readonly IVisitDailyActionRepository _visitDailyActionRepository;

        public VisitDailyActionManager(IVisitDailyActionRepository visitDailyActionRepository)
        {
            _visitDailyActionRepository = visitDailyActionRepository;
        }

        public async Task<VisitDailyAction> CreateAsync(
        Guid? identityUserId, DateTime visitDailyDate, decimal visitDaily1, decimal visitDaily2, decimal visitDaily3, decimal visitDaily4, decimal visitDaily5, decimal visitDaily6, decimal visitDaily7, decimal visitDaily8, decimal visitDaily9, decimal visitDaily10, decimal visitDaily11, decimal visitDaily12, decimal visitDaily13, decimal visitDaily14, decimal visitDaily15, DateTime visitDailyCloseDate, string visitDailyNote)
        {
            Check.NotNull(visitDailyDate, nameof(visitDailyDate));
            Check.NotNull(visitDailyCloseDate, nameof(visitDailyCloseDate));

            var visitDailyAction = new VisitDailyAction(
             GuidGenerator.Create(),
             identityUserId, visitDailyDate, visitDaily1, visitDaily2, visitDaily3, visitDaily4, visitDaily5, visitDaily6, visitDaily7, visitDaily8, visitDaily9, visitDaily10, visitDaily11, visitDaily12, visitDaily13, visitDaily14, visitDaily15, visitDailyCloseDate, visitDailyNote
             );

            return await _visitDailyActionRepository.InsertAsync(visitDailyAction);
        }

        public async Task<VisitDailyAction> UpdateAsync(
            Guid id,
            Guid? identityUserId, DateTime visitDailyDate, decimal visitDaily1, decimal visitDaily2, decimal visitDaily3, decimal visitDaily4, decimal visitDaily5, decimal visitDaily6, decimal visitDaily7, decimal visitDaily8, decimal visitDaily9, decimal visitDaily10, decimal visitDaily11, decimal visitDaily12, decimal visitDaily13, decimal visitDaily14, decimal visitDaily15, DateTime visitDailyCloseDate, string visitDailyNote, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNull(visitDailyDate, nameof(visitDailyDate));
            Check.NotNull(visitDailyCloseDate, nameof(visitDailyCloseDate));

            var visitDailyAction = await _visitDailyActionRepository.GetAsync(id);

            visitDailyAction.IdentityUserId = identityUserId;
            visitDailyAction.VisitDailyDate = visitDailyDate;
            visitDailyAction.VisitDaily1 = visitDaily1;
            visitDailyAction.VisitDaily2 = visitDaily2;
            visitDailyAction.VisitDaily3 = visitDaily3;
            visitDailyAction.VisitDaily4 = visitDaily4;
            visitDailyAction.VisitDaily5 = visitDaily5;
            visitDailyAction.VisitDaily6 = visitDaily6;
            visitDailyAction.VisitDaily7 = visitDaily7;
            visitDailyAction.VisitDaily8 = visitDaily8;
            visitDailyAction.VisitDaily9 = visitDaily9;
            visitDailyAction.VisitDaily10 = visitDaily10;
            visitDailyAction.VisitDaily11 = visitDaily11;
            visitDailyAction.VisitDaily12 = visitDaily12;
            visitDailyAction.VisitDaily13 = visitDaily13;
            visitDailyAction.VisitDaily14 = visitDaily14;
            visitDailyAction.VisitDaily15 = visitDaily15;
            visitDailyAction.VisitDailyCloseDate = visitDailyCloseDate;
            visitDailyAction.VisitDailyNote = visitDailyNote;

            visitDailyAction.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _visitDailyActionRepository.UpdateAsync(visitDailyAction);
        }

    }
}