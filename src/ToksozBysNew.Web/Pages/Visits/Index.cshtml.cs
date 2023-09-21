using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using ToksozBysNew.Visits;
using ToksozBysNew.Shared;
using ToksozBysNew.Invoices;

namespace ToksozBysNew.Web.Pages.Visits
{
    public class IndexModel : AbpPageModel
    {
        public DateTime? VisitDateFilterMin { get; set; }

        public DateTime? VisitDateFilterMax { get; set; }
        public string VisitNotesFilter { get; set; }
        [SelectItems(nameof(DoctorLookupList))]
        public Guid? DoctorIdFilter { get; set; }
        public List<SelectListItem> DoctorLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(string.Empty, "")
        };

        [SelectItems(nameof(UnitLookupList))]
        public Guid? UnitIdFilter { get; set; }
        public List<SelectListItem> UnitLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(string.Empty, "")
        };

        [SelectItems(nameof(ClinicLookupList))]
        public Guid? ClinicIdFilter { get; set; }
        public List<SelectListItem> ClinicLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(string.Empty, "")
        };

        [SelectItems(nameof(BrickLookupList))]
        public Guid? BrickIdFilter { get; set; }
        public List<SelectListItem> BrickLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(string.Empty, "")
        };

        [SelectItems(nameof(IdentityUserLookupList))]
        public Guid? IdentityUserIdFilter { get; set; }
        public List<SelectListItem> IdentityUserLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(string.Empty, "")
        };

        [SelectItems(nameof(SpecLookupList))]
        public Guid? SpecIdFilter { get; set; }
        public List<SelectListItem> SpecLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(string.Empty, "")
        };

        public VisitViewModel Visit { get; set; }

        private readonly IVisitsAppService _visitsAppService;

        public IndexModel(IVisitsAppService visitsAppService)
        {
            _visitsAppService = visitsAppService;
        }

        public async Task OnGetAsync(Guid id,DateTime date)
        {
            if (id!=Guid.Empty&&date!=DateTime.MinValue)
            {

            }
            else
            {

            }
            DoctorLookupList.AddRange((
                    await _visitsAppService.GetDoctorLookupAsync(new LookupRequestDto
                    {
                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
            );

            UnitLookupList.AddRange((
                            await _visitsAppService.GetUnitLookupAsync(new LookupRequestDto
                            {
                                MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                            })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                    );

            ClinicLookupList.AddRange((
                            await _visitsAppService.GetClinicLookupAsync(new LookupRequestDto
                            {
                                MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                            })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                    );

            BrickLookupList.AddRange((
                            await _visitsAppService.GetBrickLookupAsync(new LookupRequestDto
                            {
                                MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                            })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                    );

            IdentityUserLookupList.AddRange((
                            await _visitsAppService.GetIdentityUserLookupAsync(new LookupRequestDto
                            {
                                MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                            })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                    );

            SpecLookupList.AddRange((
                            await _visitsAppService.GetSpecLookupAsync(new LookupRequestDto
                            {
                                MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                            })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                    );

            await Task.CompletedTask;
        }
        public class VisitViewModel : VisitDto { }
    }
}