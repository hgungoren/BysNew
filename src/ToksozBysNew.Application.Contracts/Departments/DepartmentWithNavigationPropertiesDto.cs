using ToksozBysNew.Companies;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace ToksozBysNew.Departments
{
    public class DepartmentWithNavigationPropertiesDto
    {
        public DepartmentDto Department { get; set; }

        public CompanyDto Company { get; set; }

    }
}