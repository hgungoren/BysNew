using ToksozBysNew.Units;
using ToksozBysNew.Specs;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace ToksozBysNew.Clinics
{
    public class ClinicWithNavigationPropertiesDto
    {
        public ClinicDto Clinic { get; set; }

        public UnitDto Unit { get; set; }
        public SpecDto Spec { get; set; }

    }
}