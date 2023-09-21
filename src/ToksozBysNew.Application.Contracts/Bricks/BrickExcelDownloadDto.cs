using Volo.Abp.Application.Dtos;
using System;

namespace ToksozBysNew.Bricks
{
    public class BrickExcelDownloadDto
    {
        public string DownloadToken { get; set; }

        public string FilterText { get; set; }

        public string BrickName { get; set; }

        public BrickExcelDownloadDto()
        {

        }
    }
}