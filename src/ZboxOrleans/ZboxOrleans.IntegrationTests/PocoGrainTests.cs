using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using ZboxOrleans.Grains;

namespace ZboxOrleans.IntegrationTests;

public class PocoGrainTests
{
    [Fact]
    public async Task Sent_PocoGrainId_Equals_Returned()
    {
        var host = Program.CreateHostBuilder(Array.Empty<string>()).Build();

        await host.StartAsync();

        var client = host.Services.GetRequiredService<IGrainFactory>();

        var grainPrimaryKey = 123;
        
        var pocoGrain = client.GetGrain<IPocoGrain>(grainPrimaryKey);

        var result = await pocoGrain.GetCalledId();
        result.Should().Be(grainPrimaryKey);

        await host.StopAsync();
    }
}