using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace ToksozBysNew.Companies
{
    public class CompanyDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string CompanyName { get; set; }
        public bool IsActive { get; set; }
        public string DepartmentName { get; set; }
        public string ConcurrencyStamp { get; set; }
    }
}