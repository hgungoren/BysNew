using DevExpress.AspNetCore;
using DevExpress.DashboardAspNetCore;
using DevExpress.DashboardCommon;
using DevExpress.DashboardWeb;
using DevExpress.DataAccess.Sql;
using DevExpress.XtraReports.Web.Extensions;
using Elsa;
using Elsa.Persistence.EntityFramework.Core.Extensions;
using Elsa.Persistence.EntityFramework.SqlServer;
using Elsa.Services;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.MicrosoftAccount;
using Microsoft.AspNetCore.Authentication.Twitter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using OpenIddict.Validation.AspNetCore;
using System;
using System.IO;
using System.Linq;
using ToksozBysNew.EntityFrameworkCore;
using ToksozBysNew.Localization;
using ToksozBysNew.MultiTenancy;
using ToksozBysNew.Permissions;
using ToksozBysNew.Web.Bundling;
using ToksozBysNew.Web.Components.DevExtremeJs;
using ToksozBysNew.Web.Hangfire;
using ToksozBysNew.Web.HealthChecks;
using ToksozBysNew.Web.Menus;
using ToksozBysNew.Web.Pages.Controllers;
using ToksozBysNew.Web.SignalR;
using ToksozBysNew.Web.Workflows;
using Volo.Abp;
using Volo.Abp.Account;
using Volo.Abp.Account.Public.Web;
using Volo.Abp.Account.Public.Web.ExternalProviders;
using Volo.Abp.Account.Web;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.AntiForgery;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Lepton;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Lepton.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared.Toolbars;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.AuditLogging.Web;
using Volo.Abp.Autofac;
using Volo.Abp.AutoMapper;
using Volo.Abp.BackgroundWorkers;
using Volo.Abp.BackgroundWorkers.Hangfire;
using Volo.Abp.BlobStoring;
using Volo.Abp.BlobStoring.Database;
using Volo.Abp.Gdpr.Web;
using Volo.Abp.Gdpr.Web.Extensions;
using Volo.Abp.Identity;
using Volo.Abp.Identity.Web;
using Volo.Abp.LanguageManagement;
using Volo.Abp.LeptonTheme.Management;
using Volo.Abp.Modularity;
using Volo.Abp.OpenIddict.Pro.Web;
using Volo.Abp.Swashbuckle;
using Volo.Abp.TextTemplateManagement.Web;
using Volo.Abp.Ui.LayoutHooks;
using Volo.Abp.UI.Navigation;
using Volo.Abp.UI.Navigation.Urls;
using Volo.Abp.VirtualFileSystem;
using Volo.FileManagement;
using Volo.FileManagement.Web;
using Volo.Saas.Host;

namespace ToksozBysNew.Web;

[DependsOn(
    typeof(ToksozBysNewHttpApiModule),
    typeof(ToksozBysNewApplicationModule),
    typeof(ToksozBysNewEntityFrameworkCoreModule),
    typeof(AbpAutofacModule),
    typeof(AbpIdentityWebModule),
    typeof(AbpAccountPublicWebOpenIddictModule),
    typeof(AbpAuditLoggingWebModule),
    typeof(SaasHostWebModule),
    typeof(AbpOpenIddictProWebModule),
    typeof(LanguageManagementWebModule),
    typeof(AbpAspNetCoreMvcUiLeptonThemeModule),
    typeof(TextTemplateManagementWebModule),
    typeof(AbpGdprWebModule),
    typeof(AbpSwashbuckleModule),
    typeof(AbpAspNetCoreSerilogModule),
    typeof(LeptonThemeManagementWebModule),
    typeof(AbpAccountAdminApplicationModule),
    typeof(FileManagementWebModule),
    typeof(AbpBackgroundWorkersHangfireModule)
)]
public class ToksozBysNewWebModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
        {
            options.AddAssemblyResource(
                typeof(ToksozBysNewResource),
                typeof(ToksozBysNewDomainModule).Assembly,
                typeof(ToksozBysNewDomainSharedModule).Assembly,
                typeof(ToksozBysNewApplicationModule).Assembly,
                typeof(ToksozBysNewApplicationContractsModule).Assembly,
                typeof(ToksozBysNewWebModule).Assembly
            );
        });

        PreConfigure<OpenIddictBuilder>(builder =>
        {
            builder.AddValidation(options =>
            {
                options.AddAudiences("ToksozBysNew");
                options.UseLocalServer();
                options.UseAspNetCore();
            });
        });
        context.Services.AddSignalR();
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var hostingEnvironment = context.Services.GetHostingEnvironment();
        var configuration = context.Services.GetConfiguration();
        IFileProvider? fileProvider = hostingEnvironment.ContentRootFileProvider;

        ConfigureBundles();
        ConfigureUrls(configuration);
        ConfigurePages(configuration);
        ConfigureAuthentication(context);
        ConfigureImpersonation(context, configuration);
        ConfigureAutoMapper();
        ConfigureVirtualFileSystem(hostingEnvironment);
        ConfigureNavigationServices();
        ConfigureAutoApiControllers();
        ConfigureSwaggerServices(context.Services);
        ConfigureExternalProviders(context);
        ConfigureHealthChecks(context);
        ConfigureCookieConsent(context);
        ConfigureDevExpress(context);
        Configure<AbpLayoutHookOptions>(options =>
        {
            options.Add(LayoutHooks.Head.Last,//The hook name
            typeof(DevExtremeJsViewComponent));//The component to add) 
        });
        Configure<AbpBlobStoringOptions>(options =>
        {
            options.Containers.Configure<FileManagementContainer>(c =>
            {
                c.UseDatabase(); // You can use FileSystem or Azure providers also.
            });
        });
        ConfigureElsa(context, configuration);
        ConfigureHangfire(context, configuration);
        Configure<AbpBundlingOptions>(options =>
        {
            options
                .ScriptBundles
                .Get(LeptonThemeBundles.Scripts.Global)
                .AddFiles("/libs/signalr/signalr.js")
                .AddFiles("/Pages/notification-hub.js");
        });

    }

    //public static DataSourceInMemoryStorage CreateDataSourceStorage()
    //{
    //    DataSourceInMemoryStorage dataSourceStorage = new DataSourceInMemoryStorage();

    //    DashboardSqlDataSource sqlDataSource = new DashboardSqlDataSource("SQL Data Source", "Default");
    //    SelectQuery query = SelectQueryFluentBuilder
    //        .AddTable("AppInvoices")
    //        .SelectAllColumnsFromTable()
    //        .Build("App Invoices");
    //    sqlDataSource.Queries.Add(query);
    //    dataSourceStorage.RegisterDataSource("sqlDataSource", sqlDataSource.SaveToXml());

    //    return dataSourceStorage;
    //}

    private void ConfigureDevExpress(ServiceConfigurationContext context)
    {
        var builder = WebApplication.CreateBuilder();

        IFileProvider? fileProvider = builder.Environment.ContentRootFileProvider;
        IConfiguration? configuration = builder.Configuration;

        builder.Services.AddScoped<DashboardConfigurator>((IServiceProvider serviceProvider) =>
        {
            DashboardConfigurator configurator = new DashboardConfigurator();
            configurator.SetDashboardStorage(new DashboardFileStorage(fileProvider.GetFileInfo("Data/Dashboards").PhysicalPath));
            //configurator.SetDataSourceStorage(CreateDataSourceStorage());
            configurator.SetConnectionStringsProvider(new DashboardConnectionStringsProvider(configuration));
            return configurator;
        });

        context.Services.AddDevExpressControls();
        context.Services.AddTransient<CustomReportDesignerController>();
        context.Services.AddTransient<CustomQueryBuilderController>();
        context.Services.AddTransient<CustomWebDocumentViewerController>();
        // Create this file if you want to use report storage which is used for menus like "Save as" etc
        context.Services.AddScoped<ReportStorageWebExtension, CustomReportStorageWebExtension>();
        context.Services.AddMvc();
        context.Services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
            {
                builder.AllowAnyOrigin();
                builder.AllowAnyMethod();
                builder.WithHeaders("Content-Type");
            });
        });

    }

    private void ConfigureCookieConsent(ServiceConfigurationContext context)
    {
        context.Services.AddAbpCookieConsent(options =>
        {
            options.IsEnabled = true;
            options.CookiePolicyUrl = "/CookiePolicy";
            options.PrivacyPolicyUrl = "/PrivacyPolicy";
        });
    }

    private void ConfigureHealthChecks(ServiceConfigurationContext context)
    {
        context.Services.AddToksozBysNewHealthChecks();
    }

    private void ConfigureBundles()
    {
        Configure<AbpBundlingOptions>(options =>
        {
            options.StyleBundles.Configure(
                LeptonThemeBundles.Styles.Global,
                bundle =>
                {
                    bundle.AddFiles("/global-styles.css");
                }
            );
        });
        Configure<AbpBundlingOptions>(options =>
        {
            options
                .StyleBundles
                .Get(StandardBundles.Styles.Global)
                .AddContributors(typeof(DevExtremeStyleContributor));
        });
    }

    private void ConfigurePages(IConfiguration configuration)
    {
        Configure<RazorPagesOptions>(options =>
        {
            options.Conventions.AuthorizePage("/HostDashboard", ToksozBysNewPermissions.Dashboard.Host);
            options.Conventions.AuthorizePage("/TenantDashboard", ToksozBysNewPermissions.Dashboard.Tenant);
            options.Conventions.AuthorizePage("/Companies/Index", ToksozBysNewPermissions.Companies.Default);
            options.Conventions.AuthorizePage("/AccountGroups/Index", ToksozBysNewPermissions.AccountGroups.Default);
            options.Conventions.AuthorizePage("/Departments/Index", ToksozBysNewPermissions.Departments.Default);
            options.Conventions.AuthorizePage("/Accounts/Index", ToksozBysNewPermissions.Accounts.Default);
            options.Conventions.AuthorizePage("/Products/Index", ToksozBysNewPermissions.Products.Default);
            options.Conventions.AuthorizePage("/Budgets/Index", ToksozBysNewPermissions.Budgets.Default);
            options.Conventions.AuthorizePage("/BudgetDistributions/Index", ToksozBysNewPermissions.BudgetDistributions.Default);
            options.Conventions.AuthorizePage("/ExpenseMonthlies/Index", ToksozBysNewPermissions.ExpenseMonthlies.Default);
            options.Conventions.AuthorizePage("/MasterDetails/Index", ToksozBysNewPermissions.MasterDetails.Default);
            options.Conventions.AuthorizePage("/Masters/Index", ToksozBysNewPermissions.Masters.Default);
            options.Conventions.AuthorizePage("/Denemes/Index", ToksozBysNewPermissions.Denemes.Default);
            options.Conventions.AuthorizePage("/DenemeDetails/Index", ToksozBysNewPermissions.DenemeDetails.Default);
            options.Conventions.AuthorizePage("/Invoices/Index", ToksozBysNewPermissions.Invoices.Default);
            options.Conventions.AuthorizePage("/InvoiceLists/Index", ToksozBysNewPermissions.Invoices.Default);
            options.Conventions.AuthorizePage("/InvoiceDetails/Index", ToksozBysNewPermissions.InvoiceDetails.Default);
            options.Conventions.AuthorizePage("/Elsa/_Host");
            options.Conventions.AuthorizePage("/Doctors/Index", ToksozBysNewPermissions.Doctors.Default);
            options.Conventions.AuthorizePage("/Bricks/Index", ToksozBysNewPermissions.Bricks.Default);
            options.Conventions.AuthorizePage("/Positions/Index", ToksozBysNewPermissions.Positions.Default);
            options.Conventions.AuthorizePage("/Specs/Index", ToksozBysNewPermissions.Specs.Default);
            options.Conventions.AuthorizePage("/Units/Index", ToksozBysNewPermissions.Units.Default);
            options.Conventions.AuthorizePage("/CustomerTitles/Index", ToksozBysNewPermissions.CustomerTitles.Default);
            options.Conventions.AuthorizePage("/CustomerAddresses/Index", ToksozBysNewPermissions.CustomerAddresses.Default);
            options.Conventions.AuthorizePage("/Countries/Index", ToksozBysNewPermissions.Countries.Default);
            options.Conventions.AuthorizePage("/Provinces/Index", ToksozBysNewPermissions.Provinces.Default);
            options.Conventions.AuthorizePage("/Districts/Index", ToksozBysNewPermissions.Districts.Default);
            options.Conventions.AuthorizePage("/Clinics/Index", ToksozBysNewPermissions.Clinics.Default);
            options.Conventions.AuthorizePage("/Visits/Index", ToksozBysNewPermissions.Visits.Default);
            options.Conventions.AuthorizePage("/VisitDailyActions/Index", ToksozBysNewPermissions.VisitDailyActions.Default);
            options.Conventions.AuthorizePage("/CompanyCalendars/Index", ToksozBysNewPermissions.CompanyCalendars.Default);
        });
    }

    private void ConfigureUrls(IConfiguration configuration)
    {
        Configure<AppUrlOptions>(options =>
        {
            options.Applications["MVC"].RootUrl = configuration["App:SelfUrl"];
        });
    }

    private void ConfigureAuthentication(ServiceConfigurationContext context)
    {
        context.Services.ForwardIdentityAuthenticationForBearer(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme);
    }

    private void ConfigureImpersonation(ServiceConfigurationContext context, IConfiguration configuration)
    {
        context.Services.Configure<AbpSaasHostWebOptions>(options =>
        {
            options.EnableTenantImpersonation = true;
        });
        context.Services.Configure<AbpIdentityWebOptions>(options =>
        {
            options.EnableUserImpersonation = true;
        });
        context.Services.Configure<AbpAccountOptions>(options =>
        {
            options.TenantAdminUserName = "admin";
            options.ImpersonationTenantPermission = SaasHostPermissions.Tenants.Impersonation;
            options.ImpersonationUserPermission = IdentityPermissions.Users.Impersonation;
        });
    }

    private void ConfigureAutoMapper()
    {
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<ToksozBysNewWebModule>();
        });
    }

    private void ConfigureVirtualFileSystem(IWebHostEnvironment hostingEnvironment)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<ToksozBysNewWebModule>();

            if (hostingEnvironment.IsDevelopment())
            {
                options.FileSets.ReplaceEmbeddedByPhysical<ToksozBysNewDomainSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}ToksozBysNew.Domain.Shared", Path.DirectorySeparatorChar)));
                options.FileSets.ReplaceEmbeddedByPhysical<ToksozBysNewDomainModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}ToksozBysNew.Domain", Path.DirectorySeparatorChar)));
                options.FileSets.ReplaceEmbeddedByPhysical<ToksozBysNewApplicationContractsModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}ToksozBysNew.Application.Contracts", Path.DirectorySeparatorChar)));
                options.FileSets.ReplaceEmbeddedByPhysical<ToksozBysNewApplicationModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}ToksozBysNew.Application", Path.DirectorySeparatorChar)));
                options.FileSets.ReplaceEmbeddedByPhysical<ToksozBysNewHttpApiModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}ToksozBysNew.HttpApi", Path.DirectorySeparatorChar)));
                options.FileSets.ReplaceEmbeddedByPhysical<ToksozBysNewWebModule>(hostingEnvironment.ContentRootPath);
            }
        });
    }

    private void ConfigureNavigationServices()
    {
        Configure<AbpNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new ToksozBysNewMenuContributor());
        });

        Configure<AbpToolbarOptions>(options =>
        {
            options.Contributors.Add(new ToksozBysNewToolbarContributor());
        });
    }

    private void ConfigureAutoApiControllers()
    {
        Configure<AbpAspNetCoreMvcOptions>(options =>
        {
            options.ConventionalControllers.Create(typeof(ToksozBysNewApplicationModule).Assembly);
        });
    }

    private void ConfigureSwaggerServices(IServiceCollection services)
    {
        services.AddAbpSwaggerGen(
            options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "ToksozBysNew API", Version = "v1" });
                options.DocInclusionPredicate((docName, description) => true);
                options.CustomSchemaIds(type => type.FullName);
                options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                options.IgnoreObsoleteActions();
                options.IgnoreObsoleteProperties();
            }
        );
    }

    private void ConfigureElsa(ServiceConfigurationContext context, IConfiguration configuration)
    {
        var elsaSection = configuration.GetSection("Elsa");

        context.Services.AddElsa(elsa =>
        {
            elsa
                .UseEntityFrameworkPersistence(ef =>
                    DbContextOptionsBuilderExtensions.UseSqlServer(ef,
                        configuration.GetConnectionString("Default")))
                .AddConsoleActivities()
                .AddHttpActivities(elsaSection.GetSection("Server").Bind)
                .AddEmailActivities(elsaSection.GetSection("Smtp").Bind)
                .AddQuartzTemporalActivities()
                .AddJavaScriptActivities()
                //.AddWorkflow<DocumentApprovalWorkflow>()
                .AddWorkflowsFrom<Startup>();
        });

        context.Services.AddElsaApiEndpoints();
        context.Services.Configure<ApiVersioningOptions>(options =>
        {
            options.UseApiBehavior = false;
        });

        Configure<AbpAntiForgeryOptions>(options =>
        {
            options.AutoValidateFilter = type => type.Assembly != typeof(Elsa.Server.Api.Endpoints.WorkflowRegistry.Get).Assembly;
        });

        context.Services.Configure<RazorPagesOptions>(options => options.RootDirectory = "/Pages");

    }

    private void ConfigureHangfire(ServiceConfigurationContext context, IConfiguration configuration)
    {
        context.Services.AddHangfire(config => config
        .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
        .UseSimpleAssemblyNameTypeSerializer()
        .UseRecommendedSerializerSettings()
        .UseSqlServerStorage(configuration.GetConnectionString("Default"), new SqlServerStorageOptions
        {
            CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
            SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
            QueuePollInterval = TimeSpan.Zero,
            UseRecommendedIsolationLevel = true,
            DisableGlobalLocks = true
        }));

        // Add the processing server as IHostedService
        context.Services.AddHangfireServer();
    }

    private void ConfigureExternalProviders(ServiceConfigurationContext context)
    {
        context.Services.AddAuthentication()
            .AddGoogle(GoogleDefaults.AuthenticationScheme, _ => { })
            .WithDynamicOptions<GoogleOptions, GoogleHandler>(
                GoogleDefaults.AuthenticationScheme,
                options =>
                {
                    options.WithProperty(x => x.ClientId);
                    options.WithProperty(x => x.ClientSecret, isSecret: true);
                }
            )
            .AddMicrosoftAccount(MicrosoftAccountDefaults.AuthenticationScheme, options =>
            {
                //Personal Microsoft accounts as an example.
                options.AuthorizationEndpoint = "https://login.microsoftonline.com/consumers/oauth2/v2.0/authorize";
                options.TokenEndpoint = "https://login.microsoftonline.com/consumers/oauth2/v2.0/token";
            })
            .WithDynamicOptions<MicrosoftAccountOptions, MicrosoftAccountHandler>(
                MicrosoftAccountDefaults.AuthenticationScheme,
                options =>
                {
                    options.WithProperty(x => x.ClientId);
                    options.WithProperty(x => x.ClientSecret, isSecret: true);
                }
            )
            .AddTwitter(TwitterDefaults.AuthenticationScheme, options => options.RetrieveUserDetails = true)
            .WithDynamicOptions<TwitterOptions, TwitterHandler>(
                TwitterDefaults.AuthenticationScheme,
                options =>
                {
                    options.WithProperty(x => x.ConsumerKey);
                    options.WithProperty(x => x.ConsumerSecret, isSecret: true);
                }
            );
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {

        var app = context.GetApplicationBuilder();
        var env = context.GetEnvironment();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseAbpRequestLocalization();

        if (!env.IsDevelopment())
        {
            app.UseErrorPage();
            app.UseHsts();
        }
        app.UseDevExpressControls();
        //app.MapDashboardRoute("api/dashboard", "DefaultDashboard");

        app.UseAbpCookieConsent();
        app.UseHttpsRedirection();
        app.UseCorrelationId();
        app.UseAbpSecurityHeaders();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAbpOpenIddictValidation();

        if (MultiTenancyConsts.IsEnabled)
        {
            app.UseMultiTenancy();
        }

        app.UseUnitOfWork();
        app.UseAuthorization();
        app.UseSwagger();
        app.UseAbpSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "ToksozBysNew API");
        });
        app.UseAuditing();
        app.UseAbpSerilogEnrichers();
        app.UseCors();

        app.UseDevExpressControls();
        app.UseCors("CorsPolicy");
        // ...
        // Requires CORS policies.

        app.UseDevExpressControls();

        app.UseHttpActivities();
        app.UseConfiguredEndpoints(endpoints => { endpoints.MapFallbackToPage("/Elsa/_Host"); });
        app.UseCors("CorsPolicy");

        var workflowRunner = context.ServiceProvider.GetRequiredService<IBuildsAndStartsWorkflow>();
        workflowRunner.BuildAndStartWorkflowAsync<DocumentApprovalWorkflow>();

        app.UseHangfireDashboard();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapHangfireDashboard();
        });
        //context.AddBackgroundWorkerAsync<MyLogWorker>();
        //    context.ServiceProvider
        //.GetRequiredService<IBackgroundWorkerManager>()
        //.AddAsync(
        //    context
        //        .ServiceProvider
        //        .GetRequiredService<MyLogWorker>()
        //);
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapHub<UiNotificationHub>("/notification-hub");
        });
    }
}