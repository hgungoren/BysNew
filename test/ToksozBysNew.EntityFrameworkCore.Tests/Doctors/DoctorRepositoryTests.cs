using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using ToksozBysNew.Doctors;
using ToksozBysNew.EntityFrameworkCore;
using Xunit;

namespace ToksozBysNew.Doctors
{
    public class DoctorRepositoryTests : ToksozBysNewEntityFrameworkCoreTestBase
    {
        private readonly IDoctorRepository _doctorRepository;

        public DoctorRepositoryTests()
        {
            _doctorRepository = GetRequiredService<IDoctorRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _doctorRepository.GetListAsync(
                    isActive: true,
                    nameSurname: "03ff46938aa54fbea1c44a353",
                    pharmacyName: "2b7cc7a157d44206b8653c47e5bbeba2a375ec5b804547d997e7cefc65aa22e14b023a4"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("a35e966e-23d5-425b-af6f-ad30e3181f8e"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _doctorRepository.GetCountAsync(
                    isActive: true,
                    nameSurname: "b69f86757e1e42598",
                    pharmacyName: "2b9db55a853d488f8bef841283d1dafb9b1e02bdab7c47618eb171ee5876c65db2984c8f6201464395354e2896d5cacd"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}