using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using ToksozBysNew.AccountGroups;

namespace ToksozBysNew.AccountGroups
{
    public class AccountGroupsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IAccountGroupRepository _accountGroupRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public AccountGroupsDataSeedContributor(IAccountGroupRepository accountGroupRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _accountGroupRepository = accountGroupRepository;
            _unitOfWorkManager = unitOfWorkManager;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _accountGroupRepository.InsertAsync(new AccountGroup
            (
                id: Guid.Parse("bbc2929d-a3fc-4fbc-afed-39c6ddafe257"),
                accountGroupName: "bcedd86c071f47dc8b7ff73611eb8e9b903dcf7d77564263aa65c34b73336b24c25309176f084d758b52978042aa9e5eb7306aab9a854e228a99fb8bb5a3a7676ee0ab6b1d974ccc8eea30eaceebd10e28541a3c8032484db80ebe579a2188d1d5acae10291142df9a1d68c597460d4cfe74b1afd60a4428a4cb483c5dfffdd",
                isUnitEnterable: true
            ));

            await _accountGroupRepository.InsertAsync(new AccountGroup
            (
                id: Guid.Parse("5d3e6ada-be2e-42b5-9a0e-521681041088"),
                accountGroupName: "174b7821ba3d4edb813a13c330fd24e86c4d92b5a9244f978b2f7bd440b5a98574ae8ee655cb408dbf6ead6534d062ed0e1dad018abc47e382625b084cf8544343f77beb873043a0b73a0edeac86e285b60d17e39cf34242a4d9a42fd36c61164fe30e6b689c4b9ebc0d008591e08f2a9ac89384573c4555b60d76c94b095aa",
                isUnitEnterable: true
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}