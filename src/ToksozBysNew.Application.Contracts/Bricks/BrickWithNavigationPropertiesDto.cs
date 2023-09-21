using ToksozBysNew.Units;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace ToksozBysNew.Bricks
{
    public class BrickWithNavigationPropertiesDto
    {
        public BrickDto Brick { get; set; }

        public UnitDto Unit { get; set; }

    }
}