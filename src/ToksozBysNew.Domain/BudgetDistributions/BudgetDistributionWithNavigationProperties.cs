using ToksozBysNew.Departments;
using ToksozBysNew.Products;
using ToksozBysNew.Budgets;
using ToksozBysNew.AccountGroups;
using ToksozBysNew.Accounts;
using Volo.Abp.Identity;

using System;
using System.Collections.Generic;

namespace ToksozBysNew.BudgetDistributions
{
    public class BudgetDistributionWithNavigationProperties
    {
        public BudgetDistribution BudgetDistribution { get; set; }

        public Department Department { get; set; }
        public Product Product { get; set; }
        public Budget Budget { get; set; }
        public AccountGroup AccountGroup { get; set; }
        public Account Account { get; set; }
        public IdentityUser IdentityUser { get; set; }
        

        
    }
}