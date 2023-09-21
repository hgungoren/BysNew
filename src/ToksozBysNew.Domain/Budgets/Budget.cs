using ToksozBysNew.Companies;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace ToksozBysNew.Budgets
{
    public class Budget : FullAuditedAggregateRoot<Guid>
    {
        [NotNull]
        public virtual string BudgetName { get; set; }

        public virtual int Year { get; set; }

        [CanBeNull]
        public virtual string Comment { get; set; }

        public virtual bool IsActive { get; set; }

        public virtual DateTime? OpenUntil { get; set; }
        public Guid? CompanyId { get; set; }

        public Budget()
        {

        }

        public Budget(Guid id, Guid? companyId, string budgetName, int year, string comment, bool isActive, DateTime? openUntil = null)
        {

            Id = id;
            Check.NotNull(budgetName, nameof(budgetName));
            Check.Length(budgetName, nameof(budgetName), BudgetConsts.BudgetNameMaxLength, 0);
            Check.Length(comment, nameof(comment), BudgetConsts.CommentMaxLength, 0);
            BudgetName = budgetName;
            Year = year;
            Comment = comment;
            IsActive = isActive;
            OpenUntil = openUntil;
            CompanyId = companyId;
        }

    }
}