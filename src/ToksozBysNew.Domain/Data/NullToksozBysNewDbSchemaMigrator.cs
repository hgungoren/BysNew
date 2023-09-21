using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace ToksozBysNew.Data;

/* This is used if database provider does't define
 * IToksozBysNewDbSchemaMigrator implementation.
 */
public class NullToksozBysNewDbSchemaMigrator : IToksozBysNewDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
