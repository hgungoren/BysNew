using Volo.Abp.Identity;

using System;
using System.Collections.Generic;

namespace ToksozBysNew.Companies
{
    public class CompanyWithNavigationProperties
    {
        public Company Company { get; set; }

        public IdentityUser IdentityUser { get; set; }
        

        
    }
}