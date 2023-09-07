using FluentAssertions;
using ZboxOrleans.Grains.Interfaces;

namespace ZboxOrleans.IntegrationTests;

public class PocoGrainTests : TestBase
{
    [Fact]
    public async Task Sent_PocoGrainId_Equals_Returned()
    {
        var grainPrimaryKey = 123;
        
        var pocoGrain = GrainFactory.GetGrain<IPocoGrain>(grainPrimaryKey);

        var result = await pocoGrain.GetCalledId();
        result.Should().Be(grainPrimaryKey);
    }
}