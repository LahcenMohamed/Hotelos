using Volo.Abp.Modularity;

namespace Hotelos;

public abstract class HotelosApplicationTestBase<TStartupModule> : HotelosTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
