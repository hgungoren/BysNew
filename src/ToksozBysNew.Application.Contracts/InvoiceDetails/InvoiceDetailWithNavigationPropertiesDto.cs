using ToksozBysNew.Invoices;
using ToksozBysNew.TaxLists;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace ToksozBysNew.InvoiceDetails
{
    public class InvoiceDetailWithNavigationPropertiesDto
    {
        public InvoiceDetailDto InvoiceDetail { get; set; }

        public InvoiceDto Invoice { get; set; }
        public TaxListDto TaxList { get; set; }

    }
}