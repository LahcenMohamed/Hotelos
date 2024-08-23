using Volo.Abp.Modularity;

namespace Hotelos;

[DependsOn(
    typeof(HotelosApplicationModule),
    typeof(HotelosDomainTestModule)
)]
public class HotelosApplicationTestModule : AbpModule
{

}
