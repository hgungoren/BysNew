using System.Collections.Generic;
using ToksozBysNew.Web.Bundling.Reporting.DocumentViewer;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.Modularity;

namespace ToksozBysNew.Web.Bundling.Reporting.DocumentDesigner
{
    [DependsOn(typeof(DevExpressDocumentViewerScriptContributor))]
    public class DevExpressDocumentDesignerScriptContributor : BundleContributor
    {
        public override void ConfigureBundle(BundleConfigurationContext context)
        {
            context.Files.AddIfNotContains("/libs/devexpress-analytics-core/js/dx-querybuilder.min.js"); 
            context.Files.AddIfNotContains("/libs/devexpress-reporting/js/dx-reportdesigner.min.js");
        }
    }
}
