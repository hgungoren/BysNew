using ToksozBysNew.TaxLists;

using System;
using System.Collections.Generic;

namespace ToksozBysNew.Invoices
{
    public class InvoiceWithNavigationProperties
    {
        public Invoice Invoice { get; set; }

        public TaxList TaxList { get; set; }
        

        
    }
}