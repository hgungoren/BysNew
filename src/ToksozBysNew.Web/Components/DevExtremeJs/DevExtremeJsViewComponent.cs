using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace ToksozBysNew.Web.Components.DevExtremeJs
{
    public class DevExtremeJsViewComponent : AbpViewComponent
    {
        public IViewComponentResult Invoke() 
        { 
            return View("/Components/DevExtremeJs/Default.cshtml"); 
        }
    }
}
