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
using Volo.Abp.EntityFrameworkCore.Modeling;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.BlobStoring.Database.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.LanguageManagement.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TextTemplateManagement.EntityFrameworkCore;
using Volo.Saas.EntityFrameworkCore;
using Volo.Saas.Editions;
using Volo.Saas.Tenants;
using Volo.Abp.Gdpr;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using ToksozBysNew.ApplicationUsers;
using Volo.FileManagement.EntityFrameworkCore;
using ToksozBysNew.BLOBStorage;
using System.Reflection.Emit;

namespace ToksozBysNew.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityProDbContext))]
[ReplaceDbContext(typeof(ISaasDbContext))]
[ConnectionStringName("Default")]
public class ToksozBysNewDbContext :
    AbpDbContext<ToksozBysNewDbContext>,
    IIdentityProDbContext,
    ISaasDbContext
{
    public DbSet<CompanyCalendar> CompanyCalendars { get; set; }
    public DbSet<VisitDailyAction> VisitDailyActions { get; set; }
    public DbSet<Visit> Visits { get; set; }
    public DbSet<Clinic> Clinics { get; set; }
    public DbSet<District> Districts { get; set; }
    public DbSet<Province> Provinces { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<CustomerAddress> CustomerAddresses { get; set; }
    public DbSet<CustomerType> CustomerTypes { get; set; }
    public DbSet<CustomerTitle> CustomerTitles { get; set; }
    public DbSet<Unit> Units { get; set; }
    public DbSet<Spec> Specs { get; set; }
    public DbSet<Position> Positions { get; set; }
    public DbSet<Brick> Bricks { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<TaxList> TaxLists { get; set; }
    public DbSet<Master> Masters { get; set; }
    public DbSet<InvoiceDetail> InvoiceDetails { get; set; }
    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<ExpenseMonthly> ExpenseMonthlies { get; set; }
    public DbSet<SpendingGroup> SpendingGroups { get; set; }
    public DbSet<Month> Months { get; set; }
    public DbSet<BudgetDistribution> BudgetDistributions { get; set; }
    public DbSet<Budget> Budgets { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<AccountGroup> AccountGroups { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    /* Add DbSet properties for your Aggregate Roots / Entities here. */

    #region Entities from the modules

    /* Notice: We only implemented IIdentityProDbContext and ISaasDbContext
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityProDbContext and ISaasDbContext.
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

    // Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }

    // SaaS
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<Edition> Editions { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    #endregion

    public ToksozBysNewDbContext(DbContextOptions<ToksozBysNewDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureIdentityPro();
        builder.ConfigureOpenIddict();
        builder.ConfigureFeatureManagement();
        builder.ConfigureLanguageManagement();
        builder.ConfigureSaas();
        builder.ConfigureTextTemplateManagement();
        builder.ConfigureBlobStoring();
        builder.ConfigureGdpr();
        builder.ConfigureFileManagement();
        builder.ConfigureBlobStoring();

        /* Configure your own tables/entities inside here */

        //builder.Entity<YourEntity>(b =>
        //{
        //    b.ToTable(ToksozBysNewConsts.DbTablePrefix + "YourEntities", ToksozBysNewConsts.DbSchema);
        //    b.ConfigureByConvention(); //auto configure for the base class props
        //    //...
        //});
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<AccountGroup>(b =>
{
    b.ToTable(ToksozBysNewConsts.DbTablePrefix + "AccountGroups", ToksozBysNewConsts.DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.AccountGroupName).HasColumnName(nameof(AccountGroup.AccountGroupName)).IsRequired().HasMaxLength(AccountGroupConsts.AccountGroupNameMaxLength);
    b.Property(x => x.IsUnitEnterable).HasColumnName(nameof(AccountGroup.IsUnitEnterable));
});

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<Account>(b =>
{
    b.ToTable(ToksozBysNewConsts.DbTablePrefix + "Accounts", ToksozBysNewConsts.DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.AccountCode).HasColumnName(nameof(Account.AccountCode)).IsRequired().HasMaxLength(AccountConsts.AccountCodeMaxLength);
    b.Property(x => x.AccountName).HasColumnName(nameof(Account.AccountName)).IsRequired().HasMaxLength(AccountConsts.AccountNameMaxLength);
    b.Property(x => x.Description).HasColumnName(nameof(Account.Description)).HasMaxLength(AccountConsts.DescriptionMaxLength);
    b.Property(x => x.IsActive).HasColumnName(nameof(Account.IsActive));
});

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<Product>(b =>
{
    b.ToTable(ToksozBysNewConsts.DbTablePrefix + "Products", ToksozBysNewConsts.DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.ProductName).HasColumnName(nameof(Product.ProductName)).IsRequired().HasMaxLength(ProductConsts.ProductNameMaxLength);
});

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<Budget>(b =>
{
    b.ToTable(ToksozBysNewConsts.DbTablePrefix + "Budgets", ToksozBysNewConsts.DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.BudgetName).HasColumnName(nameof(Budget.BudgetName)).IsRequired().HasMaxLength(BudgetConsts.BudgetNameMaxLength);
    b.Property(x => x.Year).HasColumnName(nameof(Budget.Year));
    b.Property(x => x.Comment).HasColumnName(nameof(Budget.Comment)).HasMaxLength(BudgetConsts.CommentMaxLength);
    b.Property(x => x.IsActive).HasColumnName(nameof(Budget.IsActive));
    b.Property(x => x.OpenUntil).HasColumnName(nameof(Budget.OpenUntil));
    b.HasOne<Company>().WithMany().HasForeignKey(x => x.CompanyId).OnDelete(DeleteBehavior.NoAction);
});

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<Company>(b =>
{
    b.ToTable(ToksozBysNewConsts.DbTablePrefix + "Companies", ToksozBysNewConsts.DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.CompanyName).HasColumnName(nameof(Company.CompanyName)).IsRequired().HasMaxLength(CompanyConsts.CompanyNameMaxLength);
    b.Property(x => x.IsActive).HasColumnName(nameof(Company.IsActive));
});

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<Department>(b =>
{
    b.ToTable(ToksozBysNewConsts.DbTablePrefix + "Departments", ToksozBysNewConsts.DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.DepartmentName).HasColumnName(nameof(Department.DepartmentName)).HasMaxLength(DepartmentConsts.DepartmentNameMaxLength);
    b.HasOne<Company>().WithMany().HasForeignKey(x => x.CompanyId).OnDelete(DeleteBehavior.NoAction);
});
            builder.Entity<ApplicationUser>(b =>
            {
                b.ConfigureByConvention();
                b.HasOne<Company>().WithMany().HasForeignKey(x => x.CompanyId).OnDelete(DeleteBehavior.NoAction);
                b.HasOne<Department>().WithMany().HasForeignKey(x => x.DepartmentId).OnDelete(DeleteBehavior.NoAction);
            });

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<Month>(b =>
{
    b.ToTable(ToksozBysNewConsts.DbTablePrefix + "Months", ToksozBysNewConsts.DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.Name).HasColumnName(nameof(Month.Name));
});

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<SpendingGroup>(b =>
{
    b.ToTable(ToksozBysNewConsts.DbTablePrefix + "SpendingGroups", ToksozBysNewConsts.DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.Name).HasColumnName(nameof(SpendingGroup.Name));
});

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<BudgetDistribution>(b =>
{
    b.ToTable(ToksozBysNewConsts.DbTablePrefix + "BudgetDistributions", ToksozBysNewConsts.DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.CostCenter).HasColumnName(nameof(BudgetDistribution.CostCenter)).HasMaxLength(BudgetDistributionConsts.CostCenterMaxLength);
    b.Property(x => x.ExpenseType).HasColumnName(nameof(BudgetDistribution.ExpenseType)).HasMaxLength(BudgetDistributionConsts.ExpenseTypeMaxLength);
    b.Property(x => x.ProjectItem).HasColumnName(nameof(BudgetDistribution.ProjectItem));
    b.Property(x => x.Type).HasColumnName(nameof(BudgetDistribution.Type));
    b.Property(x => x.Unit).HasColumnName(nameof(BudgetDistribution.Unit));
    b.Property(x => x.UnitValue).HasColumnName(nameof(BudgetDistribution.UnitValue));
    b.Property(x => x.Month).HasColumnName(nameof(BudgetDistribution.Month));
    b.Property(x => x.Year).HasColumnName(nameof(BudgetDistribution.Year));
    b.Property(x => x.Ratio).HasColumnName(nameof(BudgetDistribution.Ratio));
    b.Property(x => x.Amount).HasColumnName(nameof(BudgetDistribution.Amount));
    b.Property(x => x.Memo).HasColumnName(nameof(BudgetDistribution.Memo));
    b.Property(x => x.Invoice).HasColumnName(nameof(BudgetDistribution.Invoice));
    b.Property(x => x.Currency).HasColumnName(nameof(BudgetDistribution.Currency));
    b.Property(x => x.CurrencyAmount).HasColumnName(nameof(BudgetDistribution.CurrencyAmount));
    b.Property(x => x.ExpenseCategory).HasColumnName(nameof(BudgetDistribution.ExpenseCategory));
    b.Property(x => x.ExpenseNecessity).HasColumnName(nameof(BudgetDistribution.ExpenseNecessity));
    b.Property(x => x.Comment).HasColumnName(nameof(BudgetDistribution.Comment));
    b.Property(x => x.Status).HasColumnName(nameof(BudgetDistribution.Status)).HasMaxLength(BudgetDistributionConsts.StatusMaxLength);
    b.Property(x => x.Approval).HasColumnName(nameof(BudgetDistribution.Approval));
    b.Property(x => x.IsActive).HasColumnName(nameof(BudgetDistribution.IsActive));
    b.HasOne<Department>().WithMany().IsRequired().HasForeignKey(x => x.DepartmentId).OnDelete(DeleteBehavior.NoAction);
    b.HasOne<Product>().WithMany().HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.NoAction);
    b.HasOne<Budget>().WithMany().IsRequired().HasForeignKey(x => x.BudgetId).OnDelete(DeleteBehavior.NoAction);
    b.HasOne<AccountGroup>().WithMany().HasForeignKey(x => x.AccountGroupId).OnDelete(DeleteBehavior.NoAction);
    b.HasOne<Account>().WithMany().HasForeignKey(x => x.AccountId).OnDelete(DeleteBehavior.NoAction);
    b.HasOne<IdentityUser>().WithMany().HasForeignKey(x => x.IdentityUserId).OnDelete(DeleteBehavior.NoAction);
});

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<ExpenseMonthly>(b =>
{
    b.ToTable(ToksozBysNewConsts.DbTablePrefix + "ExpenseMonthlies", ToksozBysNewConsts.DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.AccountId).HasColumnName(nameof(ExpenseMonthly.AccountId));
    b.Property(x => x.AccountGroup).HasColumnName(nameof(ExpenseMonthly.AccountGroup));
    b.Property(x => x.Account).HasColumnName(nameof(ExpenseMonthly.Account));
    b.Property(x => x.Department).HasColumnName(nameof(ExpenseMonthly.Department));
    b.Property(x => x.ExpenseType).HasColumnName(nameof(ExpenseMonthly.ExpenseType));
    b.Property(x => x.Product).HasColumnName(nameof(ExpenseMonthly.Product));
    b.Property(x => x.Proje).HasColumnName(nameof(ExpenseMonthly.Proje));
    b.Property(x => x.Comment).HasColumnName(nameof(ExpenseMonthly.Comment));
    b.Property(x => x.Month).HasColumnName(nameof(ExpenseMonthly.Month));
    b.Property(x => x.Year).HasColumnName(nameof(ExpenseMonthly.Year));
    b.Property(x => x.Unit).HasColumnName(nameof(ExpenseMonthly.Unit));
    b.Property(x => x.UnitValue).HasColumnName(nameof(ExpenseMonthly.UnitValue));
    b.Property(x => x.Amount).HasColumnName(nameof(ExpenseMonthly.Amount));
    b.Property(x => x.Memo).HasColumnName(nameof(ExpenseMonthly.Memo));
    b.Property(x => x.Invoice).HasColumnName(nameof(ExpenseMonthly.Invoice));
    b.Property(x => x.Remain).HasColumnName(nameof(ExpenseMonthly.Remain));
});

        }
        if (builder.IsHostDatabase())
        {

            if (builder.IsHostDatabase())
            {

            }
            if (builder.IsHostDatabase())
            {

            }
            if (builder.IsHostDatabase())
            {

            }
            if (builder.IsHostDatabase())
            {

            }
            if (builder.IsHostDatabase())
            {

            }
        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<Master>(b =>
{
    b.ToTable(ToksozBysNewConsts.DbTablePrefix + "Masters", ToksozBysNewConsts.DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.InvoiceSerialNo).HasColumnName(nameof(Master.InvoiceSerialNo));
    b.Property(x => x.InvoicePrice).HasColumnName(nameof(Master.InvoicePrice));
    b.Property(x => x.InvoiceDate).HasColumnName(nameof(Master.InvoiceDate));
    b.Property(x => x.InvoiceNote).HasColumnName(nameof(Master.InvoiceNote));
    b.HasOne<Company>().WithMany().HasForeignKey(x => x.CompanyId).OnDelete(DeleteBehavior.NoAction);
});

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<TaxList>(b =>
{
    b.ToTable(ToksozBysNewConsts.DbTablePrefix + "TaxLists", ToksozBysNewConsts.DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.TaxName).HasColumnName(nameof(TaxList.TaxName));
    b.Property(x => x.TaxValue).HasColumnName(nameof(TaxList.TaxValue)).HasMaxLength(TaxListConsts.TaxValueMaxLength);
});

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<InvoiceDetail>(b =>
{
    b.ToTable(ToksozBysNewConsts.DbTablePrefix + "InvoiceDetails", ToksozBysNewConsts.DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.InvoiceDetailQuantity).HasColumnName(nameof(InvoiceDetail.InvoiceDetailQuantity)).HasMaxLength(InvoiceDetailConsts.InvoiceDetailQuantityMaxLength);
    b.Property(x => x.InvoiceDetailPrice).HasColumnName(nameof(InvoiceDetail.InvoiceDetailPrice)).HasMaxLength((int)InvoiceDetailConsts.InvoiceDetailPriceMaxLength);
    b.Property(x => x.InvoiceDetailNote).HasColumnName(nameof(InvoiceDetail.InvoiceDetailNote)).IsRequired();
    b.Property(x => x.InvoiceDetailDate).HasColumnName(nameof(InvoiceDetail.InvoiceDetailDate));
    b.Property(x => x.Tax).HasColumnName(nameof(InvoiceDetail.Tax)).IsRequired();
    b.Property(x => x.TaxName).HasColumnName(nameof(InvoiceDetail.TaxName));
    b.HasOne<Invoice>().WithMany().HasForeignKey(x => x.InvoiceId).OnDelete(DeleteBehavior.NoAction);
    b.HasOne<TaxList>().WithMany().HasForeignKey(x => x.TaxListId).OnDelete(DeleteBehavior.NoAction);
});

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<Invoice>(b =>
{
    b.ToTable(ToksozBysNewConsts.DbTablePrefix + "Invoices", ToksozBysNewConsts.DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.InvoiceSerialNo).HasColumnName(nameof(Invoice.InvoiceSerialNo));
    b.Property(x => x.InvoiceDate).HasColumnName(nameof(Invoice.InvoiceDate));
    b.Property(x => x.Notes).HasColumnName(nameof(Invoice.Notes));
    b.Property(x => x.PaymentDate).HasColumnName(nameof(Invoice.PaymentDate));
    b.Property(x => x.Amount).HasColumnName(nameof(Invoice.Amount));
    b.Property(x => x.ApprovalStatus).HasColumnName(nameof(Invoice.ApprovalStatus));
});

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<Position>(b =>
{
    b.ToTable(ToksozBysNewConsts.DbTablePrefix + "Positions", ToksozBysNewConsts.DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.PositionCode).HasColumnName(nameof(Position.PositionCode));
    b.Property(x => x.PositionName).HasColumnName(nameof(Position.PositionName));
});

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<Spec>(b =>
{
    b.ToTable(ToksozBysNewConsts.DbTablePrefix + "Specs", ToksozBysNewConsts.DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.SpecCode).HasColumnName(nameof(Spec.SpecCode));
    b.Property(x => x.SpecName).HasColumnName(nameof(Spec.SpecName));
});

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<CustomerTitle>(b =>
{
    b.ToTable(ToksozBysNewConsts.DbTablePrefix + "CustomerTitles", ToksozBysNewConsts.DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.TitleName).HasColumnName(nameof(CustomerTitle.TitleName));
});

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<Brick>(b =>
{
    b.ToTable(ToksozBysNewConsts.DbTablePrefix + "Bricks", ToksozBysNewConsts.DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.BrickName).HasColumnName(nameof(Brick.BrickName));
});

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<Unit>(b =>
{
    b.ToTable(ToksozBysNewConsts.DbTablePrefix + "Units", ToksozBysNewConsts.DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.UnitName).HasColumnName(nameof(Unit.UnitName));
    b.HasOne<Brick>().WithMany().HasForeignKey(x => x.BrickId).OnDelete(DeleteBehavior.NoAction);
});

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<CustomerType>(b =>
{
    b.ToTable(ToksozBysNewConsts.DbTablePrefix + "CustomerTypes", ToksozBysNewConsts.DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.TypeName).HasColumnName(nameof(CustomerType.TypeName));
});

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<Doctor>(b =>
{
    b.ToTable(ToksozBysNewConsts.DbTablePrefix + "Doctors", ToksozBysNewConsts.DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.IsActive).HasColumnName(nameof(Doctor.IsActive));
    b.Property(x => x.NameSurname).HasColumnName(nameof(Doctor.NameSurname));
    b.Property(x => x.PharmacyName).HasColumnName(nameof(Doctor.PharmacyName));
    b.HasOne<Position>().WithMany().HasForeignKey(x => x.PositionId).OnDelete(DeleteBehavior.NoAction);
    b.HasOne<Spec>().WithMany().HasForeignKey(x => x.SpecId).OnDelete(DeleteBehavior.NoAction);
    b.HasOne<CustomerTitle>().WithMany().HasForeignKey(x => x.CustomerTitleId).OnDelete(DeleteBehavior.NoAction);
    b.HasOne<Unit>().WithMany().HasForeignKey(x => x.UnitId).OnDelete(DeleteBehavior.NoAction);
    b.HasOne<CustomerType>().WithMany().HasForeignKey(x => x.CustomerTypeId).OnDelete(DeleteBehavior.NoAction);
});

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<Country>(b =>
{
    b.ToTable(ToksozBysNewConsts.DbTablePrefix + "Countries", ToksozBysNewConsts.DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.CountryName).HasColumnName(nameof(Country.CountryName));
});

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<Province>(b =>
{
    b.ToTable(ToksozBysNewConsts.DbTablePrefix + "Provinces", ToksozBysNewConsts.DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.ProvinceName).HasColumnName(nameof(Province.ProvinceName));
    b.HasOne<Country>().WithMany().HasForeignKey(x => x.CountryId).OnDelete(DeleteBehavior.NoAction);
});

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<District>(b =>
{
    b.ToTable(ToksozBysNewConsts.DbTablePrefix + "Districts", ToksozBysNewConsts.DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.DistrictName).HasColumnName(nameof(District.DistrictName));
    b.HasOne<Country>().WithMany().HasForeignKey(x => x.CountryId).OnDelete(DeleteBehavior.NoAction);
    b.HasOne<Province>().WithMany().HasForeignKey(x => x.ProvinceId).OnDelete(DeleteBehavior.NoAction);
});

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<CustomerAddress>(b =>
{
    b.ToTable(ToksozBysNewConsts.DbTablePrefix + "CustomerAddresses", ToksozBysNewConsts.DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.Address).HasColumnName(nameof(CustomerAddress.Address));
    b.HasOne<Doctor>().WithMany().HasForeignKey(x => x.DoctorId).OnDelete(DeleteBehavior.NoAction);
    b.HasOne<Brick>().WithMany().HasForeignKey(x => x.BrickId).OnDelete(DeleteBehavior.NoAction);
    b.HasOne<District>().WithMany().HasForeignKey(x => x.DistrictId).OnDelete(DeleteBehavior.NoAction);
    b.HasOne<Country>().WithMany().HasForeignKey(x => x.CountryId).OnDelete(DeleteBehavior.NoAction);
    b.HasOne<Province>().WithMany().HasForeignKey(x => x.ProvinceId).OnDelete(DeleteBehavior.NoAction);
});

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<Clinic>(b =>
{
    b.ToTable(ToksozBysNewConsts.DbTablePrefix + "Clinics", ToksozBysNewConsts.DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.ClinicName).HasColumnName(nameof(Clinic.ClinicName));
    b.HasOne<Unit>().WithMany().HasForeignKey(x => x.UnitId).OnDelete(DeleteBehavior.NoAction);
    b.HasOne<Spec>().WithMany().HasForeignKey(x => x.SpecId).OnDelete(DeleteBehavior.NoAction);
});

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<VisitDailyAction>(b =>
{
    b.ToTable(ToksozBysNewConsts.DbTablePrefix + "VisitDailyActions", ToksozBysNewConsts.DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.VisitDailyDate).HasColumnName(nameof(VisitDailyAction.VisitDailyDate));
    b.Property(x => x.VisitDaily1).HasColumnName(nameof(VisitDailyAction.VisitDaily1));
    b.Property(x => x.VisitDaily2).HasColumnName(nameof(VisitDailyAction.VisitDaily2));
    b.Property(x => x.VisitDaily3).HasColumnName(nameof(VisitDailyAction.VisitDaily3));
    b.Property(x => x.VisitDaily4).HasColumnName(nameof(VisitDailyAction.VisitDaily4));
    b.Property(x => x.VisitDaily5).HasColumnName(nameof(VisitDailyAction.VisitDaily5));
    b.Property(x => x.VisitDaily6).HasColumnName(nameof(VisitDailyAction.VisitDaily6));
    b.Property(x => x.VisitDaily7).HasColumnName(nameof(VisitDailyAction.VisitDaily7));
    b.Property(x => x.VisitDaily8).HasColumnName(nameof(VisitDailyAction.VisitDaily8));
    b.Property(x => x.VisitDaily9).HasColumnName(nameof(VisitDailyAction.VisitDaily9));
    b.Property(x => x.VisitDaily10).HasColumnName(nameof(VisitDailyAction.VisitDaily10));
    b.Property(x => x.VisitDaily11).HasColumnName(nameof(VisitDailyAction.VisitDaily11));
    b.Property(x => x.VisitDaily12).HasColumnName(nameof(VisitDailyAction.VisitDaily12));
    b.Property(x => x.VisitDaily13).HasColumnName(nameof(VisitDailyAction.VisitDaily13));
    b.Property(x => x.VisitDaily14).HasColumnName(nameof(VisitDailyAction.VisitDaily14));
    b.Property(x => x.VisitDaily15).HasColumnName(nameof(VisitDailyAction.VisitDaily15));
    b.Property(x => x.VisitDailyCloseDate).HasColumnName(nameof(VisitDailyAction.VisitDailyCloseDate));
    b.Property(x => x.VisitDailyNote).HasColumnName(nameof(VisitDailyAction.VisitDailyNote));
    b.HasOne<IdentityUser>().WithMany().HasForeignKey(x => x.IdentityUserId).OnDelete(DeleteBehavior.NoAction);
});

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<CompanyCalendar>(b =>
{
    b.ToTable(ToksozBysNewConsts.DbTablePrefix + "CompanyCalendars", ToksozBysNewConsts.DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.CompanyCalendarDate).HasColumnName(nameof(CompanyCalendar.CompanyCalendarDate));
    b.Property(x => x.IsWeekend).HasColumnName(nameof(CompanyCalendar.IsWeekend));
    b.Property(x => x.IsHoliday).HasColumnName(nameof(CompanyCalendar.IsHoliday));
});

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<Visit>(b =>
{
    b.ToTable(ToksozBysNewConsts.DbTablePrefix + "Visits", ToksozBysNewConsts.DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.VisitDate).HasColumnName(nameof(Visit.VisitDate));
    b.Property(x => x.VisitNotes).HasColumnName(nameof(Visit.VisitNotes));
    b.HasOne<Doctor>().WithMany().HasForeignKey(x => x.DoctorId).OnDelete(DeleteBehavior.NoAction);
    b.HasOne<Unit>().WithMany().HasForeignKey(x => x.UnitId).OnDelete(DeleteBehavior.NoAction);
    b.HasOne<Clinic>().WithMany().HasForeignKey(x => x.ClinicId).OnDelete(DeleteBehavior.NoAction);
    b.HasOne<Brick>().WithMany().HasForeignKey(x => x.BrickId).OnDelete(DeleteBehavior.NoAction);
    b.HasOne<IdentityUser>().WithMany().HasForeignKey(x => x.IdentityUserId).OnDelete(DeleteBehavior.NoAction);
    b.HasOne<Spec>().WithMany().HasForeignKey(x => x.SpecId).OnDelete(DeleteBehavior.NoAction);
});

        }
    }
}