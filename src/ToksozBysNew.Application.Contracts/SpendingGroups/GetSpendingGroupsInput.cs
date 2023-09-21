using Volo.Abp.Application.Dtos;
using System;

namespace ToksozBysNew.SpendingGroups
{
    public class GetSpendingGroupsInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public string Name { get; set; }

        public GetSpendingGroupsInput()
        {

        }
    }
}