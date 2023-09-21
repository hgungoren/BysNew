using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using ToksozBysNew.Products;
using ToksozBysNew.EntityFrameworkCore;
using Xunit;

namespace ToksozBysNew.Products
{
    public class ProductRepositoryTests : ToksozBysNewEntityFrameworkCoreTestBase
    {
        private readonly IProductRepository _productRepository;

        public ProductRepositoryTests()
        {
            _productRepository = GetRequiredService<IProductRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _productRepository.GetListAsync(
                    productName: "911833d532954909aadfba8fc16de8e6975bfa3e771041f493a590ef4a1457d70edf72f088da43daafa73290b7fab85d090d"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("e36ae10e-ab18-403d-a2f6-10df1e6fe8df"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _productRepository.GetCountAsync(
                    productName: "98d42e7d4fc04529a9bfdb7863eedb4e4d7d1e7d42ae4964a514a3896d3564b99ac76bcf2a9443eeae1ce6df97bf2fabdeac"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}