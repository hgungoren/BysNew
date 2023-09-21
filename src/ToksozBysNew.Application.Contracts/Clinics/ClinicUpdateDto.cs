using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace ToksozBysNew.Clinics
{
    public class ClinicUpdateDto : IHasConcurrencyStamp
    {
        public string ClinicName { get; set; }
        public Guid? UnitId { get; set; }
        public Guid? SpecId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}