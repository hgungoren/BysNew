using ToksozBysNew.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToksozBysNew.Products;

namespace ToksozBysNew.Web.Pages.Products
{
    public class CreateModalModel : ToksozBysNewPageModel
    {
        [BindProperty]
        public ProductCreateViewModel Product { get; set; }

        private readonly IProductsAppService _productsAppService;

        public CreateModalModel(IProductsAppService productsAppService)
        {
            _productsAppService = productsAppService;
        }

        public async Task OnGetAsync()
        {
            Product = new ProductCreateViewModel();

            await Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostAsync()
        {

            await _productsAppService.CreateAsync(ObjectMapper.Map<ProductCreateViewModel, ProductCreateDto>(Product));
            return NoContent();
        }
    }

    public class ProductCreateViewModel : ProductCreateDto
    {
    }
}