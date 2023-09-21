using ToksozBysNew.Invoices;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using ToksozBysNew.InvoiceDetails;

namespace ToksozBysNew.InvoiceDetails
{
    public class InvoiceDetailsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IInvoiceDetailRepository _invoiceDetailRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly InvoicesDataSeedContributor _invoicesDataSeedContributor;

        public InvoiceDetailsDataSeedContributor(IInvoiceDetailRepository invoiceDetailRepository, IUnitOfWorkManager unitOfWorkManager, InvoicesDataSeedContributor invoicesDataSeedContributor)
        {
            _invoiceDetailRepository = invoiceDetailRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _invoicesDataSeedContributor = invoicesDataSeedContributor;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _invoicesDataSeedContributor.SeedAsync(context);

             

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}