using ToksozBysNew.Doctors;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace ToksozBysNew.Specs
{
    public class SpecWithNavigationPropertiesDto
    {
        public SpecDto Spec { get; set; }

        public DoctorDto Doctor { get; set; }

    }
}