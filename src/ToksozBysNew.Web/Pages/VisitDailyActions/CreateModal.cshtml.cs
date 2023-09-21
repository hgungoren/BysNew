using ToksozBysNew.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToksozBysNew.VisitDailyActions;

namespace ToksozBysNew.Web.Pages.VisitDailyActions
{
    public class CreateModalModel : ToksozBysNewPageModel
    {
        [BindProperty]
        public VisitDailyActionCreateViewModel VisitDailyAction { get; set; }

        public List<SelectListItem> IdentityUserLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(" — ", "")
        };

        private readonly IVisitDailyActionsAppService _visitDailyActionsAppService;

        public CreateModalModel(IVisitDailyActionsAppService visitDailyActionsAppService)
        {
            _visitDailyActionsAppService = visitDailyActionsAppService;
        }

        public async Task OnGetAsync()
        {
            VisitDailyAction = new VisitDailyActionCreateViewModel();
            IdentityUserLookupList.AddRange((
                                    await _visitDailyActionsAppService.GetIdentityUserLookupAsync(new LookupRequestDto
                                    {
                                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                        );

            await Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostAsync()
        {

            await _visitDailyActionsAppService.CreateAsync(ObjectMapper.Map<VisitDailyActionCreateViewModel, VisitDailyActionCreateDto>(VisitDailyAction));
            return NoContent();
        }
    }

    public class VisitDailyActionCreateViewModel : VisitDailyActionCreateDto
    {
    }
}