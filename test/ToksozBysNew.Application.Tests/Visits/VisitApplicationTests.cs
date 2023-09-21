using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace ToksozBysNew.Visits
{
    public class VisitsAppServiceTests : ToksozBysNewApplicationTestBase
    {
        private readonly IVisitsAppService _visitsAppService;
        private readonly IRepository<Visit, Guid> _visitRepository;

        public VisitsAppServiceTests()
        {
            _visitsAppService = GetRequiredService<IVisitsAppService>();
            _visitRepository = GetRequiredService<IRepository<Visit, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _visitsAppService.GetListAsync(new GetVisitsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Visit.Id == Guid.Parse("a0fd5129-a0d6-411e-b509-9371bf807aa0")).ShouldBe(true);
            result.Items.Any(x => x.Visit.Id == Guid.Parse("7475bd8f-50df-4075-ace7-9f8e659be9a5")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _visitsAppService.GetAsync(Guid.Parse("a0fd5129-a0d6-411e-b509-9371bf807aa0"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("a0fd5129-a0d6-411e-b509-9371bf807aa0"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new VisitCreateDto
            {
                VisitDate = new DateTime(2014, 1, 19),
                VisitNotes = "a6e0f828cc9e4ef491566b4ef2ca1f2e8742cf"
            };

            // Act
            var serviceResult = await _visitsAppService.CreateAsync(input);

            // Assert
            var result = await _visitRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.VisitDate.ShouldBe(new DateTime(2014, 1, 19));
            result.VisitNotes.ShouldBe("a6e0f828cc9e4ef491566b4ef2ca1f2e8742cf");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new VisitUpdateDto()
            {
                VisitDate = new DateTime(2013, 7, 26),
                VisitNotes = "c521d190fcb74f0dae533a5007374885a5745db4ac4f4cdb80d0e37b1c7"
            };

            // Act
            var serviceResult = await _visitsAppService.UpdateAsync(Guid.Parse("a0fd5129-a0d6-411e-b509-9371bf807aa0"), input);

            // Assert
            var result = await _visitRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.VisitDate.ShouldBe(new DateTime(2013, 7, 26));
            result.VisitNotes.ShouldBe("c521d190fcb74f0dae533a5007374885a5745db4ac4f4cdb80d0e37b1c7");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _visitsAppService.DeleteAsync(Guid.Parse("a0fd5129-a0d6-411e-b509-9371bf807aa0"));

            // Assert
            var result = await _visitRepository.FindAsync(c => c.Id == Guid.Parse("a0fd5129-a0d6-411e-b509-9371bf807aa0"));

            result.ShouldBeNull();
        }
    }
}