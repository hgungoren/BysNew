using ToksozBysNew.Companies;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using ToksozBysNew.Departments;

namespace ToksozBysNew.Departments
{
    public class DepartmentsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly CompaniesDataSeedContributor _companiesDataSeedContributor;

        public DepartmentsDataSeedContributor(IDepartmentRepository departmentRepository, IUnitOfWorkManager unitOfWorkManager, CompaniesDataSeedContributor companiesDataSeedContributor)
        {
            _departmentRepository = departmentRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _companiesDataSeedContributor = companiesDataSeedContributor;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _companiesDataSeedContributor.SeedAsync(context);

            await _departmentRepository.InsertAsync(new Department
            (
                id: Guid.Parse("1b174f96-2c84-4616-8d5d-7469f680b366"),
                departmentName: "dad2b7006d0844fba78472320cc6cc5475009371fbed43c98b2cc9fedee0c78621f1163dced44825ae66566cce58f2c96eba7dc1724647f286778046c435c8cd009745c08503479593263f",
                companyId: null
            ));

            await _departmentRepository.InsertAsync(new Department
            (
                id: Guid.Parse("e033e52b-c1fa-4979-b824-3804502bb809"),
                departmentName: "d91fb687809c49c4a87143d119338c3b5abb580dac5246a289adf14fa0ba72dc5b5e22b7faa247288bfe5375b77cb4adc305fd8eb47a4214bd8f3d180e16a8c577eed0e8937046de9b74b2",
                companyId: null
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}