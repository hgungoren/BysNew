using Volo.Abp.Application.Dtos;
using System;

namespace ToksozBysNew.Provinces
{
    public class ProvinceExcelDownloadDto
    {
        public string DownloadToken { get; set; }

        public string FilterText { get; set; }

        public string ProvinceName { get; set; }
        public Guid? CountryId { get; set; }

        public ProvinceExcelDownloadDto()
        {

        }
    }
}