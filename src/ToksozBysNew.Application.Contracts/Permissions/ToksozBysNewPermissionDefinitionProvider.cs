using ToksozBysNew.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace ToksozBysNew.Permissions;

public class ToksozBysNewPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(ToksozBysNewPermissions.GroupName);

        myGroup.AddPermission(ToksozBysNewPermissions.Dashboard.Host, L("Permission:Dashboard"), MultiTenancySides.Host);
        myGroup.AddPermission(ToksozBysNewPermissions.Dashboard.Tenant, L("Permission:Dashboard"), MultiTenancySides.Tenant);

        myGroup.AddPermission(ToksozBysNewPermissions.Dashboard.ElsaDashboard, L("Permission:ElsaDashboard"));

        //Define your own permissions here. Example:
        //myGroup.AddPermission(ToksozBysNewPermissions.MyPermission1, L("Permission:MyPermission1"));

        var companyPermission = myGroup.AddPermission(ToksozBysNewPermissions.Companies.Default, L("Permission:Companies"), MultiTenancySides.Host);
        companyPermission.AddChild(ToksozBysNewPermissions.Companies.Create, L("Permission:Create"), MultiTenancySides.Host);
        companyPermission.AddChild(ToksozBysNewPermissions.Companies.Edit, L("Permission:Edit"), MultiTenancySides.Host);
        companyPermission.AddChild(ToksozBysNewPermissions.Companies.Delete, L("Permission:Delete"), MultiTenancySides.Host);

        var accountGroupPermission = myGroup.AddPermission(ToksozBysNewPermissions.AccountGroups.Default, L("Permission:AccountGroups"));
        accountGroupPermission.AddChild(ToksozBysNewPermissions.AccountGroups.Create, L("Permission:Create"));
        accountGroupPermission.AddChild(ToksozBysNewPermissions.AccountGroups.Edit, L("Permission:Edit"));
        accountGroupPermission.AddChild(ToksozBysNewPermissions.AccountGroups.Delete, L("Permission:Delete"));

        var departmentPermission = myGroup.AddPermission(ToksozBysNewPermissions.Departments.Default, L("Permission:Departments"));
        departmentPermission.AddChild(ToksozBysNewPermissions.Departments.Create, L("Permission:Create"));
        departmentPermission.AddChild(ToksozBysNewPermissions.Departments.Edit, L("Permission:Edit"));
        departmentPermission.AddChild(ToksozBysNewPermissions.Departments.Delete, L("Permission:Delete"));

        var accountPermission = myGroup.AddPermission(ToksozBysNewPermissions.Accounts.Default, L("Permission:Accounts"));
        accountPermission.AddChild(ToksozBysNewPermissions.Accounts.Create, L("Permission:Create"));
        accountPermission.AddChild(ToksozBysNewPermissions.Accounts.Edit, L("Permission:Edit"));
        accountPermission.AddChild(ToksozBysNewPermissions.Accounts.Delete, L("Permission:Delete"));

        var productPermission = myGroup.AddPermission(ToksozBysNewPermissions.Products.Default, L("Permission:Products"));
        productPermission.AddChild(ToksozBysNewPermissions.Products.Create, L("Permission:Create"));
        productPermission.AddChild(ToksozBysNewPermissions.Products.Edit, L("Permission:Edit"));
        productPermission.AddChild(ToksozBysNewPermissions.Products.Delete, L("Permission:Delete"));

        var budgetPermission = myGroup.AddPermission(ToksozBysNewPermissions.Budgets.Default, L("Permission:Budgets"));
        budgetPermission.AddChild(ToksozBysNewPermissions.Budgets.Create, L("Permission:Create"));
        budgetPermission.AddChild(ToksozBysNewPermissions.Budgets.Edit, L("Permission:Edit"));
        budgetPermission.AddChild(ToksozBysNewPermissions.Budgets.Delete, L("Permission:Delete"));

        var budgetDistributionPermission = myGroup.AddPermission(ToksozBysNewPermissions.BudgetDistributions.Default, L("Permission:BudgetDistributions"));
        budgetDistributionPermission.AddChild(ToksozBysNewPermissions.BudgetDistributions.Create, L("Permission:Create"));
        budgetDistributionPermission.AddChild(ToksozBysNewPermissions.BudgetDistributions.Edit, L("Permission:Edit"));
        budgetDistributionPermission.AddChild(ToksozBysNewPermissions.BudgetDistributions.Delete, L("Permission:Delete"));

        var monthPermission = myGroup.AddPermission(ToksozBysNewPermissions.Months.Default, L("Permission:Months"));
        monthPermission.AddChild(ToksozBysNewPermissions.Months.Create, L("Permission:Create"));
        monthPermission.AddChild(ToksozBysNewPermissions.Months.Edit, L("Permission:Edit"));
        monthPermission.AddChild(ToksozBysNewPermissions.Months.Delete, L("Permission:Delete"));

        var spendingGroupPermission = myGroup.AddPermission(ToksozBysNewPermissions.SpendingGroups.Default, L("Permission:SpendingGroups"));
        spendingGroupPermission.AddChild(ToksozBysNewPermissions.SpendingGroups.Create, L("Permission:Create"));
        spendingGroupPermission.AddChild(ToksozBysNewPermissions.SpendingGroups.Edit, L("Permission:Edit"));
        spendingGroupPermission.AddChild(ToksozBysNewPermissions.SpendingGroups.Delete, L("Permission:Delete"));

        var expenseMonthlyPermission = myGroup.AddPermission(ToksozBysNewPermissions.ExpenseMonthlies.Default, L("Permission:ExpenseMonthlies"));
        expenseMonthlyPermission.AddChild(ToksozBysNewPermissions.ExpenseMonthlies.Create, L("Permission:Create"));
        expenseMonthlyPermission.AddChild(ToksozBysNewPermissions.ExpenseMonthlies.Edit, L("Permission:Edit"));
        expenseMonthlyPermission.AddChild(ToksozBysNewPermissions.ExpenseMonthlies.Delete, L("Permission:Delete"));

        var masterDetailPermission = myGroup.AddPermission(ToksozBysNewPermissions.MasterDetails.Default, L("Permission:MasterDetails"));
        masterDetailPermission.AddChild(ToksozBysNewPermissions.MasterDetails.Create, L("Permission:Create"));
        masterDetailPermission.AddChild(ToksozBysNewPermissions.MasterDetails.Edit, L("Permission:Edit"));
        masterDetailPermission.AddChild(ToksozBysNewPermissions.MasterDetails.Delete, L("Permission:Delete"));

        var masterPermission = myGroup.AddPermission(ToksozBysNewPermissions.Masters.Default, L("Permission:Masters"));
        masterPermission.AddChild(ToksozBysNewPermissions.Masters.Create, L("Permission:Create"));
        masterPermission.AddChild(ToksozBysNewPermissions.Masters.Edit, L("Permission:Edit"));
        masterPermission.AddChild(ToksozBysNewPermissions.Masters.Delete, L("Permission:Delete"));

        var denemePermission = myGroup.AddPermission(ToksozBysNewPermissions.Denemes.Default, L("Permission:Denemes"));
        denemePermission.AddChild(ToksozBysNewPermissions.Denemes.Create, L("Permission:Create"));
        denemePermission.AddChild(ToksozBysNewPermissions.Denemes.Edit, L("Permission:Edit"));
        denemePermission.AddChild(ToksozBysNewPermissions.Denemes.Delete, L("Permission:Delete"));

        var denemeDetailPermission = myGroup.AddPermission(ToksozBysNewPermissions.DenemeDetails.Default, L("Permission:DenemeDetails"));
        denemeDetailPermission.AddChild(ToksozBysNewPermissions.DenemeDetails.Create, L("Permission:Create"));
        denemeDetailPermission.AddChild(ToksozBysNewPermissions.DenemeDetails.Edit, L("Permission:Edit"));
        denemeDetailPermission.AddChild(ToksozBysNewPermissions.DenemeDetails.Delete, L("Permission:Delete"));

        var invoicePermission = myGroup.AddPermission(ToksozBysNewPermissions.Invoices.Default, L("Permission:Invoices"));
        invoicePermission.AddChild(ToksozBysNewPermissions.Invoices.Create, L("Permission:Create"));
        invoicePermission.AddChild(ToksozBysNewPermissions.Invoices.Edit, L("Permission:Edit"));
        invoicePermission.AddChild(ToksozBysNewPermissions.Invoices.Delete, L("Permission:Delete"));

        var invoiceDetailPermission = myGroup.AddPermission(ToksozBysNewPermissions.InvoiceDetails.Default, L("Permission:InvoiceDetails"));
        invoiceDetailPermission.AddChild(ToksozBysNewPermissions.InvoiceDetails.Create, L("Permission:Create"));
        invoiceDetailPermission.AddChild(ToksozBysNewPermissions.InvoiceDetails.Edit, L("Permission:Edit"));
        invoiceDetailPermission.AddChild(ToksozBysNewPermissions.InvoiceDetails.Delete, L("Permission:Delete"));

        var taxListPermission = myGroup.AddPermission(ToksozBysNewPermissions.TaxLists.Default, L("Permission:TaxLists"));
        taxListPermission.AddChild(ToksozBysNewPermissions.TaxLists.Create, L("Permission:Create"));
        taxListPermission.AddChild(ToksozBysNewPermissions.TaxLists.Edit, L("Permission:Edit"));
        taxListPermission.AddChild(ToksozBysNewPermissions.TaxLists.Delete, L("Permission:Delete"));

        var doctorPermission = myGroup.AddPermission(ToksozBysNewPermissions.Doctors.Default, L("Permission:Doctors"));
        doctorPermission.AddChild(ToksozBysNewPermissions.Doctors.Create, L("Permission:Create"));
        doctorPermission.AddChild(ToksozBysNewPermissions.Doctors.Edit, L("Permission:Edit"));
        doctorPermission.AddChild(ToksozBysNewPermissions.Doctors.Delete, L("Permission:Delete"));

        var brickPermission = myGroup.AddPermission(ToksozBysNewPermissions.Bricks.Default, L("Permission:Bricks"));
        brickPermission.AddChild(ToksozBysNewPermissions.Bricks.Create, L("Permission:Create"));
        brickPermission.AddChild(ToksozBysNewPermissions.Bricks.Edit, L("Permission:Edit"));
        brickPermission.AddChild(ToksozBysNewPermissions.Bricks.Delete, L("Permission:Delete"));

        var positionPermission = myGroup.AddPermission(ToksozBysNewPermissions.Positions.Default, L("Permission:Positions"));
        positionPermission.AddChild(ToksozBysNewPermissions.Positions.Create, L("Permission:Create"));
        positionPermission.AddChild(ToksozBysNewPermissions.Positions.Edit, L("Permission:Edit"));
        positionPermission.AddChild(ToksozBysNewPermissions.Positions.Delete, L("Permission:Delete"));

        var specPermission = myGroup.AddPermission(ToksozBysNewPermissions.Specs.Default, L("Permission:Specs"));
        specPermission.AddChild(ToksozBysNewPermissions.Specs.Create, L("Permission:Create"));
        specPermission.AddChild(ToksozBysNewPermissions.Specs.Edit, L("Permission:Edit"));
        specPermission.AddChild(ToksozBysNewPermissions.Specs.Delete, L("Permission:Delete"));

        var unitPermission = myGroup.AddPermission(ToksozBysNewPermissions.Units.Default, L("Permission:Units"));
        unitPermission.AddChild(ToksozBysNewPermissions.Units.Create, L("Permission:Create"));
        unitPermission.AddChild(ToksozBysNewPermissions.Units.Edit, L("Permission:Edit"));
        unitPermission.AddChild(ToksozBysNewPermissions.Units.Delete, L("Permission:Delete"));

        var customerTitlePermission = myGroup.AddPermission(ToksozBysNewPermissions.CustomerTitles.Default, L("Permission:CustomerTitles"));
        customerTitlePermission.AddChild(ToksozBysNewPermissions.CustomerTitles.Create, L("Permission:Create"));
        customerTitlePermission.AddChild(ToksozBysNewPermissions.CustomerTitles.Edit, L("Permission:Edit"));
        customerTitlePermission.AddChild(ToksozBysNewPermissions.CustomerTitles.Delete, L("Permission:Delete"));

        var customerTypePermission = myGroup.AddPermission(ToksozBysNewPermissions.CustomerTypes.Default, L("Permission:CustomerTypes"));
        customerTypePermission.AddChild(ToksozBysNewPermissions.CustomerTypes.Create, L("Permission:Create"));
        customerTypePermission.AddChild(ToksozBysNewPermissions.CustomerTypes.Edit, L("Permission:Edit"));
        customerTypePermission.AddChild(ToksozBysNewPermissions.CustomerTypes.Delete, L("Permission:Delete"));

        var customerAddressPermission = myGroup.AddPermission(ToksozBysNewPermissions.CustomerAddresses.Default, L("Permission:CustomerAddresses"));
        customerAddressPermission.AddChild(ToksozBysNewPermissions.CustomerAddresses.Create, L("Permission:Create"));
        customerAddressPermission.AddChild(ToksozBysNewPermissions.CustomerAddresses.Edit, L("Permission:Edit"));
        customerAddressPermission.AddChild(ToksozBysNewPermissions.CustomerAddresses.Delete, L("Permission:Delete"));

        var countryPermission = myGroup.AddPermission(ToksozBysNewPermissions.Countries.Default, L("Permission:Countries"));
        countryPermission.AddChild(ToksozBysNewPermissions.Countries.Create, L("Permission:Create"));
        countryPermission.AddChild(ToksozBysNewPermissions.Countries.Edit, L("Permission:Edit"));
        countryPermission.AddChild(ToksozBysNewPermissions.Countries.Delete, L("Permission:Delete"));

        var provincePermission = myGroup.AddPermission(ToksozBysNewPermissions.Provinces.Default, L("Permission:Provinces"));
        provincePermission.AddChild(ToksozBysNewPermissions.Provinces.Create, L("Permission:Create"));
        provincePermission.AddChild(ToksozBysNewPermissions.Provinces.Edit, L("Permission:Edit"));
        provincePermission.AddChild(ToksozBysNewPermissions.Provinces.Delete, L("Permission:Delete"));

        var districtPermission = myGroup.AddPermission(ToksozBysNewPermissions.Districts.Default, L("Permission:Districts"));
        districtPermission.AddChild(ToksozBysNewPermissions.Districts.Create, L("Permission:Create"));
        districtPermission.AddChild(ToksozBysNewPermissions.Districts.Edit, L("Permission:Edit"));
        districtPermission.AddChild(ToksozBysNewPermissions.Districts.Delete, L("Permission:Delete"));

        var clinicPermission = myGroup.AddPermission(ToksozBysNewPermissions.Clinics.Default, L("Permission:Clinics"));
        clinicPermission.AddChild(ToksozBysNewPermissions.Clinics.Create, L("Permission:Create"));
        clinicPermission.AddChild(ToksozBysNewPermissions.Clinics.Edit, L("Permission:Edit"));
        clinicPermission.AddChild(ToksozBysNewPermissions.Clinics.Delete, L("Permission:Delete"));

        var visitPermission = myGroup.AddPermission(ToksozBysNewPermissions.Visits.Default, L("Permission:Visits"));
        visitPermission.AddChild(ToksozBysNewPermissions.Visits.Create, L("Permission:Create"));
        visitPermission.AddChild(ToksozBysNewPermissions.Visits.Edit, L("Permission:Edit"));
        visitPermission.AddChild(ToksozBysNewPermissions.Visits.Delete, L("Permission:Delete"));

        var visitDailyActionPermission = myGroup.AddPermission(ToksozBysNewPermissions.VisitDailyActions.Default, L("Permission:VisitDailyActions"));
        visitDailyActionPermission.AddChild(ToksozBysNewPermissions.VisitDailyActions.Create, L("Permission:Create"));
        visitDailyActionPermission.AddChild(ToksozBysNewPermissions.VisitDailyActions.Edit, L("Permission:Edit"));
        visitDailyActionPermission.AddChild(ToksozBysNewPermissions.VisitDailyActions.Delete, L("Permission:Delete"));

        var companyCalendarPermission = myGroup.AddPermission(ToksozBysNewPermissions.CompanyCalendars.Default, L("Permission:CompanyCalendars"));
        companyCalendarPermission.AddChild(ToksozBysNewPermissions.CompanyCalendars.Create, L("Permission:Create"));
        companyCalendarPermission.AddChild(ToksozBysNewPermissions.CompanyCalendars.Edit, L("Permission:Edit"));
        companyCalendarPermission.AddChild(ToksozBysNewPermissions.CompanyCalendars.Delete, L("Permission:Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<ToksozBysNewResource>(name);
    }
}