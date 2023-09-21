using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using ToksozBysNew.Departments;
using ToksozBysNew.EntityFrameworkCore;
using Xunit;

namespace ToksozBysNew.Departments
{
    public class DepartmentRepositoryTests : ToksozBysNewEntityFrameworkCoreTestBase
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentRepositoryTests()
        {
            _departmentRepository = GetRequiredService<IDepartmentRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _departmentRepository.GetListAsync(
                    departmentName: "dad2b7006d0844fba78472320cc6cc5475009371fbed43c98b2cc9fedee0c78621f1163dced44825ae66566cce58f2c96eba7dc1724647f286778046c435c8cd009745c08503479593263f"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("1b174f96-2c84-4616-8d5d-7469f680b366"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _departmentRepository.GetCountAsync(
                    departmentName: "d91fb687809c49c4a87143d119338c3b5abb580dac5246a289adf14fa0ba72dc5b5e22b7faa247288bfe5375b77cb4adc305fd8eb47a4214bd8f3d180e16a8c577eed0e8937046de9b74b2"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}