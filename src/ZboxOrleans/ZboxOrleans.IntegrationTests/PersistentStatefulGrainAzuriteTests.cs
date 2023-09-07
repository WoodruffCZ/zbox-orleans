using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using ZboxOrleans.Grains;
using ZboxOrleans.Grains.Interfaces;
using ZboxOrleans.IntegrationTests.Fixtures;

namespace ZboxOrleans.IntegrationTests;

[Collection(nameof(AzuriteCollection))]
public class PersistentStatefulGrainAzuriteTests : AzuriteTestBase
{
    public PersistentStatefulGrainAzuriteTests(AzuriteApplicationFactory factory) : base(factory)
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