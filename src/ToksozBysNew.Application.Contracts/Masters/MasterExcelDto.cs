using System;

namespace ToksozBysNew.Masters
{
    public class MasterExcelDto
    {
        public string InvoiceSerialNo { get; set; }
        public decimal InvoicePrice { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public string InvoiceNote { get; set; }
    }
}