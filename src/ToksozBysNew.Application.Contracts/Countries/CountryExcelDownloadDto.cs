using Volo.Abp.Application.Dtos;
using System;

namespace ToksozBysNew.Countries
{
    public class CountryExcelDownloadDto
    {
        public string DownloadToken { get; set; }

        public string FilterText { get; set; }

        public string CountryName { get; set; }

        public CountryExcelDownloadDto()
        {

        }
    }
}