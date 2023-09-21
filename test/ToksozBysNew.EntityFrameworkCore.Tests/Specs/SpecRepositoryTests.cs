using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using ToksozBysNew.Specs;
using ToksozBysNew.EntityFrameworkCore;
using Xunit;

namespace ToksozBysNew.Specs
{
    public class SpecRepositoryTests : ToksozBysNewEntityFrameworkCoreTestBase
    {
        private readonly ISpecRepository _specRepository;

        public SpecRepositoryTests()
        {
            _specRepository = GetRequiredService<ISpecRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _specRepository.GetListAsync(
                    specCode: "3a216249951a498d89245f0083b9483a3cc14dc43bb94540942cbbae0d3e22a",
                    specName: "b2c7f92d559249869ba84c29be90e55e5"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("4ed69100-26c6-4519-9a70-9e52bcc90594"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _specRepository.GetCountAsync(
                    specCode: "3708cf618fcc4a56a9200c8858d6a3a8e5a6399422574e958610d67af4eaf19d02d68742922",
                    specName: "cfb9397894c244e09f0377db03f8e2989099e2df45c2430fb"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}