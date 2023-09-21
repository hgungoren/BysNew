using ToksozBysNew.Countries;
using ToksozBysNew.Provinces;

using System;
using System.Collections.Generic;

namespace ToksozBysNew.Districts
{
    public class DistrictWithNavigationProperties
    {
        public District District { get; set; }

        public Country Country { get; set; }
        public Province Province { get; set; }
        

        
    }
}