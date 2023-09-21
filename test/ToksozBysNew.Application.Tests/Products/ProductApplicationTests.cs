using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace ToksozBysNew.Products
{
    public class ProductsAppServiceTests : ToksozBysNewApplicationTestBase
    {
        private readonly IProductsAppService _productsAppService;
        private readonly IRepository<Product, Guid> _productRepository;

        public ProductsAppServiceTests()
        {
            _productsAppService = GetRequiredService<IProductsAppService>();
            _productRepository = GetRequiredService<IRepository<Product, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _productsAppService.GetListAsync(new GetProductsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("e36ae10e-ab18-403d-a2f6-10df1e6fe8df")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("239d49d9-c964-42e3-a535-45938fe429c0")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _productsAppService.GetAsync(Guid.Parse("e36ae10e-ab18-403d-a2f6-10df1e6fe8df"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("e36ae10e-ab18-403d-a2f6-10df1e6fe8df"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new ProductCreateDto
            {
                ProductName = "12ee734a844b4e56b86f4e0d1107221f71732c9ce0b942d3abaf134f3336b512c375ea00765a4ecdbecb78d0cfc2bbfd7552"
            };

            // Act
            var serviceResult = await _productsAppService.CreateAsync(input);

            // Assert
            var result = await _productRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.ProductName.ShouldBe("12ee734a844b4e56b86f4e0d1107221f71732c9ce0b942d3abaf134f3336b512c375ea00765a4ecdbecb78d0cfc2bbfd7552");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new ProductUpdateDto()
            {
                ProductName = "e4b5c33c9f544adda9db0bc24af5135e74bbec9a3ffc4273a3d585cb16f358767a8caf4ef8b24092a7d59262ee30d378b938"
            };

            // Act
            var serviceResult = await _productsAppService.UpdateAsync(Guid.Parse("e36ae10e-ab18-403d-a2f6-10df1e6fe8df"), input);

            // Assert
            var result = await _productRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.ProductName.ShouldBe("e4b5c33c9f544adda9db0bc24af5135e74bbec9a3ffc4273a3d585cb16f358767a8caf4ef8b24092a7d59262ee30d378b938");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _productsAppService.DeleteAsync(Guid.Parse("e36ae10e-ab18-403d-a2f6-10df1e6fe8df"));

            // Assert
            var result = await _productRepository.FindAsync(c => c.Id == Guid.Parse("e36ae10e-ab18-403d-a2f6-10df1e6fe8df"));

            result.ShouldBeNull();
        }
    }
}