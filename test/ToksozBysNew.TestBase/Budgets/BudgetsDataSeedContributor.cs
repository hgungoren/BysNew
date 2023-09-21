using ToksozBysNew.Companies;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using ToksozBysNew.Budgets;

namespace ToksozBysNew.Budgets
{
    public class BudgetsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IBudgetRepository _budgetRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly CompaniesDataSeedContributor _companiesDataSeedContributor;

        public BudgetsDataSeedContributor(IBudgetRepository budgetRepository, IUnitOfWorkManager unitOfWorkManager, CompaniesDataSeedContributor companiesDataSeedContributor)
        {
            _budgetRepository = budgetRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _companiesDataSeedContributor = companiesDataSeedContributor;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _companiesDataSeedContributor.SeedAsync(context);

            await _budgetRepository.InsertAsync(new Budget
            (
                id: Guid.Parse("c175814f-d2bb-4939-a226-080e2f05ad6f"),
                budgetName: "f9b8bc1fd40d49dfa47ae6bbdcba6e63f947ddc9419e478191",
                year: 1807151740,
                comment: "d930dd06e72649f9a6d931589c5e4b810e2ca8f1b84140f7afca56d8c34762e523a8e68b174749e8a1c9b1fa215f1a0df2e0ee07cf3040b388fa9de26e8708ab79ce526c03904edea8be0760c613258b484fc7722c084647ab94adefd36f972b0b7bba69eb704d9a9cb0b93d26b3414bd1626690a335486d84c7c2515090148",
                isActive: true,
                openUntil: new DateTime(2016, 9, 4),
                companyId: null
            ));

            await _budgetRepository.InsertAsync(new Budget
            (
                id: Guid.Parse("c894d8dd-58ca-4b3e-8456-a05842e69d1f"),
                budgetName: "c0a9857ac01244da89f5c1ba47544361fedca4630f784ecdba",
                year: 1795075967,
                comment: "1de319b2225841bc920de44b2910bcaa604f24bc244d4f3796bd3a16a6839a6ebacbdc5162a04059b102e0d42ceaad441542eac8740f4222994a26ccea9c16aae61150e3672248c196892512e4e4f091f6e74389aac142669efd37f3dc2b93387f68b109a1124736b6d08a79e9745194d7de77b34e054db29626e33f76ab593",
                isActive: true,
                openUntil: new DateTime(2015, 6, 4),
                companyId: null
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}