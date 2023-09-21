using Volo.Abp.Application.Dtos;
using System;

namespace ToksozBysNew.Companies
{
    public class GetCompaniesInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public string CompanyName { get; set; }
        public bool? IsActive { get; set; }

        public GetCompaniesInput()
        {

        }
    }
}