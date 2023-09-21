using Volo.Abp.Application.Dtos;
using System;

namespace ToksozBysNew.Products
{
    public class GetProductsInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public string ProductName { get; set; }

        public GetProductsInput()
        {

        }
    }
}