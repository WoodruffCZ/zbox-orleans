using System.Diagnostics;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Abstractions;
using ZboxOrleans.Grains;

namespace ZboxOrleans.IntegrationTests;

public class StatelessGrainTests
{
    private readonly ITestOutputHelper _testOutputHelper;

    public StatelessGrainTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public async Task PerformanceTest()
    {
        const int callsCount = 100000;
        var host = Program.CreateHostBuilder(Array.Empty<string>()).Build();

        await host.StartAsync();

        var client = host.Services.GetRequiredService<IGrainFactory>();


        var stopwatch = Stopwatch.StartNew();
        
        Parallel.For(1, callsCount, new ParallelOptions{MaxDegreeOfParallelism = 10000}, async i =>
        {
            var statelessGrain = client.GetGrain<IStatelessGrain>(Guid.NewGuid());
            var getTimeFunc = async ()=> await statelessGrain.GetCurrentDateTime();
            await getTimeFunc.Should().NotThrowAsync();
        });
        
        stopwatch.Stop();

        _testOutputHelper.WriteLine($"Performance was: {callsCount/stopwatch.Elapsed.TotalSeconds}/s");
        
        await host.StopAsync();
    }
}