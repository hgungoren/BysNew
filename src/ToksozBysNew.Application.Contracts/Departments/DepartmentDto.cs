using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace ToksozBysNew.Departments
{
    public class DepartmentDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string DepartmentName { get; set; }
        public Guid? CompanyId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}