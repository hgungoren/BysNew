using Volo.Abp.Application.Dtos;
using System;

namespace ToksozBysNew.Clinics
{
    public class GetClinicsInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public string ClinicName { get; set; }
        public Guid? UnitId { get; set; }
        public Guid? SpecId { get; set; }

        public GetClinicsInput()
        {

        }
    }
}