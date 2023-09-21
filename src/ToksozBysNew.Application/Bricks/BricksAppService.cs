using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using ToksozBysNew.Permissions;
using ToksozBysNew.Bricks;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using ToksozBysNew.Shared;

namespace ToksozBysNew.Bricks
{

    [Authorize(ToksozBysNewPermissions.Bricks.Default)]
    public class BricksAppService : ApplicationService, IBricksAppService
    {
        private readonly IDistributedCache<BrickExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly IBrickRepository _brickRepository;
        private readonly BrickManager _brickManager;

        public BricksAppService(IBrickRepository brickRepository, BrickManager brickManager, IDistributedCache<BrickExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _brickRepository = brickRepository;
            _brickManager = brickManager;
        }

        public virtual async Task<PagedResultDto<BrickDto>> GetListAsync(GetBricksInput input)
        {
            var totalCount = await _brickRepository.GetCountAsync(input.FilterText, input.BrickName);
            var items = await _brickRepository.GetListAsync(input.FilterText, input.BrickName, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<BrickDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Brick>, List<BrickDto>>(items)
            };
        }

        public virtual async Task<BrickDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Brick, BrickDto>(await _brickRepository.GetAsync(id));
        }

        [Authorize(ToksozBysNewPermissions.Bricks.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _brickRepository.DeleteAsync(id);
        }

        [Authorize(ToksozBysNewPermissions.Bricks.Create)]
        public virtual async Task<BrickDto> CreateAsync(BrickCreateDto input)
        {

            var brick = await _brickManager.CreateAsync(
            input.BrickName
            );

            return ObjectMapper.Map<Brick, BrickDto>(brick);
        }

        [Authorize(ToksozBysNewPermissions.Bricks.Edit)]
        public virtual async Task<BrickDto> UpdateAsync(Guid id, BrickUpdateDto input)
        {

            var brick = await _brickManager.UpdateAsync(
            id,
            input.BrickName, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<Brick, BrickDto>(brick);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(BrickExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _brickRepository.GetListAsync(input.FilterText, input.BrickName);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<Brick>, List<BrickExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "Bricks.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new BrickExcelDownloadTokenCacheItem { Token = token },
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
                });

            return new DownloadTokenResultDto
            {
                Token = token
            };
        }
    }
}