using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ToksozBysNew.Data;
using Volo.Abp.DependencyInjection;

namespace ToksozBysNew.EntityFrameworkCore;

public class EntityFrameworkCoreToksozBysNewDbSchemaMigrator
    : IToksozBysNewDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreToksozBysNewDbSchemaMigrator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the ToksozBysNewDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<ToksozBysNewDbContext>()
            .Database
            .MigrateAsync();
    }
}
