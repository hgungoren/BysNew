using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ToksozBysNew.Masters
{
    public class MasterCreateDto
    {
        public string InvoiceSerialNo { get; set; }
        public decimal InvoicePrice { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public string InvoiceNote { get; set; }
        public Guid? CompanyId { get; set; }
    }
}