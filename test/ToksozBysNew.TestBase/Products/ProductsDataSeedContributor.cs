using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using ToksozBysNew.Products;

namespace ToksozBysNew.Products
{
    public class ProductsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public ProductsDataSeedContributor(IProductRepository productRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _productRepository = productRepository;
            _unitOfWorkManager = unitOfWorkManager;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _productRepository.InsertAsync(new Product
            (
                id: Guid.Parse("e36ae10e-ab18-403d-a2f6-10df1e6fe8df"),
                productName: "911833d532954909aadfba8fc16de8e6975bfa3e771041f493a590ef4a1457d70edf72f088da43daafa73290b7fab85d090d"
            ));

            await _productRepository.InsertAsync(new Product
            (
                id: Guid.Parse("239d49d9-c964-42e3-a535-45938fe429c0"),
                productName: "98d42e7d4fc04529a9bfdb7863eedb4e4d7d1e7d42ae4964a514a3896d3564b99ac76bcf2a9443eeae1ce6df97bf2fabdeac"
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}