using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace ToksozBysNew.VisitDailyActions
{
    public class VisitDailyActionsAppServiceTests : ToksozBysNewApplicationTestBase
    {
        private readonly IVisitDailyActionsAppService _visitDailyActionsAppService;
        private readonly IRepository<VisitDailyAction, Guid> _visitDailyActionRepository;

        public VisitDailyActionsAppServiceTests()
        {
            _visitDailyActionsAppService = GetRequiredService<IVisitDailyActionsAppService>();
            _visitDailyActionRepository = GetRequiredService<IRepository<VisitDailyAction, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _visitDailyActionsAppService.GetListAsync(new GetVisitDailyActionsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.VisitDailyAction.Id == Guid.Parse("ea7d8753-4ba8-40a2-8475-20fe3d2c231e")).ShouldBe(true);
            result.Items.Any(x => x.VisitDailyAction.Id == Guid.Parse("abfef905-3608-46fd-a801-2707eb789efa")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _visitDailyActionsAppService.GetAsync(Guid.Parse("ea7d8753-4ba8-40a2-8475-20fe3d2c231e"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("ea7d8753-4ba8-40a2-8475-20fe3d2c231e"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new VisitDailyActionCreateDto
            {
                VisitDailyDate = new DateTime(2010, 3, 14),
                VisitDaily1 = 1176416080,
                VisitDaily2 = 1432769450,
                VisitDaily3 = 1121087635,
                VisitDaily4 = 989269669,
                VisitDaily5 = 1400113306,
                VisitDaily6 = 295126746,
                VisitDaily7 = 1160236756,
                VisitDaily8 = 1170211047,
                VisitDaily9 = 614460156,
                VisitDaily10 = 307643201,
                VisitDaily11 = 46480757,
                VisitDaily12 = 1427938798,
                VisitDaily13 = 1981989066,
                VisitDaily14 = 512231819,
                VisitDaily15 = 482776998,
                VisitDailyCloseDate = new DateTime(2000, 7, 3),
                VisitDailyNote = "33d4e47d7564480d8cf0ea214fcd2d37e60b432a59174b2e8c281db5e008f6a8c42cf639292d4adfa9e2415d2607"
            };

            // Act
            var serviceResult = await _visitDailyActionsAppService.CreateAsync(input);

            // Assert
            var result = await _visitDailyActionRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.VisitDailyDate.ShouldBe(new DateTime(2010, 3, 14));
            result.VisitDaily1.ShouldBe(1176416080);
            result.VisitDaily2.ShouldBe(1432769450);
            result.VisitDaily3.ShouldBe(1121087635);
            result.VisitDaily4.ShouldBe(989269669);
            result.VisitDaily5.ShouldBe(1400113306);
            result.VisitDaily6.ShouldBe(295126746);
            result.VisitDaily7.ShouldBe(1160236756);
            result.VisitDaily8.ShouldBe(1170211047);
            result.VisitDaily9.ShouldBe(614460156);
            result.VisitDaily10.ShouldBe(307643201);
            result.VisitDaily11.ShouldBe(46480757);
            result.VisitDaily12.ShouldBe(1427938798);
            result.VisitDaily13.ShouldBe(1981989066);
            result.VisitDaily14.ShouldBe(512231819);
            result.VisitDaily15.ShouldBe(482776998);
            result.VisitDailyCloseDate.ShouldBe(new DateTime(2000, 7, 3));
            result.VisitDailyNote.ShouldBe("33d4e47d7564480d8cf0ea214fcd2d37e60b432a59174b2e8c281db5e008f6a8c42cf639292d4adfa9e2415d2607");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new VisitDailyActionUpdateDto()
            {
                VisitDailyDate = new DateTime(2021, 9, 21),
                VisitDaily1 = 1555351001,
                VisitDaily2 = 1432134882,
                VisitDaily3 = 1522128186,
                VisitDaily4 = 196791248,
                VisitDaily5 = 445134311,
                VisitDaily6 = 1715256376,
                VisitDaily7 = 1165528903,
                VisitDaily8 = 447229642,
                VisitDaily9 = 606684173,
                VisitDaily10 = 1849827221,
                VisitDaily11 = 843433130,
                VisitDaily12 = 1185244287,
                VisitDaily13 = 40436053,
                VisitDaily14 = 1151885009,
                VisitDaily15 = 741173213,
                VisitDailyCloseDate = new DateTime(2020, 1, 15),
                VisitDailyNote = "fd428b6488254a4c86c08f1e42b1fa870d5d6fa6b5a148c1a7d80a951e0c5"
            };

            // Act
            var serviceResult = await _visitDailyActionsAppService.UpdateAsync(Guid.Parse("ea7d8753-4ba8-40a2-8475-20fe3d2c231e"), input);

            // Assert
            var result = await _visitDailyActionRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.VisitDailyDate.ShouldBe(new DateTime(2021, 9, 21));
            result.VisitDaily1.ShouldBe(1555351001);
            result.VisitDaily2.ShouldBe(1432134882);
            result.VisitDaily3.ShouldBe(1522128186);
            result.VisitDaily4.ShouldBe(196791248);
            result.VisitDaily5.ShouldBe(445134311);
            result.VisitDaily6.ShouldBe(1715256376);
            result.VisitDaily7.ShouldBe(1165528903);
            result.VisitDaily8.ShouldBe(447229642);
            result.VisitDaily9.ShouldBe(606684173);
            result.VisitDaily10.ShouldBe(1849827221);
            result.VisitDaily11.ShouldBe(843433130);
            result.VisitDaily12.ShouldBe(1185244287);
            result.VisitDaily13.ShouldBe(40436053);
            result.VisitDaily14.ShouldBe(1151885009);
            result.VisitDaily15.ShouldBe(741173213);
            result.VisitDailyCloseDate.ShouldBe(new DateTime(2020, 1, 15));
            result.VisitDailyNote.ShouldBe("fd428b6488254a4c86c08f1e42b1fa870d5d6fa6b5a148c1a7d80a951e0c5");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _visitDailyActionsAppService.DeleteAsync(Guid.Parse("ea7d8753-4ba8-40a2-8475-20fe3d2c231e"));

            // Assert
            var result = await _visitDailyActionRepository.FindAsync(c => c.Id == Guid.Parse("ea7d8753-4ba8-40a2-8475-20fe3d2c231e"));

            result.ShouldBeNull();
        }
    }
}