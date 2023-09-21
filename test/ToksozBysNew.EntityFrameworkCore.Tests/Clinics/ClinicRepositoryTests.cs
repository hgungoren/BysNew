using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using ToksozBysNew.Clinics;
using ToksozBysNew.EntityFrameworkCore;
using Xunit;

namespace ToksozBysNew.Clinics
{
    public class ClinicRepositoryTests : ToksozBysNewEntityFrameworkCoreTestBase
    {
        private readonly IClinicRepository _clinicRepository;

        public ClinicRepositoryTests()
        {
            _clinicRepository = GetRequiredService<IClinicRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _clinicRepository.GetListAsync(
                    clinicName: "460559137ebd4471a4e9cb5a0a4cc15354113f22311c49babbe85e3cb59d40db9f29b16b55d64231a6b665619d"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("66b66471-2b93-4826-bdd3-f7ac1cec9a93"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _clinicRepository.GetCountAsync(
                    clinicName: "f55b6aca14c04ee3a0ad511b4feb69a94ac0c5fa24db47d98cdc80e1f0b82d4a0685"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}