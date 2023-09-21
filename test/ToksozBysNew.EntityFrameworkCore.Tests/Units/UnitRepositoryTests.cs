using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using ToksozBysNew.Units;
using ToksozBysNew.EntityFrameworkCore;
using Xunit;

namespace ToksozBysNew.Units
{
    public class UnitRepositoryTests : ToksozBysNewEntityFrameworkCoreTestBase
    {
        private readonly IUnitRepository _unitRepository;

        public UnitRepositoryTests()
        {
            _unitRepository = GetRequiredService<IUnitRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _unitRepository.GetListAsync(
                    unitName: "41e1edee7c994ae6a7fe0d9491b1d048b9958a3ec58a4d13876cc38e"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("9238db44-8200-4eed-95da-631df0726a12"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _unitRepository.GetCountAsync(
                    unitName: "c166c71ac43f4c27b0cd1b01237f0e3c5853faa62f6e49b88c70c29ec4c90"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}