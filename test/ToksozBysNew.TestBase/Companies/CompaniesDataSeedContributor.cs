using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using ToksozBysNew.Companies;

namespace ToksozBysNew.Companies
{
    public class CompaniesDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly ICompanyRepository _companyRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public CompaniesDataSeedContributor(ICompanyRepository companyRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _companyRepository = companyRepository;
            _unitOfWorkManager = unitOfWorkManager;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _companyRepository.InsertAsync(new Company
            (
                id: Guid.Parse("7d3f3766-a5f8-421e-8696-bf63ca6a302f"),
                companyName: "4a665442c45e4d68bee0c0c8e7486ecbc505ea98fb694a43b8",
                isActive: true
            ));

            await _companyRepository.InsertAsync(new Company
            (
                id: Guid.Parse("da810093-82b5-41cf-abab-5098585b385a"),
                companyName: "b0be913dbd774f4ba38ec1ed6fef17535a8179198b3a4431a3",
                isActive: true
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}