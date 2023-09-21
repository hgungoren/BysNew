using Volo.Abp.Identity;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace ToksozBysNew.VisitDailyActions
{
    public class VisitDailyActionWithNavigationPropertiesDto
    {
        public VisitDailyActionDto VisitDailyAction { get; set; }

        public IdentityUserDto IdentityUser { get; set; }

    }
}