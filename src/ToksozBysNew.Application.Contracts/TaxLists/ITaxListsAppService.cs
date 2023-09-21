using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace ToksozBysNew.TaxLists
{
    public interface ITaxListsAppService : IApplicationService
    {
        Task<PagedResultDto<TaxListDto>> GetListAsync(GetTaxListsInput input);

        Task<TaxListDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<TaxListDto> CreateAsync(TaxListCreateDto input);

        Task<TaxListDto> UpdateAsync(Guid id, TaxListUpdateDto input);
    }
}