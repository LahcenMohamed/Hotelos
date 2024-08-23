using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Hotelos.Data;
using Volo.Abp.DependencyInjection;

namespace Hotelos.EntityFrameworkCore;

public class EntityFrameworkCoreHotelosDbSchemaMigrator
    : IHotelosDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreHotelosDbSchemaMigrator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the HotelosDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<HotelosDbContext>()
            .Database
            .MigrateAsync();
    }
}
