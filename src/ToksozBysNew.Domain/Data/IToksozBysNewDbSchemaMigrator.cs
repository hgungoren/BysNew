using System.Threading.Tasks;

namespace ToksozBysNew.Data;

public interface IToksozBysNewDbSchemaMigrator
{
    Task MigrateAsync();
}
