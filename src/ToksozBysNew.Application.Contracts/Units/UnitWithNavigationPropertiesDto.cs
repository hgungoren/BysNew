using ToksozBysNew.Bricks;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace ToksozBysNew.Units
{
    public class UnitWithNavigationPropertiesDto
    {
        public UnitDto Unit { get; set; }

        public BrickDto Brick { get; set; }

    }
}