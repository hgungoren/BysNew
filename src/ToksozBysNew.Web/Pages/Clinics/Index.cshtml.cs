using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using ToksozBysNew.Clinics;
using ToksozBysNew.Shared;

namespace ToksozBysNew.Web.Pages.Clinics
{
    public class IndexModel : AbpPageModel
    {
        public string ClinicNameFilter { get; set; }
        [SelectItems(nameof(UnitLookupList))]
        public Guid? UnitIdFilter { get; set; }
        public List<SelectListItem> UnitLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(string.Empty, "")
        };

        [SelectItems(nameof(SpecLookupList))]
        public Guid? SpecIdFilter { get; set; }
        public List<SelectListItem> SpecLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(string.Empty, "")
        };

        private readonly IClinicsAppService _clinicsAppService;

        public IndexModel(IClinicsAppService clinicsAppService)
        {
            _clinicsAppService = clinicsAppService;
        }

        public async Task OnGetAsync()
        {
            UnitLookupList.AddRange((
                    await _clinicsAppService.GetUnitLookupAsync(new LookupRequestDto
                    {
                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
            );

            SpecLookupList.AddRange((
                            await _clinicsAppService.GetSpecLookupAsync(new LookupRequestDto
                            {
                                MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                            })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                    );

            await Task.CompletedTask;
        }
    }
}