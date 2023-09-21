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

namespace ToksozBysNew.Departments
{
    public class Department : FullAuditedAggregateRoot<Guid>
    {
        [CanBeNull]
        public virtual string DepartmentName { get; set; }
        public Guid? CompanyId { get; set; }

        public Department()
        {

        }

        public Department(Guid id, Guid? companyId, string departmentName)
        {

            Id = id;
            Check.Length(departmentName, nameof(departmentName), DepartmentConsts.DepartmentNameMaxLength, 0);
            DepartmentName = departmentName;
            CompanyId = companyId;
        }

    }
}