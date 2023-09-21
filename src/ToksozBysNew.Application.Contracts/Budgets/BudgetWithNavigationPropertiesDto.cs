using ToksozBysNew.Companies;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace ToksozBysNew.Budgets
{
    public class BudgetWithNavigationPropertiesDto
    {
        public BudgetDto Budget { get; set; }

        public CompanyDto Company { get; set; }

    }
}