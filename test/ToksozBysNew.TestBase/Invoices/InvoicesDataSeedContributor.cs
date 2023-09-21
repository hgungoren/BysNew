using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using ToksozBysNew.Invoices;

namespace ToksozBysNew.Invoices
{
    public class InvoicesDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public InvoicesDataSeedContributor(IInvoiceRepository invoiceRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _invoiceRepository = invoiceRepository;
            _unitOfWorkManager = unitOfWorkManager;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _invoiceRepository.InsertAsync(new Invoice
            (
                id: Guid.Parse("d5f66ecf-55af-4878-b649-6d9bc1a5afc3"),
                invoiceSerialNo: "70c7dee2cab742a5b005dc0c71a5496231b20a7cc81f4f209408f470a9e40a54bd4f0e66adcc4160837849cdcf1e936",
                invoiceDate: new DateTime(2013, 10, 7),
                notes: "7e23ca6bd9b1435d9dfd7c1f964e8edecd40620d23d2424bbcfd40d12c5c6ff07e86b3b2211d4c46a4cea39e7d",
                paymentDate: new DateTime(2003, 3, 8),
                amount: 1263406507,
                approvalStatus: 168777838
            ));

            await _invoiceRepository.InsertAsync(new Invoice
            (
                id: Guid.Parse("50ada226-89e7-4026-9edb-f3d6e800bc74"),
                invoiceSerialNo: "0b12a20e202e4b8682d620acbdb0f3f19a26895dc19947c79d49324e82b0634dd70c51323b664182a9bc4ef307b9a6914",
                invoiceDate: new DateTime(2015, 8, 25),
                notes: "e8de158fbca0490c86e1b1c",
                paymentDate: new DateTime(2012, 7, 12),
                amount: 875211534,
                approvalStatus: 329160849
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}