using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using ToksozBysNew.Doctors;
using ToksozBysNew.Shared;

namespace ToksozBysNew.Web.Pages.Doctors
{
    public class IndexModel : AbpPageModel
    {
        [SelectItems(nameof(IsActiveBoolFilterItems))]
        public string IsActiveFilter { get; set; }

        public List<SelectListItem> IsActiveBoolFilterItems { get; set; } =
            new List<SelectListItem>
            {
                new SelectListItem("", ""),
                new SelectListItem("Yes", "true"),
                new SelectListItem("No", "false"),
            };
        public string NameSurnameFilter { get; set; }
        public string PharmacyNameFilter { get; set; }
        [SelectItems(nameof(PositionLookupList))]
        public Guid? PositionIdFilter { get; set; }
        public List<SelectListItem> PositionLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(string.Empty, "")
        };

        [SelectItems(nameof(SpecLookupList))]
        public Guid? SpecIdFilter { get; set; }
        public List<SelectListItem> SpecLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(string.Empty, "")
        };

        [SelectItems(nameof(CustomerTitleLookupList))]
        public Guid? CustomerTitleIdFilter { get; set; }
        public List<SelectListItem> CustomerTitleLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(string.Empty, "")
        };

        [SelectItems(nameof(UnitLookupList))]
        public Guid? UnitIdFilter { get; set; }
        public List<SelectListItem> UnitLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(string.Empty, "")
        };

        [SelectItems(nameof(CustomerTypeLookupList))]
        public Guid? CustomerTypeIdFilter { get; set; }
        public List<SelectListItem> CustomerTypeLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(string.Empty, "")
        };

        private readonly IDoctorsAppService _doctorsAppService;

        public IndexModel(IDoctorsAppService doctorsAppService)
        {
            _doctorsAppService = doctorsAppService;
        }

        public async Task OnGetAsync()
        {
            PositionLookupList.AddRange((
                    await _doctorsAppService.GetPositionLookupAsync(new LookupRequestDto
                    {
                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
            );

            SpecLookupList.AddRange((
                            await _doctorsAppService.GetSpecLookupAsync(new LookupRequestDto
                            {
                                MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                            })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                    );

            CustomerTitleLookupList.AddRange((
                            await _doctorsAppService.GetCustomerTitleLookupAsync(new LookupRequestDto
                            {
                                MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                            })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                    );

            UnitLookupList.AddRange((
                            await _doctorsAppService.GetUnitLookupAsync(new LookupRequestDto
                            {
                                MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                            })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                    );

            CustomerTypeLookupList.AddRange((
                            await _doctorsAppService.GetCustomerTypeLookupAsync(new LookupRequestDto
                            {
                                MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                            })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                    );

            await Task.CompletedTask;
        }
    }
}