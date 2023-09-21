using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using ToksozBysNew.Shared;

namespace ToksozBysNew.CompanyCalendars
{
    public interface ICompanyCalendarsAppService : IApplicationService
    {
        Task<PagedResultDto<CompanyCalendarDto>> GetListAsync(GetCompanyCalendarsInput input);

        Task<CompanyCalendarDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<CompanyCalendarDto> CreateAsync(CompanyCalendarCreateDto input);

        Task<CompanyCalendarDto> UpdateAsync(Guid id, CompanyCalendarUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(CompanyCalendarExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}