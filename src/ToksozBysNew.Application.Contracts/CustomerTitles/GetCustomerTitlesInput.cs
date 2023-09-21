using Volo.Abp.Application.Dtos;
using System;

namespace ToksozBysNew.CustomerTitles
{
    public class GetCustomerTitlesInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public string TitleName { get; set; }

        public GetCustomerTitlesInput()
        {

        }
    }
}