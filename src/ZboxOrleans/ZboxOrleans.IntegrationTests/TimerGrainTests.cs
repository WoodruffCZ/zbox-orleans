using FluentAssertions;
using ZboxOrleans.Grains.Interfaces;
using ZboxOrleans.IntegrationTests.Fixtures;

namespace ZboxOrleans.IntegrationTests;

[Collection(nameof(OrleansCollection))]
public class TimerGrainTests : TestBase
{
    public TimerGrainTests(OrleansApplicationFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task TimerTicking()
    {
        var grainPrimaryKey = Guid.NewGuid();

        var timerGrain = GrainFactory.GetGrain<ITimerGrain>(grainPrimaryKey);

        await timerGrain.GetSecondsFromLastCall();
        
        await Task.Delay(TimeSpan.FromSeconds(3));
        
        var secondsFromLastCall = await timerGrain.GetSecondsFromLastCall();
        secondsFromLastCall.Should().BeGreaterOrEqualTo(3);

        await timerGrain.StopTimer();
    }
}