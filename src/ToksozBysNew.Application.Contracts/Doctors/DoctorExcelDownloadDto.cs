using Volo.Abp.Application.Dtos;
using System;

namespace ToksozBysNew.Doctors
{
    public class DoctorExcelDownloadDto
    {
        public string DownloadToken { get; set; }

        public string FilterText { get; set; }

        public bool? IsActive { get; set; }
        public string NameSurname { get; set; }
        public string PharmacyName { get; set; }
        public Guid? PositionId { get; set; }
        public Guid? SpecId { get; set; }
        public Guid? CustomerTitleId { get; set; }
        public Guid? UnitId { get; set; }
        public Guid? CustomerTypeId { get; set; }

        public DoctorExcelDownloadDto()
        {

        }
    }
}