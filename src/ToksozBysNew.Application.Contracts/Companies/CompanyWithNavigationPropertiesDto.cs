using Volo.Abp.Identity;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace ToksozBysNew.Companies
{
    public class CompanyWithNavigationPropertiesDto
    {
        public CompanyDto Company { get; set; }

        public IdentityUserDto IdentityUser { get; set; }

    }
}