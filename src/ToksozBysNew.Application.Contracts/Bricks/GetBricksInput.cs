using Volo.Abp.Application.Dtos;
using System;

namespace ToksozBysNew.Bricks
{
    public class GetBricksInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public string BrickName { get; set; }

        public GetBricksInput()
        {

        }
    }
}