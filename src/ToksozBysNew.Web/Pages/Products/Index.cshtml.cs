using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using ToksozBysNew.Products;
using ToksozBysNew.Shared;

namespace ToksozBysNew.Web.Pages.Products
{
    public class IndexModel : AbpPageModel
    {
        public string ProductNameFilter { get; set; }

        private readonly IProductsAppService _productsAppService;

        public IndexModel(IProductsAppService productsAppService)
        {
            _productsAppService = productsAppService;
        }

        public async Task OnGetAsync()
        {

            await Task.CompletedTask;
        }
    }
}