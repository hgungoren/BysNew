using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using ToksozBysNew.CustomerTitles;

namespace ToksozBysNew.CustomerTitles
{
    public class CustomerTitlesDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly ICustomerTitleRepository _customerTitleRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public CustomerTitlesDataSeedContributor(ICustomerTitleRepository customerTitleRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _customerTitleRepository = customerTitleRepository;
            _unitOfWorkManager = unitOfWorkManager;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _customerTitleRepository.InsertAsync(new CustomerTitle
            (
                id: Guid.Parse("3d057538-9269-4c85-b4c0-a7d2fdbfe3b5"),
                titleName: "614656effd494506adcbaae80143e6724a28697e891a4b7ca5664f8e364724a4904047468b1e4f418236398de9db9b"
            ));

            await _customerTitleRepository.InsertAsync(new CustomerTitle
            (
                id: Guid.Parse("94b86765-49f2-49fd-9df3-60da564009a1"),
                titleName: "f730c5f034b5431c8f1b3736ad1174ff92c5"
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}