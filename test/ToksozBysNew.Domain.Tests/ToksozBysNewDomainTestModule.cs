using ToksozBysNew.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace ToksozBysNew;

[DependsOn(
    typeof(ToksozBysNewEntityFrameworkCoreTestModule)
    )]
public class ToksozBysNewDomainTestModule : AbpModule
{

}
