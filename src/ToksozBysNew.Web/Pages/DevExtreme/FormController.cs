using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;
using static ToksozBysNew.Web.Pages.DevExtreme.ActiveCompanies;

namespace ToksozBysNew.Web.Pages.DevExtreme
{
    public class FormController : Controller
    {
        public ActionResult Overview()
        {
            return View(SampleData1.ActiveCompanies.First());
        }

        [HttpGet]
        public ActionResult GetCompanies(DataSourceLoadOptions loadOptions)
        {
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(SampleData1.ActiveCompanies, loadOptions)), "application/json");
        }
    }
}
