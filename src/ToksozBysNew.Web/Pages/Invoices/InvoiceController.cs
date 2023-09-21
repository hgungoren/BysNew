using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using NetBox.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using ToksozBysNew.InvoiceDetails;
using Elsa.Services;

namespace ToksozBysNew.Web.Pages.Invoices
{
    public class InvoiceController : Controller
    {
        private readonly IInvoiceDetailsAppService _invoiceDetailssAppService;
        private readonly IWorkflowRunner _workflowRUnner;

        public InvoiceController(IInvoiceDetailsAppService invoiceDetailsAppService, IWorkflowRunner workflowRunner)
        {
            _invoiceDetailssAppService = invoiceDetailsAppService;
            _workflowRUnner = workflowRunner;
        }

        [HttpGet]
        public ActionResult GetData(DataSourceLoadOptions loadOptions,Guid id)
        { 
            var data = _invoiceDetailssAppService.GetListByInvoiceId(id).Result.Items.ToList();
             

            var res = GetEnumerable(id);

            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(res, loadOptions)), "application/json");
        }

        public IEnumerable<InvoiceDetailDto> GetEnumerable(Guid id)
        {
            List<InvoiceDetailDto> books = _invoiceDetailssAppService.GetListByInvoiceId(id).Result.Items.ToList();
            return books;
        }

        [HttpPut]
        public IActionResult UpdateInvoiceDetail(Guid Id, string values)
        { 
            var order = _invoiceDetailssAppService.UpdateDetailAsync(Id, values);

            return Ok(order);
        }
    }
}
