using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ToksozBysNew.Positions
{
    public class PositionCreateDto
    {
        public string PositionCode { get; set; }
        public string PositionName { get; set; }
    }
}