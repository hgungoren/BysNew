using Volo.Abp.Application.Dtos;
using System;

namespace ToksozBysNew.CustomerTypes
{
    public class GetCustomerTypesInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public string TypeName { get; set; }

        public GetCustomerTypesInput()
        {

        }
    }
}