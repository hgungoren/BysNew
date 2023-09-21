using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using ToksozBysNew.Permissions;
using ToksozBysNew.Products;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using ToksozBysNew.Shared;

namespace ToksozBysNew.Products
{

    [Authorize(ToksozBysNewPermissions.Products.Default)]
    public class ProductsAppService : ApplicationService, IProductsAppService
    {
        private readonly IDistributedCache<ProductExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly IProductRepository _productRepository;
        private readonly ProductManager _productManager;

        public ProductsAppService(IProductRepository productRepository, ProductManager productManager, IDistributedCache<ProductExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _productRepository = productRepository;
            _productManager = productManager;
        }

        public virtual async Task<PagedResultDto<ProductDto>> GetListAsync(GetProductsInput input)
        {
            var totalCount = await _productRepository.GetCountAsync(input.FilterText, input.ProductName);
            var items = await _productRepository.GetListAsync(input.FilterText, input.ProductName, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<ProductDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Product>, List<ProductDto>>(items)
            };
        }

        public virtual async Task<ProductDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Product, ProductDto>(await _productRepository.GetAsync(id));
        }

        [Authorize(ToksozBysNewPermissions.Products.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _productRepository.DeleteAsync(id);
        }

        [Authorize(ToksozBysNewPermissions.Products.Create)]
        public virtual async Task<ProductDto> CreateAsync(ProductCreateDto input)
        {

            var product = await _productManager.CreateAsync(
            input.ProductName
            );

            return ObjectMapper.Map<Product, ProductDto>(product);
        }

        [Authorize(ToksozBysNewPermissions.Products.Edit)]
        public virtual async Task<ProductDto> UpdateAsync(Guid id, ProductUpdateDto input)
        {

            var product = await _productManager.UpdateAsync(
            id,
            input.ProductName, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<Product, ProductDto>(product);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(ProductExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _productRepository.GetListAsync(input.FilterText, input.ProductName);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<Product>, List<ProductExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "Products.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new ProductExcelDownloadTokenCacheItem { Token = token },
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
                });

            return new DownloadTokenResultDto
            {
                Token = token
            };
        }
    }
}