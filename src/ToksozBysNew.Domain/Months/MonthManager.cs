using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace ToksozBysNew.Months
{
    public class MonthManager : DomainService
    {
        private readonly IMonthRepository _monthRepository;

        public MonthManager(IMonthRepository monthRepository)
        {
            _monthRepository = monthRepository;
        }

        public async Task<Month> CreateAsync(
        string name)
        {
            var month = new Month(
             GuidGenerator.Create(),
             name
             );

            return await _monthRepository.InsertAsync(month);
        }

        public async Task<Month> UpdateAsync(
            Guid id,
            string name, [CanBeNull] string concurrencyStamp = null
        )
        {
            var queryable = await _monthRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var month = await AsyncExecuter.FirstOrDefaultAsync(query);

            month.Name = name;

            month.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _monthRepository.UpdateAsync(month);
        }

    }
}