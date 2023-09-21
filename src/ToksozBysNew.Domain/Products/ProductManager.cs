using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace ToksozBysNew.Products
{
    public class ProductManager : DomainService
    {
        private readonly IProductRepository _productRepository;

        public ProductManager(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> CreateAsync(
        string productName)
        {
            var product = new Product(
             GuidGenerator.Create(),
             productName
             );

            return await _productRepository.InsertAsync(product);
        }

        public async Task<Product> UpdateAsync(
            Guid id,
            string productName, [CanBeNull] string concurrencyStamp = null
        )
        {
            var queryable = await _productRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var product = await AsyncExecuter.FirstOrDefaultAsync(query);

            product.ProductName = productName;

            product.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _productRepository.UpdateAsync(product);
        }

    }
}