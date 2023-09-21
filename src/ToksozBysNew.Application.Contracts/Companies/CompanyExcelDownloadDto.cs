using Volo.Abp.Application.Dtos;
using System;

namespace ToksozBysNew.Companies
{
    public class CompanyExcelDownloadDto
    {
        public string DownloadToken { get; set; }

        public string FilterText { get; set; }

        public string CompanyName { get; set; }
        public bool? IsActive { get; set; }

        public CompanyExcelDownloadDto()
        {

        }
    }
}