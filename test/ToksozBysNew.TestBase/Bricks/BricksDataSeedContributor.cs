using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using ToksozBysNew.Bricks;

namespace ToksozBysNew.Bricks
{
    public class BricksDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IBrickRepository _brickRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public BricksDataSeedContributor(IBrickRepository brickRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _brickRepository = brickRepository;
            _unitOfWorkManager = unitOfWorkManager;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _brickRepository.InsertAsync(new Brick
            (
                id: Guid.Parse("cb141fd3-dc82-4923-b8b3-2d8a41cd9908"),
                brickName: "620718b744964f58bf6092cf56c8"
            ));

            await _brickRepository.InsertAsync(new Brick
            (
                id: Guid.Parse("d9dbb40d-1c41-4143-a8df-263b85f69967"),
                brickName: "a5174e0389054232a485753f33bdf2b533a087c27264437abf764c93086b2191ff7f63b6541346cf97b09439c2187c1d35"
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}