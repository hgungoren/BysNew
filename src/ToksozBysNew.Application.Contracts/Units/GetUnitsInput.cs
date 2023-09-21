using Volo.Abp.Application.Dtos;
using System;

namespace ToksozBysNew.Units
{
    public class GetUnitsInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public string UnitName { get; set; }
        public Guid? BrickId { get; set; }

        public GetUnitsInput()
        {

        }
    }
}