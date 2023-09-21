using ToksozBysNew.Doctors;
using ToksozBysNew.Bricks;
using ToksozBysNew.Districts;
using ToksozBysNew.Countries;
using ToksozBysNew.Provinces;

using System;
using System.Collections.Generic;

namespace ToksozBysNew.CustomerAddresses
{
    public class CustomerAddressWithNavigationProperties
    {
        public CustomerAddress CustomerAddress { get; set; }

        public Doctor Doctor { get; set; }
        public Brick Brick { get; set; }
        public District District { get; set; }
        public Country Country { get; set; }
        public Province Province { get; set; }
        

        
    }
}