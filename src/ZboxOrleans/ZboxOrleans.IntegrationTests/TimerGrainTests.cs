using FluentAssertions;
using ZboxOrleans.Grains.Interfaces;
using ZboxOrleans.IntegrationTests.Fixtures;

namespace ZboxOrleans.IntegrationTests;

[Collection(nameof(AzuriteCollection))]
public class TimerGrainTests : AzuriteTestBase
{
    public TimerGrainTests(AzuriteApplicationFactory factory) : base(factory)
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


    }
}