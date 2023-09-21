using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ToksozBysNew.Units
{
    public class UnitCreateDto
    {
        public string UnitName { get; set; }
        public Guid? BrickId { get; set; }
    }
}