using ToksozBysNew.Companies;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace ToksozBysNew.Masters
{
    public class MasterWithNavigationPropertiesDto
    {
        public MasterDto Master { get; set; }

        public CompanyDto Company { get; set; }

    }
}