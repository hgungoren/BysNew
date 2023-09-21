using ToksozBysNew.Positions;
using ToksozBysNew.Specs;
using ToksozBysNew.CustomerTitles;
using ToksozBysNew.Units;
using ToksozBysNew.CustomerTypes;

using System;
using System.Collections.Generic;

namespace ToksozBysNew.Doctors
{
    public class DoctorWithNavigationProperties
    {
        public Doctor Doctor { get; set; }

        public Position Position { get; set; }
        public Spec Spec { get; set; }
        public CustomerTitle CustomerTitle { get; set; }
        public Unit Unit { get; set; }
        public CustomerType CustomerType { get; set; }
        

        
    }
}