using ToksozBysNew.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.BlobStoring;
using Volo.Abp.BlobStoring.Database;
using Volo.Abp.Modularity;
using Volo.FileManagement;

namespace ToksozBysNew.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(ToksozBysNewEntityFrameworkCoreModule),
    typeof(ToksozBysNewApplicationContractsModule)
)]
public class ToksozBysNewDbMigratorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpBackgroundJobOptions>(options =>
        {
            options.IsJobExecutionEnabled = false;
        });
        Configure<AbpBlobStoringOptions>(options =>
        {
            options.Containers.Configure<FileManagementContainer>(c =>
            {
                c.UseDatabase(); // You can use FileSystem or Azure providers also.
            });
        });
    }
}
