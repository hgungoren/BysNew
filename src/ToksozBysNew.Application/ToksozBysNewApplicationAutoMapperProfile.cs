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
using Volo.Abp.AutoMapper;
using AutoMapper;
using System;
using ToksozBysNew.AccountGroups;
using ToksozBysNew.Accounts;
using ToksozBysNew.BudgetDistributions;
using ToksozBysNew.Budgets;
using ToksozBysNew.Companies;
using ToksozBysNew.Departments;
using ToksozBysNew.ExpenseMonthlies;
using ToksozBysNew.Months;
using ToksozBysNew.Products;
using ToksozBysNew.Shared;
using ToksozBysNew.SpendingGroups;
using Volo.Abp.Identity;

namespace ToksozBysNew;

public class ToksozBysNewApplicationAutoMapperProfile : Profile
{
    public ToksozBysNewApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<Company, CompanyDto>();
        CreateMap<Company, CompanyExcelDto>();

        CreateMap<AccountGroup, AccountGroupDto>();
        CreateMap<AccountGroup, AccountGroupExcelDto>();

        CreateMap<CompanyWithNavigationProperties, CompanyWithNavigationPropertiesDto>();
        CreateMap<AccountGroup, LookupDto<Guid?>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.AccountGroupName));

        CreateMap<Department, DepartmentDto>();
        CreateMap<Department, DepartmentExcelDto>();

        CreateMap<Account, AccountDto>();
        CreateMap<Account, AccountExcelDto>();

        CreateMap<Product, ProductDto>();
        CreateMap<Product, ProductExcelDto>();

        CreateMap<DepartmentWithNavigationProperties, DepartmentWithNavigationPropertiesDto>();
        CreateMap<Company, LookupDto<Guid?>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.CompanyName));

        CreateMap<Budget, BudgetDto>();
        CreateMap<Budget, BudgetExcelDto>();
        CreateMap<BudgetWithNavigationProperties, BudgetWithNavigationPropertiesDto>();

        CreateMap<BudgetDistribution, BudgetDistributionDto>();
        CreateMap<BudgetDistribution, BudgetDistributionExcelDto>();
        CreateMap<BudgetDistributionWithNavigationProperties, BudgetDistributionWithNavigationPropertiesDto>();
        CreateMap<Department, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.DepartmentName));
        CreateMap<Product, LookupDto<Guid?>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.ProductName));
        CreateMap<Budget, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.BudgetName));
        CreateMap<Account, LookupDto<Guid?>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.AccountName));
        CreateMap<IdentityUser, LookupDto<Guid?>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.UserName));
        CreateMap<Invoice, LookupDto<Guid?>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.InvoiceSerialNo));

        CreateMap<Month, MonthDto>();

        CreateMap<SpendingGroup, SpendingGroupDto>();

        CreateMap<ExpenseMonthly, ExpenseMonthlyDto>();
        CreateMap<ExpenseMonthly, ExpenseMonthlyExcelDto>();

        CreateMap<Invoice, InvoiceDto>();
        CreateMap<Invoice, InvoiceExcelDto>();

        CreateMap<InvoiceDetail, InvoiceDetailDto>();
        CreateMap<InvoiceDetail, InvoiceDetailExcelDto>();

        CreateMap<InvoiceDetailWithNavigationProperties, InvoiceDetailWithNavigationPropertiesDto>();
        CreateMap<Invoice, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.InvoiceSerialNo));

        CreateMap<Master, MasterDto>();
        CreateMap<Master, MasterExcelDto>();
        CreateMap<MasterWithNavigationProperties, MasterWithNavigationPropertiesDto>();
        CreateMap<Company, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.CompanyName));

        CreateMap<TaxList, TaxListDto>();

        CreateMap<InvoiceWithNavigationProperties, InvoiceWithNavigationPropertiesDto>();
        CreateMap<TaxList, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.TaxName));

        CreateMap<Doctor, DoctorDto>();
        CreateMap<Doctor, DoctorExcelDto>();

        CreateMap<Brick, BrickDto>();
        CreateMap<Brick, BrickExcelDto>();

        CreateMap<Position, PositionDto>();
        CreateMap<Position, PositionExcelDto>();

        CreateMap<Spec, SpecDto>();
        CreateMap<Spec, SpecExcelDto>();

        CreateMap<Unit, UnitDto>();
        CreateMap<Unit, UnitExcelDto>();

        CreateMap<BrickWithNavigationProperties, BrickWithNavigationPropertiesDto>();
        CreateMap<Unit, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.UnitName));

        CreateMap<SpecWithNavigationProperties, SpecWithNavigationPropertiesDto>();
        CreateMap<Doctor, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.NameSurname));

        CreateMap<DoctorWithNavigationProperties, DoctorWithNavigationPropertiesDto>();
        CreateMap<Position, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.PositionName));

        CreateMap<Spec, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.SpecName));

        CreateMap<CustomerTitle, CustomerTitleDto>();
        CreateMap<CustomerTitle, CustomerTitleExcelDto>();

        CreateMap<CustomerTitle, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.TitleName));

        CreateMap<UnitWithNavigationProperties, UnitWithNavigationPropertiesDto>();
        CreateMap<Brick, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.BrickName));

        CreateMap<CustomerType, CustomerTypeDto>();

        CreateMap<CustomerType, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.TypeName));

        CreateMap<CustomerAddress, CustomerAddressDto>();
        CreateMap<CustomerAddress, CustomerAddressExcelDto>();
        CreateMap<CustomerAddressWithNavigationProperties, CustomerAddressWithNavigationPropertiesDto>();

        CreateMap<Country, CountryDto>();
        CreateMap<Country, CountryExcelDto>();

        CreateMap<Province, ProvinceDto>();
        CreateMap<Province, ProvinceExcelDto>();
        CreateMap<ProvinceWithNavigationProperties, ProvinceWithNavigationPropertiesDto>();
        CreateMap<Country, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.CountryName));

        CreateMap<District, DistrictDto>();
        CreateMap<District, DistrictExcelDto>();
        CreateMap<DistrictWithNavigationProperties, DistrictWithNavigationPropertiesDto>();
        CreateMap<Province, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.ProvinceName));

        CreateMap<District, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.DistrictName));

        CreateMap<Clinic, ClinicDto>();
        CreateMap<Clinic, ClinicExcelDto>();
        CreateMap<ClinicWithNavigationProperties, ClinicWithNavigationPropertiesDto>();

        CreateMap<Visit, VisitDto>();
        CreateMap<Visit, VisitExcelDto>();
        CreateMap<VisitWithNavigationProperties, VisitWithNavigationPropertiesDto>();
        CreateMap<Clinic, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.ClinicName));

        CreateMap<IdentityUser, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Id));

        CreateMap<IdentityUser, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.UserName));

        CreateMap<VisitDailyAction, VisitDailyActionDto>();
        CreateMap<VisitDailyAction, VisitDailyActionExcelDto>();
        CreateMap<VisitDailyActionWithNavigationProperties, VisitDailyActionWithNavigationPropertiesDto>();

        CreateMap<CompanyCalendar, CompanyCalendarDto>();
        CreateMap<CompanyCalendar, CompanyCalendarExcelDto>();

        CreateMap<Spec, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.SpecCode));
    }
}