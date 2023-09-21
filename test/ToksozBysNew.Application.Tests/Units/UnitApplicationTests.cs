using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace ToksozBysNew.Units
{
    public class UnitsAppServiceTests : ToksozBysNewApplicationTestBase
    {
        private readonly IUnitsAppService _unitsAppService;
        private readonly IRepository<Unit, Guid> _unitRepository;

        public UnitsAppServiceTests()
        {
            _unitsAppService = GetRequiredService<IUnitsAppService>();
            _unitRepository = GetRequiredService<IRepository<Unit, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _unitsAppService.GetListAsync(new GetUnitsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Unit.Id == Guid.Parse("9238db44-8200-4eed-95da-631df0726a12")).ShouldBe(true);
            result.Items.Any(x => x.Unit.Id == Guid.Parse("316d5039-b574-47c8-951f-2b21c36490bb")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _unitsAppService.GetAsync(Guid.Parse("9238db44-8200-4eed-95da-631df0726a12"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("9238db44-8200-4eed-95da-631df0726a12"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new UnitCreateDto
            {
                UnitName = "147c742377b3441"
            };

            // Act
            var serviceResult = await _unitsAppService.CreateAsync(input);

            // Assert
            var result = await _unitRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.UnitName.ShouldBe("147c742377b3441");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new UnitUpdateDto()
            {
                UnitName = "a27e5bfc2bfc4a2b9a55b6d140d0257d3a205c1f85cf44db8c4d2fa7dadbd3f2934ac77f8f0a45eb8fc6244106f043b"
            };

            // Act
            var serviceResult = await _unitsAppService.UpdateAsync(Guid.Parse("9238db44-8200-4eed-95da-631df0726a12"), input);

            // Assert
            var result = await _unitRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.UnitName.ShouldBe("a27e5bfc2bfc4a2b9a55b6d140d0257d3a205c1f85cf44db8c4d2fa7dadbd3f2934ac77f8f0a45eb8fc6244106f043b");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _unitsAppService.DeleteAsync(Guid.Parse("9238db44-8200-4eed-95da-631df0726a12"));

            // Assert
            var result = await _unitRepository.FindAsync(c => c.Id == Guid.Parse("9238db44-8200-4eed-95da-631df0726a12"));

            result.ShouldBeNull();
        }
    }
}