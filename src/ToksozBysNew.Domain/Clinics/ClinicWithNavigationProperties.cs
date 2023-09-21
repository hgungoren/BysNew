using ToksozBysNew.Units;
using ToksozBysNew.Specs;

using System;
using System.Collections.Generic;

namespace ToksozBysNew.Clinics
{
    public class ClinicWithNavigationProperties
    {
        public Clinic Clinic { get; set; }

        public Unit Unit { get; set; }
        public Spec Spec { get; set; }
        

        
    }
}