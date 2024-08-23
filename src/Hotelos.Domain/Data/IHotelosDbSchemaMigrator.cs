using System.Threading.Tasks;

namespace Hotelos.Data;

public interface IHotelosDbSchemaMigrator
{
    Task MigrateAsync();
}
