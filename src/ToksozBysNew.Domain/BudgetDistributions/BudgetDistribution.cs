using ToksozBysNew.Departments;
using ToksozBysNew.Products;
using ToksozBysNew.Budgets;
using ToksozBysNew.AccountGroups;
using ToksozBysNew.Accounts;
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

namespace ToksozBysNew.BudgetDistributions
{
    public class BudgetDistribution : FullAuditedAggregateRoot<Guid>
    {
        [CanBeNull]
        public virtual string CostCenter { get; set; }

        [CanBeNull]
        public virtual string ExpenseType { get; set; }

        public virtual int? ProjectItem { get; set; }

        public virtual int? Type { get; set; }

        public virtual int? Unit { get; set; }

        public virtual float? UnitValue { get; set; }

        public virtual int Month { get; set; }

        public virtual int? Year { get; set; }

        public virtual float? Ratio { get; set; }

        public virtual float Amount { get; set; }

        public virtual float? Memo { get; set; }

        public virtual float? Invoice { get; set; }

        public virtual int? Currency { get; set; }

        public virtual float? CurrencyAmount { get; set; }

        public virtual int? ExpenseCategory { get; set; }

        public virtual int? ExpenseNecessity { get; set; }

        [CanBeNull]
        public virtual string Comment { get; set; }

        [CanBeNull]
        public virtual string Status { get; set; }

        public virtual int? Approval { get; set; }

        public virtual bool IsActive { get; set; }
        public Guid DepartmentId { get; set; }
        public Guid? ProductId { get; set; }
        public Guid BudgetId { get; set; }
        public Guid? AccountGroupId { get; set; }
        public Guid? AccountId { get; set; }
        public Guid? IdentityUserId { get; set; }

        public BudgetDistribution()
        {

        }

        public BudgetDistribution(Guid id, Guid departmentId, Guid? productId, Guid budgetId, Guid? accountGroupId, Guid? accountId, Guid? identityUserId, string costCenter, string expenseType, int month, float amount, string comment, string status, bool isActive, int? projectItem = null, int? type = null, int? unit = null, float? unitValue = null, int? year = null, float? ratio = null, float? memo = null, float? invoice = null, int? currency = null, float? currencyAmount = null, int? expenseCategory = null, int? expenseNecessity = null, int? approval = null)
        {

            Id = id;
            Check.Length(costCenter, nameof(costCenter), BudgetDistributionConsts.CostCenterMaxLength, 0);
            Check.Length(expenseType, nameof(expenseType), BudgetDistributionConsts.ExpenseTypeMaxLength, 0);
            Check.Length(status, nameof(status), BudgetDistributionConsts.StatusMaxLength, 0);
            CostCenter = costCenter;
            ExpenseType = expenseType;
            Month = month;
            Amount = amount;
            Comment = comment;
            Status = status;
            IsActive = isActive;
            ProjectItem = projectItem;
            Type = type;
            Unit = unit;
            UnitValue = unitValue;
            Year = year;
            Ratio = ratio;
            Memo = memo;
            Invoice = invoice;
            Currency = currency;
            CurrencyAmount = currencyAmount;
            ExpenseCategory = expenseCategory;
            ExpenseNecessity = expenseNecessity;
            Approval = approval;
            DepartmentId = departmentId;
            ProductId = productId;
            BudgetId = budgetId;
            AccountGroupId = accountGroupId;
            AccountId = accountId;
            IdentityUserId = identityUserId;
        }

    }
}