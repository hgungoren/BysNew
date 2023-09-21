using ToksozBysNew.Doctors;
using ToksozBysNew.Units;
using ToksozBysNew.Clinics;
using ToksozBysNew.Bricks;
using Volo.Abp.Identity;
using ToksozBysNew.Specs;

using System;
using System.Collections.Generic;

namespace ToksozBysNew.Visits
{
    public class VisitWithNavigationProperties
    {
        public Visit Visit { get; set; }

        public Doctor Doctor { get; set; }
        public Unit Unit { get; set; }
        public Clinic Clinic { get; set; }
        public Brick Brick { get; set; }
        public IdentityUser IdentityUser { get; set; }
        public Spec Spec { get; set; }
        

        
    }
}