using Volo.Abp.Identity;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace ToksozBysNew.VisitDailyActions
{
    public class VisitDailyAction : FullAuditedAggregateRoot<Guid>
    {
        public virtual DateTime VisitDailyDate { get; set; }

        public virtual decimal VisitDaily1 { get; set; }

        public virtual decimal VisitDaily2 { get; set; }

        public virtual decimal VisitDaily3 { get; set; }

        public virtual decimal VisitDaily4 { get; set; }

        public virtual decimal VisitDaily5 { get; set; }

        public virtual decimal VisitDaily6 { get; set; }

        public virtual decimal VisitDaily7 { get; set; }

        public virtual decimal VisitDaily8 { get; set; }

        public virtual decimal VisitDaily9 { get; set; }

        public virtual decimal VisitDaily10 { get; set; }

        public virtual decimal VisitDaily11 { get; set; }

        public virtual decimal VisitDaily12 { get; set; }

        public virtual decimal VisitDaily13 { get; set; }

        public virtual decimal VisitDaily14 { get; set; }

        public virtual decimal VisitDaily15 { get; set; }

        public virtual DateTime VisitDailyCloseDate { get; set; }

        [CanBeNull]
        public virtual string VisitDailyNote { get; set; }
        public Guid? IdentityUserId { get; set; }

        public VisitDailyAction()
        {

        }

        public VisitDailyAction(Guid id, Guid? identityUserId, DateTime visitDailyDate, decimal visitDaily1, decimal visitDaily2, decimal visitDaily3, decimal visitDaily4, decimal visitDaily5, decimal visitDaily6, decimal visitDaily7, decimal visitDaily8, decimal visitDaily9, decimal visitDaily10, decimal visitDaily11, decimal visitDaily12, decimal visitDaily13, decimal visitDaily14, decimal visitDaily15, DateTime visitDailyCloseDate, string visitDailyNote)
        {

            Id = id;
            VisitDailyDate = visitDailyDate;
            VisitDaily1 = visitDaily1;
            VisitDaily2 = visitDaily2;
            VisitDaily3 = visitDaily3;
            VisitDaily4 = visitDaily4;
            VisitDaily5 = visitDaily5;
            VisitDaily6 = visitDaily6;
            VisitDaily7 = visitDaily7;
            VisitDaily8 = visitDaily8;
            VisitDaily9 = visitDaily9;
            VisitDaily10 = visitDaily10;
            VisitDaily11 = visitDaily11;
            VisitDaily12 = visitDaily12;
            VisitDaily13 = visitDaily13;
            VisitDaily14 = visitDaily14;
            VisitDaily15 = visitDaily15;
            VisitDailyCloseDate = visitDailyCloseDate;
            VisitDailyNote = visitDailyNote;
            IdentityUserId = identityUserId;
        }

    }
}