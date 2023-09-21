using Volo.Abp.Application.Dtos;
using System;

namespace ToksozBysNew.CustomerTitles
{
    public class CustomerTitleExcelDownloadDto
    {
        public string DownloadToken { get; set; }

        public string FilterText { get; set; }

        public string TitleName { get; set; }

        public CustomerTitleExcelDownloadDto()
        {

        }
    }
}