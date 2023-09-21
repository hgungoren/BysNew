using ToksozBysNew.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace ToksozBysNew.Web.Pages;

public abstract class ToksozBysNewPageModel : AbpPageModel
{
    protected ToksozBysNewPageModel()
    {
        LocalizationResourceType = typeof(ToksozBysNewResource);
    }
}
