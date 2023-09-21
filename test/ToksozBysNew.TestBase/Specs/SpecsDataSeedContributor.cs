using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using ToksozBysNew.Specs;

namespace ToksozBysNew.Specs
{
    public class SpecsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly ISpecRepository _specRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public SpecsDataSeedContributor(ISpecRepository specRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _specRepository = specRepository;
            _unitOfWorkManager = unitOfWorkManager;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _specRepository.InsertAsync(new Spec
            (
                id: Guid.Parse("4ed69100-26c6-4519-9a70-9e52bcc90594"),
                specCode: "3a216249951a498d89245f0083b9483a3cc14dc43bb94540942cbbae0d3e22a",
                specName: "b2c7f92d559249869ba84c29be90e55e5"
            ));

            await _specRepository.InsertAsync(new Spec
            (
                id: Guid.Parse("6fef81ad-4ea7-4ffb-ac07-6b3c402a5661"),
                specCode: "3708cf618fcc4a56a9200c8858d6a3a8e5a6399422574e958610d67af4eaf19d02d68742922",
                specName: "cfb9397894c244e09f0377db03f8e2989099e2df45c2430fb"
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}