using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using ToksozBysNew.SpendingGroups;

namespace ToksozBysNew.SpendingGroups
{
    public class SpendingGroupsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly ISpendingGroupRepository _spendingGroupRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public SpendingGroupsDataSeedContributor(ISpendingGroupRepository spendingGroupRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _spendingGroupRepository = spendingGroupRepository;
            _unitOfWorkManager = unitOfWorkManager;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _spendingGroupRepository.InsertAsync(new SpendingGroup
            (
                id: Guid.Parse("cc378bb3-3f05-459c-9259-1e59d3d267ef"),
                name: "62cbb81"
            ));

            await _spendingGroupRepository.InsertAsync(new SpendingGroup
            (
                id: Guid.Parse("e861d57e-a7c4-48ab-9857-c5a8c3a7edfa"),
                name: "b16401ec066d41fab583d02213553eed06f1f3be113a4782ae7df78bb3cf80ba71c15e87b70343ca87a8bf06a3e641"
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}