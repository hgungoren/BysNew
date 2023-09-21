using ToksozBysNew.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using ToksozBysNew.Shared;

namespace ToksozBysNew.CustomerAddresses
{
    public interface ICustomerAddressesAppService : IApplicationService
    {
        Task<PagedResultDto<CustomerAddressWithNavigationPropertiesDto>> GetListAsync(GetCustomerAddressesInput input);
        Task<PagedResultDto<CustomerAddressDto>> GetListWithDoctorIdAsync(Guid id);

        Task<CustomerAddressWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<CustomerAddressDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetDoctorLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid>>> GetBrickLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid>>> GetDistrictLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid>>> GetCountryLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid>>> GetProvinceLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<CustomerAddressDto> CreateAsync(CustomerAddressCreateDto input);

        Task<CustomerAddressDto> UpdateAsync(Guid id, CustomerAddressUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(CustomerAddressExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}