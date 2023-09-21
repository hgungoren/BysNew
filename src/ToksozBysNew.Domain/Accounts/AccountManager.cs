using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace ToksozBysNew.Accounts
{
    public class AccountManager : DomainService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountManager(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<Account> CreateAsync(
        string accountCode, string accountName, string description, bool isActive)
        {
            var account = new Account(
             GuidGenerator.Create(),
             accountCode, accountName, description, isActive
             );

            return await _accountRepository.InsertAsync(account);
        }

        public async Task<Account> UpdateAsync(
            Guid id,
            string accountCode, string accountName, string description, bool isActive, [CanBeNull] string concurrencyStamp = null
        )
        {
            var queryable = await _accountRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var account = await AsyncExecuter.FirstOrDefaultAsync(query);

            account.AccountCode = accountCode;
            account.AccountName = accountName;
            account.Description = description;
            account.IsActive = isActive;

            account.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _accountRepository.UpdateAsync(account);
        }

    }
}