using ToksozBysNew.Shared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using ToksozBysNew.CustomerAddresses;

namespace ToksozBysNew.Web.Pages.CustomerAddresses
{
    public class EditModalModel : ToksozBysNewPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public CustomerAddressUpdateViewModel CustomerAddress { get; set; }

        public List<SelectListItem> DoctorLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(" — ", "")
        };
        public List<SelectListItem> BrickLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(" — ", "")
        };
        public List<SelectListItem> DistrictLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(" — ", "")
        };
        public List<SelectListItem> CountryLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(" — ", "")
        };
        public List<SelectListItem> ProvinceLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(" — ", "")
        };

        private readonly ICustomerAddressesAppService _customerAddressesAppService;

        public EditModalModel(ICustomerAddressesAppService customerAddressesAppService)
        {
            _customerAddressesAppService = customerAddressesAppService;
        }

        public async Task OnGetAsync()
        {
            var customerAddressWithNavigationPropertiesDto = await _customerAddressesAppService.GetWithNavigationPropertiesAsync(Id);
            CustomerAddress = ObjectMapper.Map<CustomerAddressDto, CustomerAddressUpdateViewModel>(customerAddressWithNavigationPropertiesDto.CustomerAddress);

            DoctorLookupList.AddRange((
                                    await _customerAddressesAppService.GetDoctorLookupAsync(new LookupRequestDto
                                    {
                                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                        );
            BrickLookupList.AddRange((
                                    await _customerAddressesAppService.GetBrickLookupAsync(new LookupRequestDto
                                    {
                                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                        );
            DistrictLookupList.AddRange((
                                    await _customerAddressesAppService.GetDistrictLookupAsync(new LookupRequestDto
                                    {
                                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                        );
            CountryLookupList.AddRange((
                                    await _customerAddressesAppService.GetCountryLookupAsync(new LookupRequestDto
                                    {
                                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                        );
            ProvinceLookupList.AddRange((
                                    await _customerAddressesAppService.GetProvinceLookupAsync(new LookupRequestDto
                                    {
                                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                        );

        }

        public async Task<NoContentResult> OnPostAsync()
        {

            await _customerAddressesAppService.UpdateAsync(Id, ObjectMapper.Map<CustomerAddressUpdateViewModel, CustomerAddressUpdateDto>(CustomerAddress));
            return NoContent();
        }
    }

    public class CustomerAddressUpdateViewModel : CustomerAddressUpdateDto
    {
    }
}