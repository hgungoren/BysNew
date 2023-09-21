using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace ToksozBysNew.Units
{
    public class UnitUpdateDto : IHasConcurrencyStamp
    {
        public string UnitName { get; set; }
        public Guid? BrickId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}