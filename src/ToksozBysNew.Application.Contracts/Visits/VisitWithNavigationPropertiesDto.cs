using ToksozBysNew.Doctors;
using ToksozBysNew.Units;
using ToksozBysNew.Clinics;
using ToksozBysNew.Bricks;
using Volo.Abp.Identity;
using ToksozBysNew.Specs;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace ToksozBysNew.Visits
{
    public class VisitWithNavigationPropertiesDto
    {
        public VisitDto Visit { get; set; }

        public DoctorDto Doctor { get; set; }
        public UnitDto Unit { get; set; }
        public ClinicDto Clinic { get; set; }
        public BrickDto Brick { get; set; }
        public IdentityUserDto IdentityUser { get; set; }
        public SpecDto Spec { get; set; }

    }
}