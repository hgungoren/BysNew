using ToksozBysNew.Companies;

using System;
using System.Collections.Generic;

namespace ToksozBysNew.Masters
{
    public class MasterWithNavigationProperties
    {
        public Master Master { get; set; }

        public Company Company { get; set; }
        

        
    }
}