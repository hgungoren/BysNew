using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using ToksozBysNew.Months;

namespace ToksozBysNew.Months
{
    public class MonthsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IMonthRepository _monthRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public MonthsDataSeedContributor(IMonthRepository monthRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _monthRepository = monthRepository;
            _unitOfWorkManager = unitOfWorkManager;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _monthRepository.InsertAsync(new Month
            (
                id: Guid.Parse("f8176482-db5a-4228-bb07-f9b45e7bf5bb"),
                name: "19b9434dbf7e4236af74730bcb4179c692b67f663813409884455b2707e882efe14414a49fbc4b9eba02746aaabde4f7e"
            ));

            await _monthRepository.InsertAsync(new Month
            (
                id: Guid.Parse("30e1f009-4785-4d03-909b-2d3c631032bd"),
                name: "3b64a7b1037c4cf293f5c9ab236fd722082da2bb6bb544f6aa6c39bb60f6193feaa7bd4ce70b4315"
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}