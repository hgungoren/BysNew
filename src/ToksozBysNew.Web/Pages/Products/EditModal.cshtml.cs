using ToksozBysNew.Shared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using ToksozBysNew.Products;

namespace ToksozBysNew.Web.Pages.Products
{
    public class EditModalModel : ToksozBysNewPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public ProductUpdateViewModel Product { get; set; }

        private readonly IProductsAppService _productsAppService;

        public EditModalModel(IProductsAppService productsAppService)
        {
            _productsAppService = productsAppService;
        }

        public async Task OnGetAsync()
        {
            var product = await _productsAppService.GetAsync(Id);
            Product = ObjectMapper.Map<ProductDto, ProductUpdateViewModel>(product);

        }

        public async Task<NoContentResult> OnPostAsync()
        {

            await _productsAppService.UpdateAsync(Id, ObjectMapper.Map<ProductUpdateViewModel, ProductUpdateDto>(Product));
            return NoContent();
        }
    }

    public class ProductUpdateViewModel : ProductUpdateDto
    {
    }
}