using Volo.Abp.Modularity;

namespace Hotelos;

[DependsOn(
    typeof(HotelosDomainModule),
    typeof(HotelosTestBaseModule)
)]
public class HotelosDomainTestModule : AbpModule
{

}
