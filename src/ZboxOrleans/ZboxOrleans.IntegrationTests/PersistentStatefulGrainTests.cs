using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using ZboxOrleans.Grains;

namespace ZboxOrleans.IntegrationTests;

public class PersistentStatefulGrainTests
{
    [Fact]
    public async Task Sent_PocoGrainId_Equals_Returned()
    {
        var host = Program.CreateHostBuilder(Array.Empty<string>()).Build();

        await host.StartAsync();

        var client = host.Services.GetRequiredService<IGrainFactory>();

        var grainPrimaryKey = Guid.NewGuid();

        var statefulGrain = client.GetGrain<IPersistedStatefulGrain>(grainPrimaryKey);

        var expected = "some value";
        await statefulGrain.SetValue("some value");
        
        statefulGrain.Dispose();
        
        statefulGrain = client.GetGrain<IPersistedStatefulGrain>(grainPrimaryKey);
        
        var result = await statefulGrain.GetValue();

        result.Should().Be(expected);
        
        await host.StopAsync();
    }
}