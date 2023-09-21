using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using ToksozBysNew.Specs;
using ToksozBysNew.Shared;

namespace ToksozBysNew.Web.Pages.Specs
{
    public class IndexModel : AbpPageModel
    {
        public string SpecCodeFilter { get; set; }
        public string SpecNameFilter { get; set; }

        private readonly ISpecsAppService _specsAppService;

        public IndexModel(ISpecsAppService specsAppService)
        {
            _specsAppService = specsAppService;
        }

        public async Task OnGetAsync()
        {

            await Task.CompletedTask;
        }
    }
}