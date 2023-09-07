using System.Diagnostics;
using FluentAssertions;
using Xunit.Abstractions;
using ZboxOrleans.Grains.Interfaces;
using ZboxOrleans.IntegrationTests.Fixtures;

namespace ZboxOrleans.IntegrationTests;

[Collection(nameof(AzuriteCollection))]
public class StatelessGrainAzuriteTests : AzuriteTestBase
{
    private readonly ITestOutputHelper _testOutputHelper;

    public StatelessGrainAzuriteTests(AzuriteApplicationFactory factory, ITestOutputHelper testOutputHelper) : base(factory)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void PerformanceTest()
    {
        const int callsCount = 100000;

        var stopwatch = Stopwatch.StartNew();
        
        Parallel.For(1, callsCount, new ParallelOptions{MaxDegreeOfParallelism = 10000}, async i =>
        {
            var statelessGrain = GrainFactory.GetGrain<IStatelessGrain>(Guid.NewGuid());
            var getTimeFunc = async ()=> await statelessGrain.GetCurrentDateTime();
            await getTimeFunc.Should().NotThrowAsync();
        });
        
        stopwatch.Stop();

        _testOutputHelper.WriteLine($"Performance was: {callsCount/stopwatch.Elapsed.TotalSeconds}/s");
    }
}