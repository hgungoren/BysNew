using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using ToksozBysNew.BudgetDistributions;
using ToksozBysNew.EntityFrameworkCore;
using Xunit;

namespace ToksozBysNew.BudgetDistributions
{
    public class BudgetDistributionRepositoryTests : ToksozBysNewEntityFrameworkCoreTestBase
    {
        private readonly IBudgetDistributionRepository _budgetDistributionRepository;

        public BudgetDistributionRepositoryTests()
        {
            _budgetDistributionRepository = GetRequiredService<IBudgetDistributionRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _budgetDistributionRepository.GetListAsync(
                    costCenter: "602ff66c11f84a7fbed7ec41a26a1b54b0209a2d0f38468aa8",
                    expenseType: "f374756207f84f4e841fbfaa7cd6845e6aa06c08aacd45e0a5458eecc32cac2b12a58a43469c4d518515a7c48a3c43fabf0e6a5926ad4544b5d5acfcc1fb989fcbe31e2656e24c1285234eb6aafa5b8b8dd87d30a3864e58bc7653a11b80184c2fc91a25ff03430ca563cc7c3a19b4e17fd390a0a1fd441c8b5741a6ee8d520fd3ad877afce244c7a590979e63707b1f93783b4a697040df9a312b21ec821494e3694a9fd0e94078be699a2b9319d460f0db842f323947cbaf0b85af890f8da4ddbeb431db1f4495809fd769f81395de99485f6a88a04d17adce0b9bce79863f005baf39b50d4f369b1683c29a6137c9797c34e3d6b74032b1b5e888574b27a0561fccdcaa014654b04ac4400e82580d115bc2e7314745b9bee779a70e9dd9fa90d2cb9239df43e9af65505e",
                    comment: "9498caf0e7704ae2ab864b3a069bf048cde07044097a48acbf0e30b887",
                    status: "7c450",
                    isActive: true
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("2adf600c-203a-4ffe-ad5a-748b09b557b0"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _budgetDistributionRepository.GetCountAsync(
                    costCenter: "9a94d0c8b86f4e219f39e9ebfc9420b7fa6274a4a3fa4677b1",
                    expenseType: "e0a8b112abf1451d8e2aeb649974f41f1c1b6aebdce6403fa0e4e9e9ac008ffacc0f4c282e6942208e5d5a6d149c0f4fedf9f56fffde42e291a7816d83075266bd6a65b3ae844ca7ba07e00500187c061d8231d5b99c4d8585464efd6971131019783d45969e4cd794dddcb0c34103d5f6b1265c115440a2bc4035e09c34dd25bd78d69910af424092f43c6da658d2f5dd9ef31342ab4a7dabb9b97f05395997093717934e8f4e0b86282473ab850405a553fab379294596b7c0229f924e918bd1f3cbb889044113a4e9dff46326524fb5f0299720024456abc003c314cb6453c738552cf48147c483e776281f3c99510e33a78d38cf4c83bcccccf4d2069d19447c5b8747464479bcbca87abedc5fffc4dad12ea9284c96aeb44fc13fb91bc78dddea67272b4f5992cc3fec",
                    comment: "c292b8f0d6144df59b9dd65ffd6",
                    status: "82ecf",
                    isActive: true
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}