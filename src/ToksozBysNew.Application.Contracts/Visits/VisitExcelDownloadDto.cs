using Volo.Abp.Application.Dtos;
using System;

namespace ToksozBysNew.Visits
{
    public class VisitExcelDownloadDto
    {
        public string DownloadToken { get; set; }

        public string FilterText { get; set; }

        public DateTime? VisitDateMin { get; set; }
        public DateTime? VisitDateMax { get; set; }
        public string VisitNotes { get; set; }
        public Guid? DoctorId { get; set; }
        public Guid? UnitId { get; set; }
        public Guid? ClinicId { get; set; }
        public Guid? BrickId { get; set; }
        public Guid? IdentityUserId { get; set; }
        public Guid? SpecId { get; set; }

        public VisitExcelDownloadDto()
        {

        }
    }
}