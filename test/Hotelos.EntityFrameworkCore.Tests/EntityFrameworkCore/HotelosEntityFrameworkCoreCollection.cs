using Xunit;

namespace Hotelos.EntityFrameworkCore;

[CollectionDefinition(HotelosTestConsts.CollectionDefinitionName)]
public class HotelosEntityFrameworkCoreCollection : ICollectionFixture<HotelosEntityFrameworkCoreFixture>
{

}
