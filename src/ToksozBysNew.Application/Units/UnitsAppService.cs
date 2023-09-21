using ToksozBysNew.Shared;
using ToksozBysNew.Bricks;
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
using ToksozBysNew.Units;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using ToksozBysNew.Shared;

namespace ToksozBysNew.Units
{

    [Authorize(ToksozBysNewPermissions.Units.Default)]
    public class UnitsAppService : ApplicationService, IUnitsAppService
    {
        private readonly IDistributedCache<UnitExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly IUnitRepository _unitRepository;
        private readonly UnitManager _unitManager;
        private readonly IRepository<Brick, Guid> _brickRepository;

        public UnitsAppService(IUnitRepository unitRepository, UnitManager unitManager, IDistributedCache<UnitExcelDownloadTokenCacheItem, string> excelDownloadTokenCache, IRepository<Brick, Guid> brickRepository)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _unitRepository = unitRepository;
            _unitManager = unitManager; _brickRepository = brickRepository;
        }

        public virtual async Task<PagedResultDto<UnitWithNavigationPropertiesDto>> GetListAsync(GetUnitsInput input)
        {
            var totalCount = await _unitRepository.GetCountAsync(input.FilterText, input.UnitName, input.BrickId);
            var items = await _unitRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.UnitName, input.BrickId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<UnitWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<UnitWithNavigationProperties>, List<UnitWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<UnitWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<UnitWithNavigationProperties, UnitWithNavigationPropertiesDto>
                (await _unitRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<UnitDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Unit, UnitDto>(await _unitRepository.GetAsync(id));
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetBrickLookupAsync(LookupRequestDto input)
        {
            var query = (await _brickRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.BrickName != null &&
                         x.BrickName.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<Brick>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Brick>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        [Authorize(ToksozBysNewPermissions.Units.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _unitRepository.DeleteAsync(id);
        }

        [Authorize(ToksozBysNewPermissions.Units.Create)]
        public virtual async Task<UnitDto> CreateAsync(UnitCreateDto input)
        {

            var unit = await _unitManager.CreateAsync(
            input.BrickId, input.UnitName
            );

            return ObjectMapper.Map<Unit, UnitDto>(unit);
        }

        [Authorize(ToksozBysNewPermissions.Units.Edit)]
        public virtual async Task<UnitDto> UpdateAsync(Guid id, UnitUpdateDto input)
        {

            var unit = await _unitManager.UpdateAsync(
            id,
            input.BrickId, input.UnitName, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<Unit, UnitDto>(unit);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(UnitExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var units = await _unitRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.UnitName);
            var items = units.Select(item => new
            {
                UnitName = item.Unit.UnitName,

                BrickBrickName = item.Brick?.BrickName,

            });

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(items);
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "Units.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new UnitExcelDownloadTokenCacheItem { Token = token },
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