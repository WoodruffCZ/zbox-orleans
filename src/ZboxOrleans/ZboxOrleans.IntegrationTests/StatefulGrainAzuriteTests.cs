using FluentAssertions;
using ZboxOrleans.Grains.Interfaces;
using ZboxOrleans.IntegrationTests.Fixtures;

namespace ZboxOrleans.IntegrationTests;

[Collection(nameof(AzuriteCollection))]
public class StatefulGrainAzuriteTests : AzuriteTestBase
{
    public StatefulGrainAzuriteTests(AzuriteApplicationFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Value_Is_Persisted()
    {
        var grainPrimaryKey = Guid.NewGuid();

        var statefulGrain = GrainFactory.GetGrain<IStatefulGrain>(grainPrimaryKey);

        var expected = "some value";
        await statefulGrain.SetValue("some value");
        
        statefulGrain.Dispose();
        
        statefulGrain = GrainFactory.GetGrain<IStatefulGrain>(grainPrimaryKey);
        
        var result = await statefulGrain.GetValue();

        result.Should().Be(expected);
    }
}