using ToksozBysNew.Departments;
using ToksozBysNew.Products;
using ToksozBysNew.Budgets;
using ToksozBysNew.AccountGroups;
using ToksozBysNew.Accounts;
using Volo.Abp.Identity;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace ToksozBysNew.BudgetDistributions
{
    public class BudgetDistributionWithNavigationPropertiesDto
    {
        public BudgetDistributionDto BudgetDistribution { get; set; }

        public DepartmentDto Department { get; set; }
        public ProductDto Product { get; set; }
        public BudgetDto Budget { get; set; }
        public AccountGroupDto AccountGroup { get; set; }
        public AccountDto Account { get; set; }
        public IdentityUserDto IdentityUser { get; set; }

    }
}