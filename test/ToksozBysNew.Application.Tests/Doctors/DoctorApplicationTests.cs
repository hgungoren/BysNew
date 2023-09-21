using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace ToksozBysNew.Doctors
{
    public class DoctorsAppServiceTests : ToksozBysNewApplicationTestBase
    {
        private readonly IDoctorsAppService _doctorsAppService;
        private readonly IRepository<Doctor, Guid> _doctorRepository;

        public DoctorsAppServiceTests()
        {
            _doctorsAppService = GetRequiredService<IDoctorsAppService>();
            _doctorRepository = GetRequiredService<IRepository<Doctor, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _doctorsAppService.GetListAsync(new GetDoctorsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Doctor.Id == Guid.Parse("a35e966e-23d5-425b-af6f-ad30e3181f8e")).ShouldBe(true);
            result.Items.Any(x => x.Doctor.Id == Guid.Parse("7ee8c5ac-96d4-495a-871c-b0bb9f003a62")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _doctorsAppService.GetAsync(Guid.Parse("a35e966e-23d5-425b-af6f-ad30e3181f8e"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("a35e966e-23d5-425b-af6f-ad30e3181f8e"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new DoctorCreateDto
            {
                IsActive = true,
                NameSurname = "2b1116ff474c41ff9e56f1b794b934f48641c7379f67424dbf4742f04b10dbe4638246b257314016ae2446",
                PharmacyName = "b1a8b4b231344dd5bc362c63756eceb32da42b8b2a334591bcbf568"
            };

            // Act
            var serviceResult = await _doctorsAppService.CreateAsync(input);

            // Assert
            var result = await _doctorRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.IsActive.ShouldBe(true);
            result.NameSurname.ShouldBe("2b1116ff474c41ff9e56f1b794b934f48641c7379f67424dbf4742f04b10dbe4638246b257314016ae2446");
            result.PharmacyName.ShouldBe("b1a8b4b231344dd5bc362c63756eceb32da42b8b2a334591bcbf568");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new DoctorUpdateDto()
            {
                IsActive = true,
                NameSurname = "04d26595117d4a25930ab3703ac986eb",
                PharmacyName = "a949102ac57f4dbd930b3ad93948a2a379660dfc11d3497d8a51e1eab9e03ed143f8"
            };

            // Act
            var serviceResult = await _doctorsAppService.UpdateAsync(Guid.Parse("a35e966e-23d5-425b-af6f-ad30e3181f8e"), input);

            // Assert
            var result = await _doctorRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.IsActive.ShouldBe(true);
            result.NameSurname.ShouldBe("04d26595117d4a25930ab3703ac986eb");
            result.PharmacyName.ShouldBe("a949102ac57f4dbd930b3ad93948a2a379660dfc11d3497d8a51e1eab9e03ed143f8");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _doctorsAppService.DeleteAsync(Guid.Parse("a35e966e-23d5-425b-af6f-ad30e3181f8e"));

            // Assert
            var result = await _doctorRepository.FindAsync(c => c.Id == Guid.Parse("a35e966e-23d5-425b-af6f-ad30e3181f8e"));

            result.ShouldBeNull();
        }
    }
}