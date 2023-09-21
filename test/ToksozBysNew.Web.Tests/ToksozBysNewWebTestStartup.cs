using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ToksozBysNew;

public class ToksozBysNewWebTestStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddApplication<ToksozBysNewWebTestModule>();
    }

    public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
    {
        app.InitializeApplication();
    }
}
