using System.Diagnostics;
using FluentAssertions;
using Xunit.Abstractions;
using ZboxOrleans.Grains.Interfaces;
using ZboxOrleans.IntegrationTests.Fixtures;

namespace ZboxOrleans.IntegrationTests;

[Collection(nameof(OrleansCollection))]
public class ReentrantGrainTests  : TestBase
{
    private readonly ITestOutputHelper _testOutputHelper;

    public ReentrantGrainTests(OrleansApplicationFactory factory, ITestOutputHelper testOutputHelper) : base(factory)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public async Task Test()
    {
        const int callsCount = 30;
        
        var tasks = new List<Task<(DateTime startTime, long id, DateTime endTime)>>();

        var statelessGrain = GrainFactory.GetGrain<IReentrantGrain>(Guid.NewGuid());
        
        for (long i = 0; i < callsCount; i++)
        {
            
            tasks.Add(statelessGrain.ReturnSameValue(i));
        };

        var stopwatch = Stopwatch.StartNew();
        await Task.WhenAll(tasks);
        stopwatch.Stop();
        
        stopwatch.Elapsed.TotalSeconds.Should().BeLessThan(30);

        foreach (var task in tasks)
        {
            var result = await task;
            _testOutputHelper.WriteLine($"Id: {result.id} StartTime: {result.startTime}.{result.startTime.Millisecond} EndTime: {result.endTime}.{result.endTime.Millisecond}");
        }
        
    }
}