using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace ToksozBysNew.Web;

[Dependency(ReplaceServices = true)]
public class ToksozBysNewBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "ToksozBysNew";
}
