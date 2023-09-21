using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ToksozBysNew.Visits
{
    public class VisitCreateDto
    {
        public DateTime VisitDate { get; set; }
        public string VisitNotes { get; set; }
        public Guid? DoctorId { get; set; }
        public Guid? UnitId { get; set; }
        public Guid? ClinicId { get; set; }
        public Guid? BrickId { get; set; }
        public Guid? IdentityUserId { get; set; }
        public Guid? SpecId { get; set; }
    }
}