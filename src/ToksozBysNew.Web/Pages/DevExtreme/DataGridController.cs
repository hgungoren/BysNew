using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;
using static ToksozBysNew.Web.Pages.DevExtreme.ActiveCompanies;

namespace ToksozBysNew.Web.Pages.DevExtreme
{
    public class DataGridController : Controller
    {
        public ActionResult ExcelJSOverview()
        {
            return View(SampleData.DataGridEmployees.Take(10));
        }
        [HttpGet]
        public ActionResult GetData(DataSourceLoadOptions loadOptions)
        {
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(SampleData.DataGridEmployees, loadOptions)), "application/json");
        }
    }
}
