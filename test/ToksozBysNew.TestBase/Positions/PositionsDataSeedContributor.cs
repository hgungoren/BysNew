using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using ToksozBysNew.Positions;

namespace ToksozBysNew.Positions
{
    public class PositionsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IPositionRepository _positionRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public PositionsDataSeedContributor(IPositionRepository positionRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _positionRepository = positionRepository;
            _unitOfWorkManager = unitOfWorkManager;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _positionRepository.InsertAsync(new Position
            (
                id: Guid.Parse("491e8315-8ffe-458d-a483-9e9f5ba8e394"),
                positionCode: "9710d65208d342ca9437494c5dce000f21bc596f2bc6451481b08c30f75c3a22a79954456c0b49bda659ec186fd5f7f70",
                positionName: "071492897a2749989fc7c4288d"
            ));

            await _positionRepository.InsertAsync(new Position
            (
                id: Guid.Parse("fd327612-d00d-4d6a-b0e5-7c88b971a5f1"),
                positionCode: "2467e379c5994aa38a96d7f62723b2f464221214a80f4ecfbddee0433d68f204f",
                positionName: "00623fa0b5dc458787371d04f71b55e26ce7"
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}