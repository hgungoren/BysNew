using Volo.Abp.Application.Dtos;
using System;

namespace ToksozBysNew.Positions
{
    public class PositionExcelDownloadDto
    {
        public string DownloadToken { get; set; }

        public string FilterText { get; set; }

        public string PositionCode { get; set; }
        public string PositionName { get; set; }

        public PositionExcelDownloadDto()
        {

        }
    }
}