using Volo.Abp.Modularity;

namespace ToksozBysNew;

[DependsOn(
    typeof(ToksozBysNewApplicationModule),
    typeof(ToksozBysNewDomainTestModule)
    )]
public class ToksozBysNewApplicationTestModule : AbpModule
{

}
