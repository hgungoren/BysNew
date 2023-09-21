using Volo.Abp.Application.Dtos;
using System;

namespace ToksozBysNew.Months
{
    public class GetMonthsInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public string Name { get; set; }

        public GetMonthsInput()
        {

        }
    }
}