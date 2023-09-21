using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using ToksozBysNew.CustomerAddresses;
using ToksozBysNew.Shared;

namespace ToksozBysNew.Web.Pages.CustomerAddresses
{
    public class IndexModel : AbpPageModel
    {
        public string AddressFilter { get; set; }
        [SelectItems(nameof(DoctorLookupList))]
        public Guid? DoctorIdFilter { get; set; }
        public List<SelectListItem> DoctorLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(string.Empty, "")
        };

        [SelectItems(nameof(BrickLookupList))]
        public Guid? BrickIdFilter { get; set; }
        public List<SelectListItem> BrickLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(string.Empty, "")
        };

        [SelectItems(nameof(DistrictLookupList))]
        public Guid? DistrictIdFilter { get; set; }
        public List<SelectListItem> DistrictLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(string.Empty, "")
        };

        [SelectItems(nameof(CountryLookupList))]
        public Guid? CountryIdFilter { get; set; }
        public List<SelectListItem> CountryLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(string.Empty, "")
        };

        [SelectItems(nameof(ProvinceLookupList))]
        public Guid? ProvinceIdFilter { get; set; }
        public List<SelectListItem> ProvinceLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(string.Empty, "")
        };

        private readonly ICustomerAddressesAppService _customerAddressesAppService;

        public IndexModel(ICustomerAddressesAppService customerAddressesAppService)
        {
            _customerAddressesAppService = customerAddressesAppService;
        }

        public async Task OnGetAsync()
        {
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
    }
}