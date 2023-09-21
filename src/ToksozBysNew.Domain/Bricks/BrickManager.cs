using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace ToksozBysNew.Bricks
{
    public class BrickManager : DomainService
    {
        private readonly IBrickRepository _brickRepository;

        public BrickManager(IBrickRepository brickRepository)
        {
            _brickRepository = brickRepository;
        }

        public async Task<Brick> CreateAsync(
        string brickName)
        {

            var brick = new Brick(
             GuidGenerator.Create(),
             brickName
             );

            return await _brickRepository.InsertAsync(brick);
        }

        public async Task<Brick> UpdateAsync(
            Guid id,
            string brickName, [CanBeNull] string concurrencyStamp = null
        )
        {

            var brick = await _brickRepository.GetAsync(id);

            brick.BrickName = brickName;

            brick.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _brickRepository.UpdateAsync(brick);
        }

    }
}