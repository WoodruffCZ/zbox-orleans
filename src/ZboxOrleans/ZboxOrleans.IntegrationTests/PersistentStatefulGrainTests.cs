using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using ZboxOrleans.Grains;
using ZboxOrleans.Grains.Interfaces;

namespace ZboxOrleans.IntegrationTests;

public class PersistentStatefulGrainTests : TestBase
{
    [Fact]
    public async Task Sent_PocoGrainId_Equals_Returned()
    {
        var grainPrimaryKey = Guid.NewGuid();

        var statefulGrain = GrainFactory.GetGrain<IPersistedStatefulGrain>(grainPrimaryKey);

        var expected = "some value";
        await statefulGrain.SetValue("some value");
        
        statefulGrain.Dispose();
        
        statefulGrain = GrainFactory.GetGrain<IPersistedStatefulGrain>(grainPrimaryKey);
        
        var result = await statefulGrain.GetValue();

        result.Should().Be(expected);
    }
}