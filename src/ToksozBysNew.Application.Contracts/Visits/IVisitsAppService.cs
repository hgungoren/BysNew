using ToksozBysNew.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using ToksozBysNew.Shared;

namespace ToksozBysNew.Visits
{
    public interface IVisitsAppService : IApplicationService
    {
        Task<PagedResultDto<VisitWithNavigationPropertiesDto>> GetListAsync(GetVisitsInput input);
        Task<PagedResultDto<VisitDto>> GetVisitListAsync(Guid id, DateTime date);
        Task<PagedResultDto<VisitDto>> GetVisitListWithCustomerNameAsync(string name);

        Task<VisitWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<VisitDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetDoctorLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid>>> GetUnitLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid>>> GetClinicLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid>>> GetBrickLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid>>> GetIdentityUserLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid>>> GetSpecLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<VisitDto> CreateAsync(VisitCreateDto input);

        Task<VisitDto> UpdateAsync(Guid id, VisitUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(VisitExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}