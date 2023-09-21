using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ToksozBysNew.Departments
{
    public class DepartmentCreateDto
    {
        [StringLength(DepartmentConsts.DepartmentNameMaxLength)]
        public string DepartmentName { get; set; }
        public Guid? CompanyId { get; set; }
    }
}