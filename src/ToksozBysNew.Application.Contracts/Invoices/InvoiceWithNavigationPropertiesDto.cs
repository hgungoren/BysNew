using ToksozBysNew.TaxLists;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace ToksozBysNew.Invoices
{
    public class InvoiceWithNavigationPropertiesDto
    {
        public InvoiceDto Invoice { get; set; }

        public TaxListDto TaxList { get; set; }

    }
}