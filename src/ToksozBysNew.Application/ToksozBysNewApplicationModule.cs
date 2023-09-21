using Volo.Abp.Account;
using Volo.Abp.AuditLogging;
using Volo.Abp.AutoMapper;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Gdpr;
using Volo.Abp.Identity;
using Volo.Abp.LanguageManagement;
using Volo.Abp.Modularity;
using Volo.Abp.OpenIddict;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.TextTemplateManagement;
using Volo.Saas.Host;
using Volo.Abp.LeptonTheme.Management;
using Volo.FileManagement;
using Volo.Abp.BlobStoring;
using Volo.Abp.BlobStoring.Database;

namespace ToksozBysNew;

[DependsOn(
    typeof(ToksozBysNewDomainModule),
    typeof(ToksozBysNewApplicationContractsModule),
    typeof(AbpIdentityApplicationModule),
    typeof(AbpPermissionManagementApplicationModule),
    typeof(AbpFeatureManagementApplicationModule),
    typeof(AbpSettingManagementApplicationModule),
    typeof(SaasHostApplicationModule),
    typeof(AbpAuditLoggingApplicationModule),
    typeof(AbpOpenIddictProApplicationModule),
    typeof(AbpAccountPublicApplicationModule),
    typeof(AbpAccountAdminApplicationModule),
    typeof(LanguageManagementApplicationModule),
    typeof(AbpGdprApplicationModule),
    typeof(TextTemplateManagementApplicationModule),
	typeof(LeptonThemeManagementApplicationModule),
    typeof(FileManagementApplicationModule),
    typeof(AbpBlobStoringModule)
    )]  
    public class ToksozBysNewApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<ToksozBysNewApplicationModule>();
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
