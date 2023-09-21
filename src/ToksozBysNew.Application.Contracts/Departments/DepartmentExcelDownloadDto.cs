using Volo.Abp.Application.Dtos;
using System;

namespace ToksozBysNew.Departments
{
    public class DepartmentExcelDownloadDto
    {
        public string DownloadToken { get; set; }

        public string FilterText { get; set; }

        public string DepartmentName { get; set; }
        public Guid? CompanyId { get; set; }

        public DepartmentExcelDownloadDto()
        {

        }
    }
}