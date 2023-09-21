using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace ToksozBysNew.ExpenseMonthlies
{
    public class ExpenseMonthly : FullAuditedAggregateRoot<Guid>
    {
        [CanBeNull]
        public virtual string AccountId { get; set; }

        [CanBeNull]
        public virtual string AccountGroup { get; set; }

        [CanBeNull]
        public virtual string Account { get; set; }

        [CanBeNull]
        public virtual string Department { get; set; }

        [CanBeNull]
        public virtual string ExpenseType { get; set; }

        [CanBeNull]
        public virtual string Product { get; set; }

        [CanBeNull]
        public virtual string Proje { get; set; }

        [CanBeNull]
        public virtual string Comment { get; set; }

        [CanBeNull]
        public virtual string Month { get; set; }

        public virtual int Year { get; set; }

        public virtual int Unit { get; set; }

        public virtual float UnitValue { get; set; }

        public virtual float Amount { get; set; }

        public virtual float Memo { get; set; }

        [CanBeNull]
        public virtual string Invoice { get; set; }

        public virtual float Remain { get; set; }

        public ExpenseMonthly()
        {

        }

        public ExpenseMonthly(Guid id, string accountId, string accountGroup, string account, string department, string expenseType, string product, string proje, string comment, string month, int year, int unit, float unitValue, float amount, float memo, string invoice, float remain)
        {

            Id = id;
            AccountId = accountId;
            AccountGroup = accountGroup;
            Account = account;
            Department = department;
            ExpenseType = expenseType;
            Product = product;
            Proje = proje;
            Comment = comment;
            Month = month;
            Year = year;
            Unit = unit;
            UnitValue = unitValue;
            Amount = amount;
            Memo = memo;
            Invoice = invoice;
            Remain = remain;
        }

    }
}