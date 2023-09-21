using ToksozBysNew.Web.Pages.CompanyCalendars;
using ToksozBysNew.CompanyCalendars;
using ToksozBysNew.Web.Pages.VisitDailyActions;
using ToksozBysNew.VisitDailyActions;
using ToksozBysNew.Web.Pages.Visits;
using ToksozBysNew.Visits;
using ToksozBysNew.Web.Pages.Clinics;
using ToksozBysNew.Clinics;
using ToksozBysNew.Web.Pages.Districts;
using ToksozBysNew.Districts;
using ToksozBysNew.Web.Pages.Provinces;
using ToksozBysNew.Provinces;
using ToksozBysNew.Web.Pages.Countries;
using ToksozBysNew.Countries;
using ToksozBysNew.Web.Pages.CustomerAddresses;
using ToksozBysNew.CustomerAddresses;
using ToksozBysNew.Web.Pages.CustomerTitles;
using ToksozBysNew.CustomerTitles;
using ToksozBysNew.Web.Pages.Units;
using ToksozBysNew.Units;
using ToksozBysNew.Web.Pages.Specs;
using ToksozBysNew.Specs;
using ToksozBysNew.Web.Pages.Positions;
using ToksozBysNew.Positions;
using ToksozBysNew.Web.Pages.Bricks;
using ToksozBysNew.Bricks;
using ToksozBysNew.Web.Pages.Doctors;
using Volo.Abp.AutoMapper;
using ToksozBysNew.Doctors;
using AutoMapper;
using ToksozBysNew.AccountGroups;
using ToksozBysNew.Accounts;
using ToksozBysNew.BudgetDistributions;
using ToksozBysNew.Budgets;
using ToksozBysNew.Companies;
using ToksozBysNew.Departments;
using ToksozBysNew.ExpenseMonthlies;
using ToksozBysNew.InvoiceDetails;
using ToksozBysNew.Invoices;
using ToksozBysNew.Masters;
using ToksozBysNew.Products;
using ToksozBysNew.Web.Pages.AccountGroups;
using ToksozBysNew.Web.Pages.Accounts;
using ToksozBysNew.Web.Pages.BudgetDistributions;
using ToksozBysNew.Web.Pages.Budgets;
using ToksozBysNew.Web.Pages.Companies;
using ToksozBysNew.Web.Pages.Departments;
using ToksozBysNew.Web.Pages.ExpenseMonthlies;
using ToksozBysNew.Web.Pages.InvoiceDetails;
using ToksozBysNew.Web.Pages.InvoiceLists;
using ToksozBysNew.Web.Pages.Invoices;
using ToksozBysNew.Web.Pages.Products;
using static ToksozBysNew.Web.Pages.Invoices.IndexModel;
using static ToksozBysNew.Web.Pages.Doctors.DetailModel;

namespace ToksozBysNew.Web;

public class ToksozBysNewWebAutoMapperProfile : Profile
{
    public ToksozBysNewWebAutoMapperProfile()
    {
        //Define your object mappings here, for the Web project

        CreateMap<CompanyDto, CompanyUpdateViewModel>();
        CreateMap<CompanyUpdateViewModel, CompanyUpdateDto>();
        CreateMap<CompanyCreateViewModel, CompanyCreateDto>();

        CreateMap<AccountGroupDto, AccountGroupUpdateViewModel>();
        CreateMap<AccountGroupUpdateViewModel, AccountGroupUpdateDto>();
        CreateMap<AccountGroupCreateViewModel, AccountGroupCreateDto>();

        CreateMap<DepartmentDto, DepartmentUpdateViewModel>();
        CreateMap<DepartmentUpdateViewModel, DepartmentUpdateDto>();
        CreateMap<DepartmentCreateViewModel, DepartmentCreateDto>();

        CreateMap<AccountDto, AccountUpdateViewModel>();
        CreateMap<AccountUpdateViewModel, AccountUpdateDto>();
        CreateMap<AccountCreateViewModel, AccountCreateDto>();

        CreateMap<ProductDto, ProductUpdateViewModel>();
        CreateMap<ProductUpdateViewModel, ProductUpdateDto>();
        CreateMap<ProductCreateViewModel, ProductCreateDto>();

        CreateMap<BudgetDto, BudgetUpdateViewModel>();
        CreateMap<BudgetUpdateViewModel, BudgetUpdateDto>();
        CreateMap<BudgetCreateViewModel, BudgetCreateDto>();

        CreateMap<BudgetDistributionDto, BudgetDistributionUpdateViewModel>();
        CreateMap<BudgetDistributionUpdateViewModel, BudgetDistributionUpdateDto>();
        CreateMap<BudgetDistributionCreateViewModel, BudgetDistributionCreateDto>();

        CreateMap<ExpenseMonthlyDto, ExpenseMonthlyUpdateViewModel>();
        CreateMap<ExpenseMonthlyUpdateViewModel, ExpenseMonthlyUpdateDto>();
        CreateMap<ExpenseMonthlyCreateViewModel, ExpenseMonthlyCreateDto>();

        CreateMap<InvoiceDto, InvoiceUpdateViewModel>();
        CreateMap<InvoiceDto, InvoiceViewModel>();
        CreateMap<InvoiceDto, InvoiceListViewModel>();
        CreateMap<InvoiceUpdateViewModel, InvoiceUpdateDto>();
        CreateMap<InvoiceCreateViewModel, InvoiceCreateDto>();
        CreateMap<InvoiceCreationViewModel, InvoiceCreateDto>();
        CreateMap<InvoiceViewModel, InvoiceCreationViewModel>();
        CreateMap<InvoiceViewModel, InvoiceCreateDto>();

        CreateMap<InvoiceDetailDto, InvoiceDetailUpdateViewModel>();
        CreateMap<InvoiceDetailUpdateViewModel, InvoiceDetailUpdateDto>();
        CreateMap<InvoiceDetailCreateViewModel, InvoiceDetailCreateDto>();
        CreateMap<string, InvoiceDetail>();

        CreateMap<DoctorDto, DoctorUpdateViewModel>();
        CreateMap<DoctorUpdateViewModel, DoctorUpdateDto>();
        CreateMap<DoctorCreateViewModel, DoctorCreateDto>();

        CreateMap<BrickDto, BrickUpdateViewModel>();
        CreateMap<BrickUpdateViewModel, BrickUpdateDto>();
        CreateMap<BrickCreateViewModel, BrickCreateDto>();

        CreateMap<PositionDto, PositionUpdateViewModel>();
        CreateMap<PositionUpdateViewModel, PositionUpdateDto>();
        CreateMap<PositionCreateViewModel, PositionCreateDto>();

        CreateMap<SpecDto, SpecUpdateViewModel>();
        CreateMap<SpecUpdateViewModel, SpecUpdateDto>();
        CreateMap<SpecCreateViewModel, SpecCreateDto>();

        CreateMap<UnitDto, UnitUpdateViewModel>();
        CreateMap<UnitUpdateViewModel, UnitUpdateDto>();
        CreateMap<UnitCreateViewModel, UnitCreateDto>();

        CreateMap<DoctorDto, DetailModel.DoctorViewModel>(); 

        CreateMap<CustomerTitleDto, CustomerTitleUpdateViewModel>();
        CreateMap<CustomerTitleUpdateViewModel, CustomerTitleUpdateDto>();
        CreateMap<CustomerTitleCreateViewModel, CustomerTitleCreateDto>();

        CreateMap<CustomerAddressDto, CustomerAddressUpdateViewModel>();
        CreateMap<CustomerAddressUpdateViewModel, CustomerAddressUpdateDto>();
        CreateMap<CustomerAddressCreateViewModel, CustomerAddressCreateDto>();

        CreateMap<CountryDto, CountryUpdateViewModel>();
        CreateMap<CountryUpdateViewModel, CountryUpdateDto>();
        CreateMap<CountryCreateViewModel, CountryCreateDto>();

        CreateMap<ProvinceDto, ProvinceUpdateViewModel>();
        CreateMap<ProvinceUpdateViewModel, ProvinceUpdateDto>();
        CreateMap<ProvinceCreateViewModel, ProvinceCreateDto>();

        CreateMap<DistrictDto, DistrictUpdateViewModel>();
        CreateMap<DistrictUpdateViewModel, DistrictUpdateDto>();
        CreateMap<DistrictCreateViewModel, DistrictCreateDto>();

        CreateMap<ClinicDto, ClinicUpdateViewModel>();
        CreateMap<ClinicUpdateViewModel, ClinicUpdateDto>();
        CreateMap<ClinicCreateViewModel, ClinicCreateDto>();

        CreateMap<VisitDto, VisitUpdateViewModel>();
        CreateMap<VisitUpdateViewModel, VisitUpdateDto>();
        CreateMap<VisitCreateViewModel, VisitCreateDto>();

        CreateMap<VisitDailyActionDto, VisitDailyActionUpdateViewModel>();
        CreateMap<VisitDailyActionUpdateViewModel, VisitDailyActionUpdateDto>();
        CreateMap<VisitDailyActionCreateViewModel, VisitDailyActionCreateDto>();

        CreateMap<CompanyCalendarDto, CompanyCalendarUpdateViewModel>();
        CreateMap<CompanyCalendarUpdateViewModel, CompanyCalendarUpdateDto>();
        CreateMap<CompanyCalendarCreateViewModel, CompanyCalendarCreateDto>();
    }
}