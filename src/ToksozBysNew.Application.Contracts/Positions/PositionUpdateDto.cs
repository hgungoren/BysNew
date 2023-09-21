using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace ToksozBysNew.Positions
{
    public class PositionUpdateDto : IHasConcurrencyStamp
    {
        public string PositionCode { get; set; }
        public string PositionName { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}