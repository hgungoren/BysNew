using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace ToksozBysNew.Units
{
    public class UnitManager : DomainService
    {
        private readonly IUnitRepository _unitRepository;

        public UnitManager(IUnitRepository unitRepository)
        {
            _unitRepository = unitRepository;
        }

        public async Task<Unit> CreateAsync(
        Guid? brickId, string unitName)
        {

            var unit = new Unit(
             GuidGenerator.Create(),
             brickId, unitName
             );

            return await _unitRepository.InsertAsync(unit);
        }

        public async Task<Unit> UpdateAsync(
            Guid id,
            Guid? brickId, string unitName, [CanBeNull] string concurrencyStamp = null
        )
        {

            var unit = await _unitRepository.GetAsync(id);

            unit.BrickId = brickId;
            unit.UnitName = unitName;

            unit.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _unitRepository.UpdateAsync(unit);
        }

    }
}