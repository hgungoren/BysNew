using Volo.Abp.Application.Dtos;
using System;

namespace ToksozBysNew.Districts
{
    public class DistrictExcelDownloadDto
    {
        public string DownloadToken { get; set; }

        public string FilterText { get; set; }

        public string DistrictName { get; set; }
        public Guid? CountryId { get; set; }
        public Guid? ProvinceId { get; set; }

        public DistrictExcelDownloadDto()
        {

        }
    }
}