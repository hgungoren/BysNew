using ToksozBysNew.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToksozBysNew.CustomerAddresses;

namespace ToksozBysNew.Web.Pages.CustomerAddresses
{
    public class CreateModalModel : ToksozBysNewPageModel
    {
        [BindProperty]
        public CustomerAddressCreateViewModel CustomerAddress { get; set; }

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

        public CreateModalModel(ICustomerAddressesAppService customerAddressesAppService)
        {
            _customerAddressesAppService = customerAddressesAppService;
        }

        public async Task OnGetAsync()
        {
            CustomerAddress = new CustomerAddressCreateViewModel();
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

            await Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostAsync()
        {

            await _customerAddressesAppService.CreateAsync(ObjectMapper.Map<CustomerAddressCreateViewModel, CustomerAddressCreateDto>(CustomerAddress));
            return NoContent();
        }
    }

    public class CustomerAddressCreateViewModel : CustomerAddressCreateDto
    {
    }
}