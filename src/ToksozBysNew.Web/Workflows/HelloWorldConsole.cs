using Elsa.Activities.Console;
using Elsa.Builders;

namespace ToksozBysNew.Web.Workflows
{
    public class HelloWorldConsole : IWorkflow
    {
        public void Build(IWorkflowBuilder builder) => builder.WriteLine("Hello World from Elsa!");
    }
}