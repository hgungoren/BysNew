using Volo.Abp.Application.Dtos;
using System;

namespace ToksozBysNew.Districts
{
    public class GetDistrictsInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public string DistrictName { get; set; }
        public Guid? CountryId { get; set; }
        public Guid? ProvinceId { get; set; }

        public GetDistrictsInput()
        {

        }
    }
}