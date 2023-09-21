using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace ToksozBysNew.SpendingGroups
{
    public class SpendingGroupManager : DomainService
    {
        private readonly ISpendingGroupRepository _spendingGroupRepository;

        public SpendingGroupManager(ISpendingGroupRepository spendingGroupRepository)
        {
            _spendingGroupRepository = spendingGroupRepository;
        }

        public async Task<SpendingGroup> CreateAsync(
        string name)
        {
            var spendingGroup = new SpendingGroup(
             GuidGenerator.Create(),
             name
             );

            return await _spendingGroupRepository.InsertAsync(spendingGroup);
        }

        public async Task<SpendingGroup> UpdateAsync(
            Guid id,
            string name, [CanBeNull] string concurrencyStamp = null
        )
        {
            var queryable = await _spendingGroupRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var spendingGroup = await AsyncExecuter.FirstOrDefaultAsync(query);

            spendingGroup.Name = name;

            spendingGroup.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _spendingGroupRepository.UpdateAsync(spendingGroup);
        }

    }
}