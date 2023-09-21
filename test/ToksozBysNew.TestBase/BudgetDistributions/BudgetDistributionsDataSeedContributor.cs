using ToksozBysNew.Accounts;
using ToksozBysNew.AccountGroups;
using ToksozBysNew.Budgets;
using ToksozBysNew.Products;
using ToksozBysNew.Departments;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using ToksozBysNew.BudgetDistributions;

namespace ToksozBysNew.BudgetDistributions
{
    public class BudgetDistributionsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IBudgetDistributionRepository _budgetDistributionRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly DepartmentsDataSeedContributor _departmentsDataSeedContributor;

        private readonly ProductsDataSeedContributor _productsDataSeedContributor;

        private readonly BudgetsDataSeedContributor _budgetsDataSeedContributor;

        private readonly AccountGroupsDataSeedContributor _accountGroupsDataSeedContributor;

        private readonly AccountsDataSeedContributor _accountsDataSeedContributor;

        public BudgetDistributionsDataSeedContributor(IBudgetDistributionRepository budgetDistributionRepository, IUnitOfWorkManager unitOfWorkManager, DepartmentsDataSeedContributor departmentsDataSeedContributor, ProductsDataSeedContributor productsDataSeedContributor, BudgetsDataSeedContributor budgetsDataSeedContributor, AccountGroupsDataSeedContributor accountGroupsDataSeedContributor, AccountsDataSeedContributor accountsDataSeedContributor)
        {
            _budgetDistributionRepository = budgetDistributionRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _departmentsDataSeedContributor = departmentsDataSeedContributor; _productsDataSeedContributor = productsDataSeedContributor; _budgetsDataSeedContributor = budgetsDataSeedContributor; _accountGroupsDataSeedContributor = accountGroupsDataSeedContributor; _accountsDataSeedContributor = accountsDataSeedContributor;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _departmentsDataSeedContributor.SeedAsync(context);
            await _productsDataSeedContributor.SeedAsync(context);
            await _budgetsDataSeedContributor.SeedAsync(context);
            await _accountGroupsDataSeedContributor.SeedAsync(context);
            await _accountsDataSeedContributor.SeedAsync(context);

            await _budgetDistributionRepository.InsertAsync(new BudgetDistribution
            (
                id: Guid.Parse("2adf600c-203a-4ffe-ad5a-748b09b557b0"),
                costCenter: "602ff66c11f84a7fbed7ec41a26a1b54b0209a2d0f38468aa8",
                expenseType: "f374756207f84f4e841fbfaa7cd6845e6aa06c08aacd45e0a5458eecc32cac2b12a58a43469c4d518515a7c48a3c43fabf0e6a5926ad4544b5d5acfcc1fb989fcbe31e2656e24c1285234eb6aafa5b8b8dd87d30a3864e58bc7653a11b80184c2fc91a25ff03430ca563cc7c3a19b4e17fd390a0a1fd441c8b5741a6ee8d520fd3ad877afce244c7a590979e63707b1f93783b4a697040df9a312b21ec821494e3694a9fd0e94078be699a2b9319d460f0db842f323947cbaf0b85af890f8da4ddbeb431db1f4495809fd769f81395de99485f6a88a04d17adce0b9bce79863f005baf39b50d4f369b1683c29a6137c9797c34e3d6b74032b1b5e888574b27a0561fccdcaa014654b04ac4400e82580d115bc2e7314745b9bee779a70e9dd9fa90d2cb9239df43e9af65505e",
                projectItem: 1676866971,
                type: 832154661,
                unit: 1783166446,
                unitValue: 1202919867,
                month: 351579813,
                year: 1853636780,
                ratio: 839550050,
                amount: 514943931,
                memo: 1008232021,
                invoice: 681772701,
                currency: 1036571519,
                currencyAmount: 1372994908,
                expenseCategory: 681520537,
                expenseNecessity: 1796052380,
                comment: "9498caf0e7704ae2ab864b3a069bf048cde07044097a48acbf0e30b887",
                status: "7c450",
                approval: 1808338820,
                isActive: true,
                departmentId: Guid.Parse("1b174f96-2c84-4616-8d5d-7469f680b366"),
                productId: null,
                budgetId: Guid.Parse("c175814f-d2bb-4939-a226-080e2f05ad6f"),
                accountGroupId: null,
                accountId: null,
                identityUserId: null
            ));

            await _budgetDistributionRepository.InsertAsync(new BudgetDistribution
            (
                id: Guid.Parse("66ab922e-f314-485f-9649-81e0c5825e0d"),
                costCenter: "9a94d0c8b86f4e219f39e9ebfc9420b7fa6274a4a3fa4677b1",
                expenseType: "e0a8b112abf1451d8e2aeb649974f41f1c1b6aebdce6403fa0e4e9e9ac008ffacc0f4c282e6942208e5d5a6d149c0f4fedf9f56fffde42e291a7816d83075266bd6a65b3ae844ca7ba07e00500187c061d8231d5b99c4d8585464efd6971131019783d45969e4cd794dddcb0c34103d5f6b1265c115440a2bc4035e09c34dd25bd78d69910af424092f43c6da658d2f5dd9ef31342ab4a7dabb9b97f05395997093717934e8f4e0b86282473ab850405a553fab379294596b7c0229f924e918bd1f3cbb889044113a4e9dff46326524fb5f0299720024456abc003c314cb6453c738552cf48147c483e776281f3c99510e33a78d38cf4c83bcccccf4d2069d19447c5b8747464479bcbca87abedc5fffc4dad12ea9284c96aeb44fc13fb91bc78dddea67272b4f5992cc3fec",
                projectItem: 213537220,
                type: 112968406,
                unit: 1248195199,
                unitValue: 147748611,
                month: 1148483028,
                year: 1801501616,
                ratio: 1388014428,
                amount: 2031836426,
                memo: 553686801,
                invoice: 1192743066,
                currency: 1150078942,
                currencyAmount: 1393926293,
                expenseCategory: 776703680,
                expenseNecessity: 638830236,
                comment: "c292b8f0d6144df59b9dd65ffd6",
                status: "82ecf",
                approval: 1664751463,
                isActive: true,
                departmentId: Guid.Parse("1b174f96-2c84-4616-8d5d-7469f680b366"),
                productId: null,
                budgetId: Guid.Parse("c175814f-d2bb-4939-a226-080e2f05ad6f"),
                accountGroupId: null,
                accountId: null,
                identityUserId: null
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}