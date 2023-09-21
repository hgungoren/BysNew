using ToksozBysNew.Positions;
using ToksozBysNew.Specs;
using ToksozBysNew.CustomerTitles;
using ToksozBysNew.Units;
using ToksozBysNew.CustomerTypes;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace ToksozBysNew.Doctors
{
    public class DoctorWithNavigationPropertiesDto
    {
        public DoctorDto Doctor { get; set; }

        public PositionDto Position { get; set; }
        public SpecDto Spec { get; set; }
        public CustomerTitleDto CustomerTitle { get; set; }
        public UnitDto Unit { get; set; }
        public CustomerTypeDto CustomerType { get; set; }

    }
}