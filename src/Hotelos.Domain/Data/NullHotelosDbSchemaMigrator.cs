using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Hotelos.Data;

/* This is used if database provider does't define
 * IHotelosDbSchemaMigrator implementation.
 */
public class NullHotelosDbSchemaMigrator : IHotelosDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
