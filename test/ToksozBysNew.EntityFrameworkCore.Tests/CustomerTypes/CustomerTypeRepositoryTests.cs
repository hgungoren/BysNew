using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using ToksozBysNew.CustomerTypes;
using ToksozBysNew.EntityFrameworkCore;
using Xunit;

namespace ToksozBysNew.CustomerTypes
{
    public class CustomerTypeRepositoryTests : ToksozBysNewEntityFrameworkCoreTestBase
    {
        private readonly ICustomerTypeRepository _customerTypeRepository;

        public CustomerTypeRepositoryTests()
        {
            _customerTypeRepository = GetRequiredService<ICustomerTypeRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _customerTypeRepository.GetListAsync(
                    typeName: "35992b28c9cb402f978df7b8abec6baaa954bb5b851a429489167e08"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("fb807768-3ca4-4e77-aafa-9a79829f5ca4"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _customerTypeRepository.GetCountAsync(
                    typeName: "e2896c801ada49b2b118748ec107a703001ae908cf884f4fa5539965c62"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}