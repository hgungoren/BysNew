using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using ToksozBysNew.CustomerTypes;

namespace ToksozBysNew.CustomerTypes
{
    public class CustomerTypesDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly ICustomerTypeRepository _customerTypeRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public CustomerTypesDataSeedContributor(ICustomerTypeRepository customerTypeRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _customerTypeRepository = customerTypeRepository;
            _unitOfWorkManager = unitOfWorkManager;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _customerTypeRepository.InsertAsync(new CustomerType
            (
                id: Guid.Parse("fb807768-3ca4-4e77-aafa-9a79829f5ca4"),
                typeName: "35992b28c9cb402f978df7b8abec6baaa954bb5b851a429489167e08"
            ));

            await _customerTypeRepository.InsertAsync(new CustomerType
            (
                id: Guid.Parse("94d54d9f-4dd2-4498-9763-a09d4ded569a"),
                typeName: "e2896c801ada49b2b118748ec107a703001ae908cf884f4fa5539965c62"
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}