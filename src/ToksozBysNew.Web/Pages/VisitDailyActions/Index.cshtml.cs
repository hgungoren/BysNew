using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using ToksozBysNew.VisitDailyActions;
using ToksozBysNew.Shared;

namespace ToksozBysNew.Web.Pages.VisitDailyActions
{
    public class IndexModel : AbpPageModel
    {
        public DateTime? VisitDailyDateFilterMin { get; set; }

        public DateTime? VisitDailyDateFilterMax { get; set; }
        public decimal? VisitDaily1FilterMin { get; set; }

        public decimal? VisitDaily1FilterMax { get; set; }
        public decimal? VisitDaily2FilterMin { get; set; }

        public decimal? VisitDaily2FilterMax { get; set; }
        public decimal? VisitDaily3FilterMin { get; set; }

        public decimal? VisitDaily3FilterMax { get; set; }
        public decimal? VisitDaily4FilterMin { get; set; }

        public decimal? VisitDaily4FilterMax { get; set; }
        public decimal? VisitDaily5FilterMin { get; set; }

        public decimal? VisitDaily5FilterMax { get; set; }
        public decimal? VisitDaily6FilterMin { get; set; }

        public decimal? VisitDaily6FilterMax { get; set; }
        public decimal? VisitDaily7FilterMin { get; set; }

        public decimal? VisitDaily7FilterMax { get; set; }
        public decimal? VisitDaily8FilterMin { get; set; }

        public decimal? VisitDaily8FilterMax { get; set; }
        public decimal? VisitDaily9FilterMin { get; set; }

        public decimal? VisitDaily9FilterMax { get; set; }
        public decimal? VisitDaily10FilterMin { get; set; }

        public decimal? VisitDaily10FilterMax { get; set; }
        public decimal? VisitDaily11FilterMin { get; set; }

        public decimal? VisitDaily11FilterMax { get; set; }
        public decimal? VisitDaily12FilterMin { get; set; }

        public decimal? VisitDaily12FilterMax { get; set; }
        public decimal? VisitDaily13FilterMin { get; set; }

        public decimal? VisitDaily13FilterMax { get; set; }
        public decimal? VisitDaily14FilterMin { get; set; }

        public decimal? VisitDaily14FilterMax { get; set; }
        public decimal? VisitDaily15FilterMin { get; set; }

        public decimal? VisitDaily15FilterMax { get; set; }
        public DateTime? VisitDailyCloseDateFilterMin { get; set; }

        public DateTime? VisitDailyCloseDateFilterMax { get; set; }
        public string VisitDailyNoteFilter { get; set; }
        [SelectItems(nameof(IdentityUserLookupList))]
        public Guid? IdentityUserIdFilter { get; set; }
        public Guid UserId { get; set; }
        public List<SelectListItem> IdentityUserLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(string.Empty, "")
        };

        private readonly IVisitDailyActionsAppService _visitDailyActionsAppService;

        public IndexModel(IVisitDailyActionsAppService visitDailyActionsAppService)
        {
            _visitDailyActionsAppService = visitDailyActionsAppService;
        }

        public async Task OnGetAsync()
        {
            UserId =Guid.Parse(CurrentUser.Id.ToString());

            IdentityUserLookupList.AddRange((
                    await _visitDailyActionsAppService.GetIdentityUserLookupAsync(new LookupRequestDto
                    {
                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
            );

            await Task.CompletedTask;
        }
    }
}