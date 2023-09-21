using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace ToksozBysNew.Bricks
{
    public class BrickUpdateDto : IHasConcurrencyStamp
    {
        public string BrickName { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}