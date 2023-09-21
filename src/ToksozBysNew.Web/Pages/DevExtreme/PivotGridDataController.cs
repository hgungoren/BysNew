using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ToksozBysNew.ExpenseMonthlies;

namespace ToksozBysNew.Web.Pages.DevExtreme
{ 
    public class PivotGridDataController : Controller
    {
        ExpenseMonthliesAppService _appService;

        public PivotGridDataController(ExpenseMonthliesAppService appService)
        {
            _appService = appService;
        }


        [HttpGet]
        public object Get(DataSourceLoadOptions loadOptions)
        {
            var res = _appService.GetExpensesByMonth();
            var json = JsonConvert.SerializeObject(res, Formatting.Indented,
        new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        });

            return json;
        }
    }
}
