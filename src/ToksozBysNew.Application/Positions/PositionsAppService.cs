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
using ToksozBysNew.Positions;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using ToksozBysNew.Shared;

namespace ToksozBysNew.Positions
{

    [Authorize(ToksozBysNewPermissions.Positions.Default)]
    public class PositionsAppService : ApplicationService, IPositionsAppService
    {
        private readonly IDistributedCache<PositionExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly IPositionRepository _positionRepository;
        private readonly PositionManager _positionManager;

        public PositionsAppService(IPositionRepository positionRepository, PositionManager positionManager, IDistributedCache<PositionExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _positionRepository = positionRepository;
            _positionManager = positionManager;
        }

        public virtual async Task<PagedResultDto<PositionDto>> GetListAsync(GetPositionsInput input)
        {
            var totalCount = await _positionRepository.GetCountAsync(input.FilterText, input.PositionCode, input.PositionName);
            var items = await _positionRepository.GetListAsync(input.FilterText, input.PositionCode, input.PositionName, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<PositionDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Position>, List<PositionDto>>(items)
            };
        }

        public virtual async Task<PositionDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Position, PositionDto>(await _positionRepository.GetAsync(id));
        }

        [Authorize(ToksozBysNewPermissions.Positions.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _positionRepository.DeleteAsync(id);
        }

        [Authorize(ToksozBysNewPermissions.Positions.Create)]
        public virtual async Task<PositionDto> CreateAsync(PositionCreateDto input)
        {

            var position = await _positionManager.CreateAsync(
            input.PositionCode, input.PositionName
            );

            return ObjectMapper.Map<Position, PositionDto>(position);
        }

        [Authorize(ToksozBysNewPermissions.Positions.Edit)]
        public virtual async Task<PositionDto> UpdateAsync(Guid id, PositionUpdateDto input)
        {

            var position = await _positionManager.UpdateAsync(
            id,
            input.PositionCode, input.PositionName, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<Position, PositionDto>(position);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(PositionExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _positionRepository.GetListAsync(input.FilterText, input.PositionCode, input.PositionName);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<Position>, List<PositionExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "Positions.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new PositionExcelDownloadTokenCacheItem { Token = token },
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