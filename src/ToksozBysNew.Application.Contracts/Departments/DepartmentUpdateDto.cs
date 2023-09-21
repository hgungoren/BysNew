using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace ToksozBysNew.Departments
{
    public class DepartmentUpdateDto : IHasConcurrencyStamp
    {
        [StringLength(DepartmentConsts.DepartmentNameMaxLength)]
        public string DepartmentName { get; set; }
        public Guid? CompanyId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}