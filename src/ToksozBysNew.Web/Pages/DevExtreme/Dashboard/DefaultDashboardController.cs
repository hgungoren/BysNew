using DevExpress.DashboardAspNetCore;
using DevExpress.DashboardCommon;
using DevExpress.DashboardWeb;
using DevExpress.DataAccess.Sql;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;

namespace ToksozBysNew.Web.Pages.DevExtreme.Dashboard
{
    public class DefaultDashboardController : DashboardController
    {
        public DefaultDashboardController(DashboardConfigurator configurator, IDataProtectionProvider? dataProtectionProvider = null)
            : base(configurator, dataProtectionProvider)
        {
        }
         
        public IActionResult Index()
        {
            return View();
        }
    }
}
