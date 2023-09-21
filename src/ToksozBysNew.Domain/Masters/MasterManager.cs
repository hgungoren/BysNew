using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace ToksozBysNew.Masters
{
    public class MasterManager : DomainService
    {
        private readonly IMasterRepository _masterRepository;

        public MasterManager(IMasterRepository masterRepository)
        {
            _masterRepository = masterRepository;
        }

        public async Task<Master> CreateAsync(
        Guid? companyId, string invoiceSerialNo, decimal invoicePrice, string invoiceNote, DateTime? invoiceDate = null)
        {

            var master = new Master(
             GuidGenerator.Create(),
             companyId, invoiceSerialNo, invoicePrice, invoiceNote, invoiceDate
             );

            return await _masterRepository.InsertAsync(master);
        }

        public async Task<Master> UpdateAsync(
            Guid id,
            Guid? companyId, string invoiceSerialNo, decimal invoicePrice, string invoiceNote, DateTime? invoiceDate = null, [CanBeNull] string concurrencyStamp = null
        )
        {

            var master = await _masterRepository.GetAsync(id);

            master.CompanyId = companyId;
            master.InvoiceSerialNo = invoiceSerialNo;
            master.InvoicePrice = invoicePrice;
            master.InvoiceNote = invoiceNote;
            master.InvoiceDate = invoiceDate;

            master.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _masterRepository.UpdateAsync(master);
        }

    }
}