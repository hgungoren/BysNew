using Volo.Abp.Application.Dtos;
using System;

namespace ToksozBysNew.Accounts
{
    public class GetAccountsInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public string AccountCode { get; set; }
        public string AccountName { get; set; }
        public string Description { get; set; }
        public bool? IsActive { get; set; }

        public GetAccountsInput()
        {

        }
    }
}