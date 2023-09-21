using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using ToksozBysNew.AccountGroups;
using ToksozBysNew.EntityFrameworkCore;
using Xunit;

namespace ToksozBysNew.AccountGroups
{
    public class AccountGroupRepositoryTests : ToksozBysNewEntityFrameworkCoreTestBase
    {
        private readonly IAccountGroupRepository _accountGroupRepository;

        public AccountGroupRepositoryTests()
        {
            _accountGroupRepository = GetRequiredService<IAccountGroupRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _accountGroupRepository.GetListAsync(
                    accountGroupName: "bcedd86c071f47dc8b7ff73611eb8e9b903dcf7d77564263aa65c34b73336b24c25309176f084d758b52978042aa9e5eb7306aab9a854e228a99fb8bb5a3a7676ee0ab6b1d974ccc8eea30eaceebd10e28541a3c8032484db80ebe579a2188d1d5acae10291142df9a1d68c597460d4cfe74b1afd60a4428a4cb483c5dfffdd",
                    isUnitEnterable: true
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("bbc2929d-a3fc-4fbc-afed-39c6ddafe257"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _accountGroupRepository.GetCountAsync(
                    accountGroupName: "174b7821ba3d4edb813a13c330fd24e86c4d92b5a9244f978b2f7bd440b5a98574ae8ee655cb408dbf6ead6534d062ed0e1dad018abc47e382625b084cf8544343f77beb873043a0b73a0edeac86e285b60d17e39cf34242a4d9a42fd36c61164fe30e6b689c4b9ebc0d008591e08f2a9ac89384573c4555b60d76c94b095aa",
                    isUnitEnterable: true
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}