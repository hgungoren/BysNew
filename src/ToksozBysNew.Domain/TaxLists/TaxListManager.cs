using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace ToksozBysNew.TaxLists
{
    public class TaxListManager : DomainService
    {
        private readonly ITaxListRepository _taxListRepository;

        public TaxListManager(ITaxListRepository taxListRepository)
        {
            _taxListRepository = taxListRepository;
        }

        public async Task<TaxList> CreateAsync(
        string taxName, int taxValue)
        {
            Check.Range(taxValue, nameof(taxValue), TaxListConsts.TaxValueMinLength, TaxListConsts.TaxValueMaxLength);

            var taxList = new TaxList(
             GuidGenerator.Create(),
             taxName, taxValue
             );

            return await _taxListRepository.InsertAsync(taxList);
        }

        public async Task<TaxList> UpdateAsync(
            Guid id,
            string taxName, int taxValue, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.Range(taxValue, nameof(taxValue), TaxListConsts.TaxValueMinLength, TaxListConsts.TaxValueMaxLength);

            var taxList = await _taxListRepository.GetAsync(id);

            taxList.TaxName = taxName;
            taxList.TaxValue = taxValue;

            taxList.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _taxListRepository.UpdateAsync(taxList);
        }

    }
}