using Elsa.Activities.Console;
using Elsa.Activities.Http;
using Elsa.Activities.Signaling.Services;
using Elsa.Builders;
using System.Net;

namespace ToksozBysNew.Web.Workflows
{
    public class HelloWorldHttp:IWorkflow
    {
        public void Build(IWorkflowBuilder builder)
        {
            builder
                .HttpEndpoint("/hello-world")
                .WriteHttpResponse(HttpStatusCode.OK, "<h1>Hello World!</h1>", "text/html");
        }
    }
}
