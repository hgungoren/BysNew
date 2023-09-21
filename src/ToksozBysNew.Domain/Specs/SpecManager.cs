using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace ToksozBysNew.Specs
{
    public class SpecManager : DomainService
    {
        private readonly ISpecRepository _specRepository;

        public SpecManager(ISpecRepository specRepository)
        {
            _specRepository = specRepository;
        }

        public async Task<Spec> CreateAsync(
        string specCode, string specName)
        {

            var spec = new Spec(
             GuidGenerator.Create(),
             specCode, specName
             );

            return await _specRepository.InsertAsync(spec);
        }

        public async Task<Spec> UpdateAsync(
            Guid id,
            string specCode, string specName, [CanBeNull] string concurrencyStamp = null
        )
        {

            var spec = await _specRepository.GetAsync(id);

            spec.SpecCode = specCode;
            spec.SpecName = specName;

            spec.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _specRepository.UpdateAsync(spec);
        }

    }
}