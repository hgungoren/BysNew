using Volo.Abp.Application.Dtos;
using System;

namespace ToksozBysNew.AccountGroups
{
    public class AccountGroupExcelDownloadDto
    {
        public string DownloadToken { get; set; }

        public string FilterText { get; set; }

        public string AccountGroupName { get; set; }
        public bool? IsUnitEnterable { get; set; }

        public AccountGroupExcelDownloadDto()
        {

        }
    }
}