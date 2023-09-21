using ToksozBysNew.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace ToksozBysNew.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class ToksozBysNewController : AbpControllerBase
{
    protected ToksozBysNewController()
    {
        LocalizationResource = typeof(ToksozBysNewResource);
    }
}
