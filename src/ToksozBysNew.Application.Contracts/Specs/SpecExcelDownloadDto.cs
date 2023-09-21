using Volo.Abp.Application.Dtos;
using System;

namespace ToksozBysNew.Specs
{
    public class SpecExcelDownloadDto
    {
        public string DownloadToken { get; set; }

        public string FilterText { get; set; }

        public string SpecCode { get; set; }
        public string SpecName { get; set; }

        public SpecExcelDownloadDto()
        {

        }
    }
}