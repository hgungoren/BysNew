using ToksozBysNew.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace ToksozBysNew.InvoiceDetails
{
    public interface IInvoiceDetailsAppService : IApplicationService
    {
        Task<PagedResultDto<InvoiceDetailWithNavigationPropertiesDto>> GetListAsync(GetInvoiceDetailsInput input);
        Task<PagedResultDto<InvoiceDetailDto>> GetListByInvoiceId(Guid id);

        Task<InvoiceDetailWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<InvoiceDetailDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetInvoiceLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid>>> GetTaxListLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<InvoiceDetailDto> CreateAsync(InvoiceDetailCreateDto input);

        Task<InvoiceDetailDto> UpdateAsync(Guid id, InvoiceDetailUpdateDto input);
        Task<InvoiceDetailDto> UpdateDetailAsync(Guid id, string inputDetail);
    }
}