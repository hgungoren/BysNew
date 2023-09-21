using AutoMapper.Internal.Mappers;
using Jint.Native;
using Jint.Runtime.Interop;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using ToksozBysNew.InvoiceDetails; 
using Volo.Abp.ObjectMapping;
using static ToksozBysNew.Permissions.ToksozBysNewPermissions;

namespace ToksozBysNew.Web.Pages.Invoices
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceDetailController : ControllerBase
    {
        private readonly IInvoiceDetailsAppService _invoiceDetailssAppService;

        public InvoiceDetailController(IInvoiceDetailsAppService invoiceDetailsAppService)
        {
            _invoiceDetailssAppService= invoiceDetailsAppService;
        }

        [HttpPut]
        public IActionResult UpdateInvoiceDetail(string Id, string values)
        { 
              var id=Guid.Parse(Id);
            var order = _invoiceDetailssAppService.UpdateDetailAsync(id,values);
              
            return Ok(order);
        }
    }
}
