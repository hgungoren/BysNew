using System;
using Volo.Abp.Identity;

namespace ToksozBysNew.ApplicationUsers
{
    public class ApplicationUser : IdentityUser
    {   
        public Guid? DepartmentId { get; set; } 
        public Guid? CompanyId { get; set; }  
    }
}
