using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using ToksozBysNew.Localization;
using ToksozBysNew.Permissions;
using Volo.Abp.AuditLogging.Web.Navigation;
using Volo.Abp.Identity.Web.Navigation;
using Volo.Abp.LanguageManagement.Navigation;
using Volo.Abp.SettingManagement.Web.Navigation;
using Volo.Abp.TextTemplateManagement.Web.Navigation;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.OpenIddict.Pro.Web.Menus;
using Volo.Abp.UI.Navigation;
using Volo.Saas.Host.Navigation;

namespace ToksozBysNew.Web.Menus;

public class ToksozBysNewMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private static Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var l = context.GetLocalizer<ToksozBysNewResource>();

        //Home
        context.Menu.AddItem(
            new ApplicationMenuItem(
                ToksozBysNewMenus.Home,
                l["Menu:Home"],
                "~/",
                icon: "fa fa-home",
                order: 1
            )
        );

        //HostDashboard
        context.Menu.AddItem(
            new ApplicationMenuItem(
                ToksozBysNewMenus.HostDashboard,
                l["Menu:Dashboard"],
                "~/HostDashboard",
                icon: "fa fa-line-chart",
                order: 2
            ).RequirePermissions(ToksozBysNewPermissions.Dashboard.Host)
        );

        //TenantDashboard
        context.Menu.AddItem(
            new ApplicationMenuItem(
                ToksozBysNewMenus.TenantDashboard,
                l["Menu:Dashboard"],
                "~/Dashboard",
                icon: "fa fa-line-chart",
                order: 2
            ).RequirePermissions(ToksozBysNewPermissions.Dashboard.Tenant)
        );

        context.Menu.SetSubItemOrder(SaasHostMenuNames.GroupName, 3);

        //add Workflow menu-item
        context.Menu.Items.Insert(1, new ApplicationMenuItem(ToksozBysNewMenus.Home, "Workflow", "~/elsa", icon: "fas fa-code-branch", order: 1, requiredPermissionName: ToksozBysNewPermissions.Dashboard.ElsaDashboard));

        //Administration
        var administration = context.Menu.GetAdministration();
        administration.Order = 5;

        //Administration->Identity
        administration.SetSubItemOrder(IdentityMenuNames.GroupName, 1);

        //Administration->OpenIddict
        administration.SetSubItemOrder(OpenIddictProMenus.GroupName, 2);

        //Administration->Language Management
        administration.SetSubItemOrder(LanguageManagementMenuNames.GroupName, 3);

        //Administration->Text Template Management
        administration.SetSubItemOrder(TextTemplateManagementMainMenuNames.GroupName, 4);

        //Administration->Audit Logs
        administration.SetSubItemOrder(AbpAuditLoggingMainMenuNames.GroupName, 5);

        //Administration->Settings
        administration.SetSubItemOrder(SettingManagementMenuNames.GroupName, 6);

        context.Menu.AddItem(
            new ApplicationMenuItem(
                ToksozBysNewMenus.Companies,
                l["Menu:Companies"],
                url: "/Companies",
                icon: "fa fa-fas fa-building",
                requiredPermissionName: ToksozBysNewPermissions.Companies.Default)
        );

        context.Menu.AddItem(
            new ApplicationMenuItem(
                ToksozBysNewMenus.AccountGroups,
                l["Menu:AccountGroups"],
                url: "/AccountGroups",
                icon: "fa fa-fas fa-money-check-alt",
                requiredPermissionName: ToksozBysNewPermissions.AccountGroups.Default)
        );

        context.Menu.AddItem(
            new ApplicationMenuItem(
                ToksozBysNewMenus.Departments,
                l["Menu:Departments"],
                url: "/Departments",
                icon: "fa fa-file-alt",
                requiredPermissionName: ToksozBysNewPermissions.Departments.Default)
        );

        context.Menu.AddItem(
            new ApplicationMenuItem(
                ToksozBysNewMenus.Accounts,
                l["Menu:Accounts"],
                url: "/Accounts",
                icon: "fa fa-file-alt",
                requiredPermissionName: ToksozBysNewPermissions.Accounts.Default)
        );

        context.Menu.AddItem(
            new ApplicationMenuItem(
                ToksozBysNewMenus.Products,
                l["Menu:Products"],
                url: "/Products",
                icon: "fa fa-fab fa-product-hunt",
                requiredPermissionName: ToksozBysNewPermissions.Products.Default)
        );

        context.Menu.AddItem(
            new ApplicationMenuItem(
                ToksozBysNewMenus.Budgets,
                l["Menu:Budgets"],
                url: "/Budgets",
                icon: "fa fa-file-alt",
                requiredPermissionName: ToksozBysNewPermissions.Budgets.Default)
        );

        context.Menu.AddItem(
            new ApplicationMenuItem(
                ToksozBysNewMenus.BudgetDistributions,
                l["Menu:BudgetDistributions"],
                url: "/BudgetDistributions",
                icon: "fa fa-fas fa-wallet",
                requiredPermissionName: ToksozBysNewPermissions.BudgetDistributions.Default)
        );

        context.Menu.AddItem(
            new ApplicationMenuItem(
                ToksozBysNewMenus.ExpenseMonthlies,
                l["Menu:ExpenseMonthlies"],
                url: "/ExpenseMonthlies",
                icon: "fa fa-fas fa-lira-sign",
                requiredPermissionName: ToksozBysNewPermissions.ExpenseMonthlies.Default)
        );

        context.Menu.AddItem(
            new ApplicationMenuItem(
                ToksozBysNewMenus.Invoices,
                l["Menu:Invoices"],
                url: "/Invoices",
                icon: "fa fa-file-alt",
                requiredPermissionName: ToksozBysNewPermissions.Invoices.Default)
        );

        //context.Menu.AddItem(
        //    new ApplicationMenuItem(
        //        ToksozBysNewMenus.InvoiceDetails,
        //        l["Menu:InvoiceDetails"],
        //        url: "/InvoiceDetails",
        //        icon: "fa fa-file-alt",
        //        requiredPermissionName: ToksozBysNewPermissions.InvoiceDetails.Default)
        //);

        context.Menu.AddItem(
            new ApplicationMenuItem(
                ToksozBysNewMenus.InvoiceLists,
                l["Menu:InvoiceLists"],
                url: "/InvoiceLists",
                icon: "fa fa-file-alt",
                requiredPermissionName: ToksozBysNewPermissions.Invoices.Default)
        );

        context.Menu.AddItem(
            new ApplicationMenuItem(
                ToksozBysNewMenus.Doctors,
                l["Menu:Doctors"],
                url: "/Doctors",
                icon: "fa fa-file-alt",
                requiredPermissionName: ToksozBysNewPermissions.Doctors.Default)
        );

        context.Menu.AddItem(
            new ApplicationMenuItem(
                ToksozBysNewMenus.Bricks,
                l["Menu:Bricks"],
                url: "/Bricks",
                icon: "fa fa-file-alt",
                requiredPermissionName: ToksozBysNewPermissions.Bricks.Default)
        );

        context.Menu.AddItem(
            new ApplicationMenuItem(
                ToksozBysNewMenus.Positions,
                l["Menu:Positions"],
                url: "/Positions",
                icon: "fa fa-file-alt",
                requiredPermissionName: ToksozBysNewPermissions.Positions.Default)
        );

        context.Menu.AddItem(
            new ApplicationMenuItem(
                ToksozBysNewMenus.Specs,
                l["Menu:Specs"],
                url: "/Specs",
                icon: "fa fa-file-alt",
                requiredPermissionName: ToksozBysNewPermissions.Specs.Default)
        );

        context.Menu.AddItem(
            new ApplicationMenuItem(
                ToksozBysNewMenus.Units,
                l["Menu:Units"],
                url: "/Units",
                icon: "fa fa-file-alt",
                requiredPermissionName: ToksozBysNewPermissions.Units.Default)
        );

        context.Menu.AddItem(
            new ApplicationMenuItem(
                ToksozBysNewMenus.CustomerTitles,
                l["Menu:CustomerTitles"],
                url: "/CustomerTitles",
                icon: "fa fa-file-alt",
                requiredPermissionName: ToksozBysNewPermissions.CustomerTitles.Default)
        );

        context.Menu.AddItem(
            new ApplicationMenuItem(
                ToksozBysNewMenus.CustomerAddresses,
                l["Menu:CustomerAddresses"],
                url: "/CustomerAddresses",
                icon: "fa fa-file-alt",
                requiredPermissionName: ToksozBysNewPermissions.CustomerAddresses.Default)
        );

        context.Menu.AddItem(
            new ApplicationMenuItem(
                ToksozBysNewMenus.Countries,
                l["Menu:Countries"],
                url: "/Countries",
                icon: "fa fa-file-alt",
                requiredPermissionName: ToksozBysNewPermissions.Countries.Default)
        );

        context.Menu.AddItem(
            new ApplicationMenuItem(
                ToksozBysNewMenus.Provinces,
                l["Menu:Provinces"],
                url: "/Provinces",
                icon: "fa fa-file-alt",
                requiredPermissionName: ToksozBysNewPermissions.Provinces.Default)
        );

        context.Menu.AddItem(
            new ApplicationMenuItem(
                ToksozBysNewMenus.Districts,
                l["Menu:Districts"],
                url: "/Districts",
                icon: "fa fa-file-alt",
                requiredPermissionName: ToksozBysNewPermissions.Districts.Default)
        );

        context.Menu.AddItem(
            new ApplicationMenuItem(
                ToksozBysNewMenus.Clinics,
                l["Menu:Clinics"],
                url: "/Clinics",
                icon: "fa fa-file-alt",
                requiredPermissionName: ToksozBysNewPermissions.Clinics.Default)
        );

        context.Menu.AddItem(
            new ApplicationMenuItem(
                ToksozBysNewMenus.Visits,
                l["Menu:Visits"],
                url: "/Visits",
                icon: "fa fa-file-alt",
                requiredPermissionName: ToksozBysNewPermissions.Visits.Default)
        );

        context.Menu.AddItem(
            new ApplicationMenuItem(
                ToksozBysNewMenus.VisitDailyActions,
                l["Menu:VisitDailyActions"],
                url: "/VisitDailyActions",
                icon: "fa fa-file-alt",
                requiredPermissionName: ToksozBysNewPermissions.VisitDailyActions.Default)
        );

        context.Menu.AddItem(
            new ApplicationMenuItem(
                ToksozBysNewMenus.CompanyCalendars,
                l["Menu:CompanyCalendars"],
                url: "/CompanyCalendars",
                icon: "fa fa-file-alt",
                requiredPermissionName: ToksozBysNewPermissions.CompanyCalendars.Default)
        );
        return Task.CompletedTask;
    }
}