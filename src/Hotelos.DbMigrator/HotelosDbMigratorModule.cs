using Hotelos.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Hotelos.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(HotelosEntityFrameworkCoreModule),
    typeof(HotelosApplicationContractsModule)
)]
public class HotelosDbMigratorModule : AbpModule
{
}
