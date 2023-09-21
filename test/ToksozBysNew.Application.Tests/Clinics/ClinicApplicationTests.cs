using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace ToksozBysNew.Clinics
{
    public class ClinicsAppServiceTests : ToksozBysNewApplicationTestBase
    {
        private readonly IClinicsAppService _clinicsAppService;
        private readonly IRepository<Clinic, Guid> _clinicRepository;

        public ClinicsAppServiceTests()
        {
            _clinicsAppService = GetRequiredService<IClinicsAppService>();
            _clinicRepository = GetRequiredService<IRepository<Clinic, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _clinicsAppService.GetListAsync(new GetClinicsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Clinic.Id == Guid.Parse("66b66471-2b93-4826-bdd3-f7ac1cec9a93")).ShouldBe(true);
            result.Items.Any(x => x.Clinic.Id == Guid.Parse("2bbd8c96-482b-4815-861a-9a592588ba23")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _clinicsAppService.GetAsync(Guid.Parse("66b66471-2b93-4826-bdd3-f7ac1cec9a93"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("66b66471-2b93-4826-bdd3-f7ac1cec9a93"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new ClinicCreateDto
            {
                ClinicName = "e1f8bee5920447cc814e8f5c0db176cb8facc862be534f0a8cb"
            };

            // Act
            var serviceResult = await _clinicsAppService.CreateAsync(input);

            // Assert
            var result = await _clinicRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.ClinicName.ShouldBe("e1f8bee5920447cc814e8f5c0db176cb8facc862be534f0a8cb");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new ClinicUpdateDto()
            {
                ClinicName = "f956d4860f2c40c8a58ea1d42d8765cb281ece603f3e4ebcac9622ce384b784d05211e60ecb5484899942cb9cac50df522"
            };

            // Act
            var serviceResult = await _clinicsAppService.UpdateAsync(Guid.Parse("66b66471-2b93-4826-bdd3-f7ac1cec9a93"), input);

            // Assert
            var result = await _clinicRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.ClinicName.ShouldBe("f956d4860f2c40c8a58ea1d42d8765cb281ece603f3e4ebcac9622ce384b784d05211e60ecb5484899942cb9cac50df522");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _clinicsAppService.DeleteAsync(Guid.Parse("66b66471-2b93-4826-bdd3-f7ac1cec9a93"));

            // Assert
            var result = await _clinicRepository.FindAsync(c => c.Id == Guid.Parse("66b66471-2b93-4826-bdd3-f7ac1cec9a93"));

            result.ShouldBeNull();
        }
    }
}