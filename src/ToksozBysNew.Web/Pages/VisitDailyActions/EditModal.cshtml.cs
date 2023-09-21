using ToksozBysNew.Shared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using ToksozBysNew.VisitDailyActions;

namespace ToksozBysNew.Web.Pages.VisitDailyActions
{
    public class EditModalModel : ToksozBysNewPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public VisitDailyActionUpdateViewModel VisitDailyAction { get; set; }

        public List<SelectListItem> IdentityUserLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(" — ", "")
        };

        private readonly IVisitDailyActionsAppService _visitDailyActionsAppService;

        public EditModalModel(IVisitDailyActionsAppService visitDailyActionsAppService)
        {
            _visitDailyActionsAppService = visitDailyActionsAppService;
        }

        public async Task OnGetAsync()
        {
            var visitDailyActionWithNavigationPropertiesDto = await _visitDailyActionsAppService.GetWithNavigationPropertiesAsync(Id);
            VisitDailyAction = ObjectMapper.Map<VisitDailyActionDto, VisitDailyActionUpdateViewModel>(visitDailyActionWithNavigationPropertiesDto.VisitDailyAction);

            IdentityUserLookupList.AddRange((
                                    await _visitDailyActionsAppService.GetIdentityUserLookupAsync(new LookupRequestDto
                                    {
                                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                        );

        }

        public async Task<NoContentResult> OnPostAsync()
        {

            await _visitDailyActionsAppService.UpdateAsync(Id, ObjectMapper.Map<VisitDailyActionUpdateViewModel, VisitDailyActionUpdateDto>(VisitDailyAction));
            return NoContent();
        }
    }

    public class VisitDailyActionUpdateViewModel : VisitDailyActionUpdateDto
    {
    }
}