using Volo.Abp.Application.Dtos;
using System;

namespace ToksozBysNew.TaxLists
{
    public class GetTaxListsInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public string TaxName { get; set; }
        public int? TaxValueMin { get; set; }
        public int? TaxValueMax { get; set; }

        public GetTaxListsInput()
        {

        }
    }
}