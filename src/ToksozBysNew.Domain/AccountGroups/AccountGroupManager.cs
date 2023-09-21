using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace ToksozBysNew.AccountGroups
{
    public class AccountGroupManager : DomainService
    {
        private readonly IAccountGroupRepository _accountGroupRepository;

        public AccountGroupManager(IAccountGroupRepository accountGroupRepository)
        {
            _accountGroupRepository = accountGroupRepository;
        }

        public async Task<AccountGroup> CreateAsync(
        string accountGroupName, bool isUnitEnterable)
        {
            var accountGroup = new AccountGroup(
             GuidGenerator.Create(),
             accountGroupName, isUnitEnterable
             );

            return await _accountGroupRepository.InsertAsync(accountGroup);
        }

        public async Task<AccountGroup> UpdateAsync(
            Guid id,
            string accountGroupName, bool isUnitEnterable, [CanBeNull] string concurrencyStamp = null
        )
        {
            var queryable = await _accountGroupRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var accountGroup = await AsyncExecuter.FirstOrDefaultAsync(query);

            accountGroup.AccountGroupName = accountGroupName;
            accountGroup.IsUnitEnterable = isUnitEnterable;

            accountGroup.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _accountGroupRepository.UpdateAsync(accountGroup);
        }

    }
}