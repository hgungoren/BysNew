using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using ToksozBysNew.AccountGroups;
using ToksozBysNew.Shared;

namespace ToksozBysNew.Web.Pages.AccountGroups
{
    public class IndexModel : AbpPageModel
    {
        public string AccountGroupNameFilter { get; set; }
        [SelectItems(nameof(IsUnitEnterableBoolFilterItems))]
        public string IsUnitEnterableFilter { get; set; }

        public List<SelectListItem> IsUnitEnterableBoolFilterItems { get; set; } =
            new List<SelectListItem>
            {
                new SelectListItem("", ""),
                new SelectListItem("Yes", "true"),
                new SelectListItem("No", "false"),
            };

        private readonly IAccountGroupsAppService _accountGroupsAppService;

        public IndexModel(IAccountGroupsAppService accountGroupsAppService)
        {
            _accountGroupsAppService = accountGroupsAppService;
        }

        public async Task OnGetAsync()
        {

            await Task.CompletedTask;
        }
    }
}