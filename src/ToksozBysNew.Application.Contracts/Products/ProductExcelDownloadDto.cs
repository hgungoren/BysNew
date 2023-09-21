using Volo.Abp.Application.Dtos;
using System;

namespace ToksozBysNew.Products
{
    public class ProductExcelDownloadDto
    {
        public string DownloadToken { get; set; }

        public string FilterText { get; set; }

        public string ProductName { get; set; }

        public ProductExcelDownloadDto()
        {

        }
    }
}