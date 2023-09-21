using ToksozBysNew.Invoices;
using ToksozBysNew.TaxLists;

using System;
using System.Collections.Generic;

namespace ToksozBysNew.InvoiceDetails
{
    public class InvoiceDetailWithNavigationProperties
    {
        public InvoiceDetail InvoiceDetail { get; set; }

        public Invoice Invoice { get; set; }
        public TaxList TaxList { get; set; }
        

        
    }
}