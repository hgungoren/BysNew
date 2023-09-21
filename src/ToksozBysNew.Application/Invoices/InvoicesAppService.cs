using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using ToksozBysNew.Permissions;
using ToksozBysNew.Invoices;
using Volo.Abp.Content;
using ToksozBysNew.Shared;
using Microsoft.Extensions.Caching.Distributed;
using Volo.Abp.Caching;
using MiniExcelLibs;
using Volo.Abp.Authorization;

namespace ToksozBysNew.Invoices
{

    [Authorize(ToksozBysNewPermissions.Invoices.Default)]
    public class InvoicesAppService : ApplicationService, IInvoicesAppService
    {
        private readonly IDistributedCache<InvoiceExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly InvoiceManager _invoiceManager;

        public InvoicesAppService(IInvoiceRepository invoiceRepository, InvoiceManager invoiceManager, IDistributedCache<InvoiceExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
        {

            _excelDownloadTokenCache = excelDownloadTokenCache;
            _invoiceRepository = invoiceRepository;
            _invoiceManager = invoiceManager;
        }

        public virtual async Task<PagedResultDto<InvoiceDto>> GetListAsync(GetInvoicesInput input)
        {
            var totalCount = await _invoiceRepository.GetCountAsync(input.FilterText, input.InvoiceSerialNo, input.InvoiceDateMin, input.InvoiceDateMax, input.Notes, input.PaymentDateMin, input.PaymentDateMax, input.AmountMin, input.AmountMax, input.ApprovalStatusMin, input.ApprovalStatusMax);
            var items = await _invoiceRepository.GetListAsync(input.FilterText, input.InvoiceSerialNo, input.InvoiceDateMin, input.InvoiceDateMax, input.Notes, input.PaymentDateMin, input.PaymentDateMax, input.AmountMin, input.AmountMax, input.ApprovalStatusMin, input.ApprovalStatusMax, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<InvoiceDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Invoice>, List<InvoiceDto>>(items)
            };
        }

        public virtual async Task<InvoiceDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Invoice, InvoiceDto>(await _invoiceRepository.GetAsync(id));
        }

        [Authorize(ToksozBysNewPermissions.Invoices.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _invoiceRepository.DeleteAsync(id);
        }

        [Authorize(ToksozBysNewPermissions.Invoices.Create)]
        public virtual async Task<InvoiceDto> CreateAsync(InvoiceCreateDto input)
        {

            var invoice = await _invoiceManager.CreateAsync(
            input.InvoiceSerialNo, input.InvoiceDate, input.Notes, input.PaymentDate, input.Amount, input.ApprovalStatus
            );

            return ObjectMapper.Map<Invoice, InvoiceDto>(invoice);
        }

        [Authorize(ToksozBysNewPermissions.Invoices.Edit)]
        public virtual async Task<InvoiceDto> UpdateAsync(Guid id, InvoiceUpdateDto input)
        {

            var invoice = await _invoiceManager.UpdateAsync(
            id,
            input.InvoiceSerialNo, input.InvoiceDate, input.Notes, input.PaymentDate, input.Amount, input.ApprovalStatus, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<Invoice, InvoiceDto>(invoice);
        }

        public async Task<Guid> CreateAndGetIdAsync(InvoiceCreateDto input)
        {
            var invoice = await _invoiceManager.CreateAsync(
           input.InvoiceSerialNo, input.InvoiceDate, input.Notes, input.PaymentDate, input.Amount
           );
            await UnitOfWorkManager.Current.SaveChangesAsync();

            return invoice.Id;
        }

        public async Task<IRemoteStreamContent> GetListAsExcelFileAsync(InvoiceExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _invoiceRepository.GetListAsync(input.FilterText, input.InvoiceSerialNo, input.InvoiceDateMin, input.InvoiceDateMax, input.Notes, input.PaymentDateMin, input.PaymentDateMax, input.AmountMin, input.AmountMax);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<Invoice>, List<InvoiceExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "Invoices.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new InvoiceExcelDownloadTokenCacheItem { Token = token },
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
                });

            return new DownloadTokenResultDto
            {
                Token = token
            };
        }
    }
}