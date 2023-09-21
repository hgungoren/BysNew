using Volo.Abp.Application.Dtos;
using System;

namespace ToksozBysNew.Specs
{
    public class GetSpecsInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public string SpecCode { get; set; }
        public string SpecName { get; set; }

        public GetSpecsInput()
        {

        }
    }
}