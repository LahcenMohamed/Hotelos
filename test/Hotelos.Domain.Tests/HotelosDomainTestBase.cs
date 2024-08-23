using Volo.Abp.Modularity;

namespace Hotelos;

/* Inherit from this class for your domain layer tests. */
public abstract class HotelosDomainTestBase<TStartupModule> : HotelosTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
