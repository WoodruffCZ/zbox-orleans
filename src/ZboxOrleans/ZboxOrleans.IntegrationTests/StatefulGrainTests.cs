using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using ZboxOrleans.Grains;

namespace ZboxOrleans.IntegrationTests;

public class StatefulGrainTests
{
    [Fact]
    public async Task Value_Is_Persisted()
    {
        var host = Program.CreateHostBuilder(Array.Empty<string>()).Build();

        await host.StartAsync();

        var client = host.Services.GetRequiredService<IGrainFactory>();

        var grainPrimaryKey = Guid.NewGuid();

        var statefulGrain = client.GetGrain<IStatefulGrain>(grainPrimaryKey);

        var expected = "some value";
        await statefulGrain.SetValue("some value");
        
        statefulGrain.Dispose();
        
        statefulGrain = client.GetGrain<IStatefulGrain>(grainPrimaryKey);
        
        var result = await statefulGrain.GetValue();

        result.Should().Be(expected);
        
        await host.StopAsync();
    }
}