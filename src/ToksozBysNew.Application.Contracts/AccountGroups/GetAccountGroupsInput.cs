using Volo.Abp.Application.Dtos;
using System;

namespace ToksozBysNew.AccountGroups
{
    public class GetAccountGroupsInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public string AccountGroupName { get; set; }
        public bool? IsUnitEnterable { get; set; }

        public GetAccountGroupsInput()
        {

        }
    }
}