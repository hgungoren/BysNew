using Volo.Abp.Identity;

using System;
using System.Collections.Generic;

namespace ToksozBysNew.VisitDailyActions
{
    public class VisitDailyActionWithNavigationProperties
    {
        public VisitDailyAction VisitDailyAction { get; set; }

        public IdentityUser IdentityUser { get; set; }
        

        
    }
}