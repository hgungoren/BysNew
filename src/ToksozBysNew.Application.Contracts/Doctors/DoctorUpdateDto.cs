using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace ToksozBysNew.Doctors
{
    public class DoctorUpdateDto : IHasConcurrencyStamp
    {
        public bool IsActive { get; set; }
        public string NameSurname { get; set; }
        public string PharmacyName { get; set; }
        public Guid? PositionId { get; set; }
        public Guid? SpecId { get; set; }
        public Guid? CustomerTitleId { get; set; }
        public Guid? UnitId { get; set; }
        public Guid? CustomerTypeId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}