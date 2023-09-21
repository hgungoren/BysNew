using Volo.Abp.Application.Dtos;
using System;

namespace ToksozBysNew.Units
{
    public class UnitExcelDownloadDto
    {
        public string DownloadToken { get; set; }

        public string FilterText { get; set; }

        public string UnitName { get; set; }
        public Guid? BrickId { get; set; }

        public UnitExcelDownloadDto()
        {

        }
    }
}