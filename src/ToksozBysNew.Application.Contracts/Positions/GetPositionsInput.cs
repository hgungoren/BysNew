using Volo.Abp.Application.Dtos;
using System;

namespace ToksozBysNew.Positions
{
    public class GetPositionsInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public string PositionCode { get; set; }
        public string PositionName { get; set; }

        public GetPositionsInput()
        {

        }
    }
}