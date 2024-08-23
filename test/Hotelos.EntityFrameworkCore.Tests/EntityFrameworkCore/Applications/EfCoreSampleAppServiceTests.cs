using Hotelos.Samples;
using Xunit;

namespace Hotelos.EntityFrameworkCore.Applications;

[Collection(HotelosTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<HotelosEntityFrameworkCoreTestModule>
{

}
