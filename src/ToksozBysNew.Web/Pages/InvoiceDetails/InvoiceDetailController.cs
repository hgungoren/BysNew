using DevExpress.ClipboardSource.SpreadsheetML;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using ToksozBysNew.InvoiceDetails;

namespace ToksozBysNew.Web.Pages.InvoiceDetails
{
    public class InvoiceDetailController : Controller
    {
        InvoiceDetailsAppService _invoiceDetailsAppService;
        public InvoiceDetailController(InvoiceDetailsAppService invoiceDetailsAppService)
        {
            _invoiceDetailsAppService = invoiceDetailsAppService;
        }
        [HttpGet]
        public ActionResult Get(Guid id)
        {
        //    var res = _invoiceDetailsAppService.GetDetailAsync(id);
        //    var json = JsonConvert.SerializeObject(res, Formatting.Indented,
        //new JsonSerializerSettings
        //{
        //    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        //});

        //    return Json(json);
        return View();
        }
    }
}
