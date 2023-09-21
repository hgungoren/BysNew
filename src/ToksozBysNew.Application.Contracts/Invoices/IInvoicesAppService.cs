using System;
using System.Threading.Tasks;
using ToksozBysNew.Shared;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;

namespace ToksozBysNew.Invoices
{
    public interface IInvoicesAppService : IApplicationService
    {
        Task<PagedResultDto<InvoiceDto>> GetListAsync(GetInvoicesInput input);

        Task<InvoiceDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<InvoiceDto> CreateAsync(InvoiceCreateDto input);

        Task<Guid> CreateAndGetIdAsync(InvoiceCreateDto input);

        Task<InvoiceDto> UpdateAsync(Guid id, InvoiceUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(InvoiceExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}