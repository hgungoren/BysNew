using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace ToksozBysNew.Positions
{
    public class PositionManager : DomainService
    {
        private readonly IPositionRepository _positionRepository;

        public PositionManager(IPositionRepository positionRepository)
        {
            _positionRepository = positionRepository;
        }

        public async Task<Position> CreateAsync(
        string positionCode, string positionName)
        {

            var position = new Position(
             GuidGenerator.Create(),
             positionCode, positionName
             );

            return await _positionRepository.InsertAsync(position);
        }

        public async Task<Position> UpdateAsync(
            Guid id,
            string positionCode, string positionName, [CanBeNull] string concurrencyStamp = null
        )
        {

            var position = await _positionRepository.GetAsync(id);

            position.PositionCode = positionCode;
            position.PositionName = positionName;

            position.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _positionRepository.UpdateAsync(position);
        }

    }
}