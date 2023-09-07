using FluentAssertions;
using ZboxOrleans.Grains.Interfaces;
using ZboxOrleans.IntegrationTests.Fixtures;

namespace ZboxOrleans.IntegrationTests;

[Collection(nameof(OrleansCollection))]
public class PersistentStatefulGrainTests : TestBase
{
    public PersistentStatefulGrainTests(OrleansApplicationFactory factory) : base(factory)
    {
    }

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