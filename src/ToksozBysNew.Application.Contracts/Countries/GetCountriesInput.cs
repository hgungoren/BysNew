using Volo.Abp.Application.Dtos;
using System;

namespace ToksozBysNew.Countries
{
    public class GetCountriesInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public string CountryName { get; set; }

        public GetCountriesInput()
        {

        }
    }
}