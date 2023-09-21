using ToksozBysNew.Localization;
using Volo.Abp.Application.Services;

namespace ToksozBysNew;

/* Inherit your application services from this class.
 */
public abstract class ToksozBysNewAppService : ApplicationService
{
    protected ToksozBysNewAppService()
    {
        LocalizationResource = typeof(ToksozBysNewResource);
    }
}
