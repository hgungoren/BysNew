using ToksozBysNew.Shared;
using ToksozBysNew.TaxLists;
using ToksozBysNew.Invoices;
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
using ToksozBysNew.InvoiceDetails;

namespace ToksozBysNew.InvoiceDetails
{

    [Authorize(ToksozBysNewPermissions.InvoiceDetails.Default)]
    public class InvoiceDetailsAppService : ApplicationService, IInvoiceDetailsAppService
    {

        private readonly IInvoiceDetailRepository _invoiceDetailRepository;
        private readonly InvoiceDetailManager _invoiceDetailManager;
        private readonly IRepository<Invoice, Guid> _invoiceRepository;
        private readonly IRepository<TaxList, Guid> _taxListRepository;

        public InvoiceDetailsAppService(IInvoiceDetailRepository invoiceDetailRepository, InvoiceDetailManager invoiceDetailManager, IRepository<Invoice, Guid> invoiceRepository, IRepository<TaxList, Guid> taxListRepository)
        {

            _invoiceDetailRepository = invoiceDetailRepository;
            _invoiceDetailManager = invoiceDetailManager; _invoiceRepository = invoiceRepository;
            _taxListRepository = taxListRepository;
        }

        public virtual async Task<PagedResultDto<InvoiceDetailWithNavigationPropertiesDto>> GetListAsync(GetInvoiceDetailsInput input)
        {
            var totalCount = await _invoiceDetailRepository.GetCountAsync(input.FilterText, input.InvoiceDetailQuantityMin, input.InvoiceDetailQuantityMax, input.InvoiceDetailPriceMin, input.InvoiceDetailPriceMax, input.InvoiceDetailNote, input.InvoiceDetailDateMin, input.InvoiceDetailDateMax, input.Tax,  input.TaxName, input.InvoiceId, input.TaxListId);
            var items = await _invoiceDetailRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.InvoiceDetailQuantityMin, input.InvoiceDetailQuantityMax, input.InvoiceDetailPriceMin, input.InvoiceDetailPriceMax, input.InvoiceDetailNote, input.InvoiceDetailDateMin, input.InvoiceDetailDateMax, input.Tax, input.TaxName, input.InvoiceId, input.TaxListId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<InvoiceDetailWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<InvoiceDetailWithNavigationProperties>, List<InvoiceDetailWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<InvoiceDetailWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<InvoiceDetailWithNavigationProperties, InvoiceDetailWithNavigationPropertiesDto>
                (await _invoiceDetailRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<InvoiceDetailDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<InvoiceDetail, InvoiceDetailDto>(await _invoiceDetailRepository.GetAsync(id));
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetInvoiceLookupAsync(LookupRequestDto input)
        {
            var query = (await _invoiceRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.InvoiceSerialNo != null &&
                         x.InvoiceSerialNo.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<Invoice>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Invoice>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetTaxListLookupAsync(LookupRequestDto input)
        {
            var query = (await _taxListRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.TaxName != null &&
                         x.TaxName.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<TaxList>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<TaxList>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        [Authorize(ToksozBysNewPermissions.InvoiceDetails.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _invoiceDetailRepository.DeleteAsync(id);
        }

        [Authorize(ToksozBysNewPermissions.InvoiceDetails.Create)]
        public virtual async Task<InvoiceDetailDto> CreateAsync(InvoiceDetailCreateDto input)
        {
            if (input.TaxListId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["TaxList"]]);
            }

            var invoiceDetail = await _invoiceDetailManager.CreateAsync(
            input.InvoiceId, input.TaxListId, input.InvoiceDetailQuantity, input.InvoiceDetailPrice, input.InvoiceDetailNote, input.InvoiceDetailDate, input.TaxName, input.Tax
            );

            return ObjectMapper.Map<InvoiceDetail, InvoiceDetailDto>(invoiceDetail);
        }

        [Authorize(ToksozBysNewPermissions.InvoiceDetails.Edit)]
        public virtual async Task<InvoiceDetailDto> UpdateAsync(Guid id, InvoiceDetailUpdateDto input)
        {
            if (input.TaxListId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["TaxList"]]);
            }

            var invoiceDetail = await _invoiceDetailManager.UpdateAsync(
            id,
            input.InvoiceId, input.TaxListId, input.InvoiceDetailQuantity, input.InvoiceDetailPrice, input.InvoiceDetailNote, input.InvoiceDetailDate, input.TaxName, input.Tax, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<InvoiceDetail, InvoiceDetailDto>(invoiceDetail);
        }

        [Authorize(ToksozBysNewPermissions.InvoiceDetails.Edit)]
        public virtual async Task<InvoiceDetailDto> UpdateDetailAsync(Guid id, string inputDetail)
        {
            var input= ObjectMapper.Map<string,InvoiceDetail>(inputDetail);
             

            var invoiceDetail = await _invoiceDetailManager.UpdateAsync(
            id,
            input.InvoiceId, input.TaxListId, input.InvoiceDetailQuantity, input.InvoiceDetailPrice, input.InvoiceDetailNote, input.InvoiceDetailDate, input.TaxName, input.Tax, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<InvoiceDetail, InvoiceDetailDto>(invoiceDetail);
        }

        public async Task<PagedResultDto<InvoiceDetailDto>> GetListByInvoiceId(Guid id)
        {
            var query = (await _invoiceDetailRepository.GetQueryableAsync())
            .Where(x => x.InvoiceId.Equals(id)); var lookupData = query.ToList();
            var totalCount = query.Count();
            return new PagedResultDto<InvoiceDetailDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<InvoiceDetail>, List<InvoiceDetailDto>>(lookupData)
            };
        }
    }
}