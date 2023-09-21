using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace ToksozBysNew.Departments
{
    public class DepartmentsAppServiceTests : ToksozBysNewApplicationTestBase
    {
        private readonly IDepartmentsAppService _departmentsAppService;
        private readonly IRepository<Department, Guid> _departmentRepository;

        public DepartmentsAppServiceTests()
        {
            _departmentsAppService = GetRequiredService<IDepartmentsAppService>();
            _departmentRepository = GetRequiredService<IRepository<Department, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _departmentsAppService.GetListAsync(new GetDepartmentsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Department.Id == Guid.Parse("1b174f96-2c84-4616-8d5d-7469f680b366")).ShouldBe(true);
            result.Items.Any(x => x.Department.Id == Guid.Parse("e033e52b-c1fa-4979-b824-3804502bb809")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _departmentsAppService.GetAsync(Guid.Parse("1b174f96-2c84-4616-8d5d-7469f680b366"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("1b174f96-2c84-4616-8d5d-7469f680b366"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new DepartmentCreateDto
            {
                DepartmentName = "a35a64dde6f1438f94de09f6c5204af708c54f32ab8144b382bcea8de2f8df87e54b917c7bae4956b5266319a12b27384f6418e0565c4198b02543bc45e837a5c30bd349f18b4ae8973fb3"
            };

            // Act
            var serviceResult = await _departmentsAppService.CreateAsync(input);

            // Assert
            var result = await _departmentRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.DepartmentName.ShouldBe("a35a64dde6f1438f94de09f6c5204af708c54f32ab8144b382bcea8de2f8df87e54b917c7bae4956b5266319a12b27384f6418e0565c4198b02543bc45e837a5c30bd349f18b4ae8973fb3");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new DepartmentUpdateDto()
            {
                DepartmentName = "cfcd8f17b58c4c1b81c8377e5499f6f3791c6d60898d4ef3a3eac1d7df400ba84f5aacff24a94be588f3e4810de9e7ae8445a76e83f94b86bae4e5dd263dd2aaf49de8672403445585b686"
            };

            // Act
            var serviceResult = await _departmentsAppService.UpdateAsync(Guid.Parse("1b174f96-2c84-4616-8d5d-7469f680b366"), input);

            // Assert
            var result = await _departmentRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.DepartmentName.ShouldBe("cfcd8f17b58c4c1b81c8377e5499f6f3791c6d60898d4ef3a3eac1d7df400ba84f5aacff24a94be588f3e4810de9e7ae8445a76e83f94b86bae4e5dd263dd2aaf49de8672403445585b686");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _departmentsAppService.DeleteAsync(Guid.Parse("1b174f96-2c84-4616-8d5d-7469f680b366"));

            // Assert
            var result = await _departmentRepository.FindAsync(c => c.Id == Guid.Parse("1b174f96-2c84-4616-8d5d-7469f680b366"));

            result.ShouldBeNull();
        }
    }
}