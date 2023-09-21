using ToksozBysNew.CompanyCalendars;
using ToksozBysNew.VisitDailyActions;
using ToksozBysNew.Visits;
using ToksozBysNew.Clinics;
using ToksozBysNew.Districts;
using ToksozBysNew.Provinces;
using ToksozBysNew.Countries;
using ToksozBysNew.CustomerAddresses;
using ToksozBysNew.CustomerTypes;
using ToksozBysNew.CustomerTitles;
using ToksozBysNew.Units;
using ToksozBysNew.Specs;
using ToksozBysNew.Positions;
using ToksozBysNew.Bricks;
using ToksozBysNew.Doctors;
using ToksozBysNew.TaxLists;
using ToksozBysNew.Masters;
using ToksozBysNew.InvoiceDetails;
using ToksozBysNew.Invoices;
using ToksozBysNew.ExpenseMonthlies;
using ToksozBysNew.SpendingGroups;
using ToksozBysNew.Months;
using ToksozBysNew.BudgetDistributions;
using ToksozBysNew.Budgets;
using ToksozBysNew.Products;
using ToksozBysNew.Accounts;
using ToksozBysNew.Departments;
using ToksozBysNew.AccountGroups;
using ToksozBysNew.Companies;
using System;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Uow;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.SqlServer;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.LanguageManagement.EntityFrameworkCore;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TextTemplateManagement.EntityFrameworkCore;
using Volo.Saas.EntityFrameworkCore;
using Volo.Abp.BlobStoring.Database.EntityFrameworkCore;
using Volo.Abp.Gdpr;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.FileManagement.EntityFrameworkCore;
using Volo.Abp.BlobStoring;
using Volo.FileManagement;
using Volo.Abp.BlobStoring.Database;

namespace ToksozBysNew.EntityFrameworkCore;

[DependsOn(
    typeof(ToksozBysNewDomainModule),
    typeof(AbpIdentityProEntityFrameworkCoreModule),
    typeof(AbpOpenIddictEntityFrameworkCoreModule),
    typeof(AbpPermissionManagementEntityFrameworkCoreModule),
    typeof(AbpSettingManagementEntityFrameworkCoreModule),
    typeof(AbpEntityFrameworkCoreSqlServerModule),
    typeof(AbpBackgroundJobsEntityFrameworkCoreModule),
    typeof(AbpAuditLoggingEntityFrameworkCoreModule),
    typeof(AbpFeatureManagementEntityFrameworkCoreModule),
    typeof(LanguageManagementEntityFrameworkCoreModule),
    typeof(SaasEntityFrameworkCoreModule),
    typeof(TextTemplateManagementEntityFrameworkCoreModule),
    typeof(AbpGdprEntityFrameworkCoreModule),
    typeof(BlobStoringDatabaseEntityFrameworkCoreModule),
    typeof(FileManagementEntityFrameworkCoreModule),
    typeof(FileManagementApplicationModule),
    typeof(BlobStoringDatabaseDomainModule)
    )]
public class ToksozBysNewEntityFrameworkCoreModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        ToksozBysNewEfCoreEntityExtensionMappings.Configure();
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<ToksozBysNewDbContext>(options =>
        {
            /* Remove "includeAllEntities: true" to create
             * default repositories only for aggregate roots */
            options.AddDefaultRepositories(includeAllEntities: true);
            options.AddRepository<Company, Companies.EfCoreCompanyRepository>();

            options.AddRepository<AccountGroup, AccountGroups.EfCoreAccountGroupRepository>();

            options.AddRepository<Department, Departments.EfCoreDepartmentRepository>();

            options.AddRepository<Account, Accounts.EfCoreAccountRepository>();

            options.AddRepository<Product, Products.EfCoreProductRepository>();

            options.AddRepository<Budget, Budgets.EfCoreBudgetRepository>();

            options.AddRepository<BudgetDistribution, BudgetDistributions.EfCoreBudgetDistributionRepository>();

            options.AddRepository<Month, Months.EfCoreMonthRepository>();

            options.AddRepository<SpendingGroup, SpendingGroups.EfCoreSpendingGroupRepository>();

            options.AddRepository<ExpenseMonthly, ExpenseMonthlies.EfCoreExpenseMonthlyRepository>();

            options.AddRepository<Invoice, Invoices.EfCoreInvoiceRepository>();

            options.AddRepository<InvoiceDetail, InvoiceDetails.EfCoreInvoiceDetailRepository>();
            options.AddRepository<Master, Masters.EfCoreMasterRepository>();

            options.AddRepository<TaxList, TaxLists.EfCoreTaxListRepository>();

            options.AddRepository<Doctor, Doctors.EfCoreDoctorRepository>();

            options.AddRepository<Brick, Bricks.EfCoreBrickRepository>();

            options.AddRepository<Position, Positions.EfCorePositionRepository>();

            options.AddRepository<Spec, Specs.EfCoreSpecRepository>();

            options.AddRepository<Unit, Units.EfCoreUnitRepository>();

            options.AddRepository<CustomerTitle, CustomerTitles.EfCoreCustomerTitleRepository>();

            options.AddRepository<CustomerType, CustomerTypes.EfCoreCustomerTypeRepository>();

            options.AddRepository<CustomerAddress, CustomerAddresses.EfCoreCustomerAddressRepository>();

            options.AddRepository<Country, Countries.EfCoreCountryRepository>();

            options.AddRepository<Province, Provinces.EfCoreProvinceRepository>();

            options.AddRepository<District, Districts.EfCoreDistrictRepository>();

            options.AddRepository<Clinic, Clinics.EfCoreClinicRepository>();

            options.AddRepository<Visit, Visits.EfCoreVisitRepository>();

            options.AddRepository<VisitDailyAction, VisitDailyActions.EfCoreVisitDailyActionRepository>();

            options.AddRepository<CompanyCalendar, CompanyCalendars.EfCoreCompanyCalendarRepository>();

        });

        Configure<AbpDbContextOptions>(options =>
        {
            /* The main point to change your DBMS.
             * See also ToksozBysNewDbContextFactory for EF Core tooling. */
            options.UseSqlServer();

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