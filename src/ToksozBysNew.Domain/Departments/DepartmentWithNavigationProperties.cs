using ToksozBysNew.Companies;

using System;
using System.Collections.Generic;

namespace ToksozBysNew.Departments
{
    public class DepartmentWithNavigationProperties
    {
        public Department Department { get; set; }

        public Company Company { get; set; }
        

        
    }
}