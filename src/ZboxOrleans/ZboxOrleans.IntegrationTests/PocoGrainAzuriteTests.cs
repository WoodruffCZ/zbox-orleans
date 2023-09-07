using FluentAssertions;
using ZboxOrleans.Grains.Interfaces;
using ZboxOrleans.IntegrationTests.Fixtures;

namespace ZboxOrleans.IntegrationTests;

[Collection(nameof(AzuriteCollection))]
public class PocoGrainAzuriteTests : AzuriteTestBase
{
    public PocoGrainAzuriteTests(AzuriteApplicationFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Sent_PocoGrainId_Equals_Returned()
    {
        var grainPrimaryKey = 123;
        
        var pocoGrain = GrainFactory.GetGrain<IPocoGrain>(grainPrimaryKey);

        var result = await pocoGrain.GetCalledId();
        result.Should().Be(grainPrimaryKey);
    }
}