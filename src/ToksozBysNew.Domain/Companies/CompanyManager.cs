using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace ToksozBysNew.Companies
{
    public class CompanyManager : DomainService
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyManager(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<Company> CreateAsync(
        string companyName, bool isActive)
        {
            var company = new Company(
             GuidGenerator.Create(),
             companyName, isActive
             );

            return await _companyRepository.InsertAsync(company);
        }

        public async Task<Company> UpdateAsync(
            Guid id,
            string companyName, bool isActive, [CanBeNull] string concurrencyStamp = null
        )
        {
            var queryable = await _companyRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var company = await AsyncExecuter.FirstOrDefaultAsync(query);

            company.CompanyName = companyName;
            company.IsActive = isActive;

            company.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _companyRepository.UpdateAsync(company);
        }

    }
}