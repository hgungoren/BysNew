using ToksozBysNew.Bricks;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using ToksozBysNew.Units;

namespace ToksozBysNew.Units
{
    public class UnitsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IUnitRepository _unitRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly BricksDataSeedContributor _bricksDataSeedContributor;

        public UnitsDataSeedContributor(IUnitRepository unitRepository, IUnitOfWorkManager unitOfWorkManager, BricksDataSeedContributor bricksDataSeedContributor)
        {
            _unitRepository = unitRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _bricksDataSeedContributor = bricksDataSeedContributor;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _bricksDataSeedContributor.SeedAsync(context);

            await _unitRepository.InsertAsync(new Unit
            (
                id: Guid.Parse("9238db44-8200-4eed-95da-631df0726a12"),
                unitName: "41e1edee7c994ae6a7fe0d9491b1d048b9958a3ec58a4d13876cc38e",
                brickId: null
            ));

            await _unitRepository.InsertAsync(new Unit
            (
                id: Guid.Parse("316d5039-b574-47c8-951f-2b21c36490bb"),
                unitName: "c166c71ac43f4c27b0cd1b01237f0e3c5853faa62f6e49b88c70c29ec4c90",
                brickId: null
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}