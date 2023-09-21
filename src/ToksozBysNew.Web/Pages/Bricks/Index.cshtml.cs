using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using ToksozBysNew.Bricks;
using ToksozBysNew.Shared;

namespace ToksozBysNew.Web.Pages.Bricks
{
    public class IndexModel : AbpPageModel
    {
        public string BrickNameFilter { get; set; }

        private readonly IBricksAppService _bricksAppService;

        public IndexModel(IBricksAppService bricksAppService)
        {
            _bricksAppService = bricksAppService;
        }

        public async Task OnGetAsync()
        {

            await Task.CompletedTask;
        }
    }
}