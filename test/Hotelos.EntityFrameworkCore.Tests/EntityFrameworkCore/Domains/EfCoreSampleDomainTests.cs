using Hotelos.Samples;
using Xunit;

namespace Hotelos.EntityFrameworkCore.Domains;

[Collection(HotelosTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<HotelosEntityFrameworkCoreTestModule>
{

}
