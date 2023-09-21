using Volo.Abp.Application.Dtos;
using System;

namespace ToksozBysNew.Provinces
{
    public class GetProvincesInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public string ProvinceName { get; set; }
        public Guid? CountryId { get; set; }

        public GetProvincesInput()
        {

        }
    }
}