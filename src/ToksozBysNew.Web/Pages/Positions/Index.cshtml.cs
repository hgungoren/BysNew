using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using ToksozBysNew.Positions;
using ToksozBysNew.Shared;

namespace ToksozBysNew.Web.Pages.Positions
{
    public class IndexModel : AbpPageModel
    {
        public string PositionCodeFilter { get; set; }
        public string PositionNameFilter { get; set; }

        private readonly IPositionsAppService _positionsAppService;

        public IndexModel(IPositionsAppService positionsAppService)
        {
            _positionsAppService = positionsAppService;
        }

        public async Task OnGetAsync()
        {

            await Task.CompletedTask;
        }
    }
}