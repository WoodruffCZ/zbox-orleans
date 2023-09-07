using FluentAssertions;
using ZboxOrleans.Grains.Interfaces;
using ZboxOrleans.IntegrationTests.Fixtures;

namespace ZboxOrleans.IntegrationTests;

[Collection(nameof(OrleansCollection))]
public class ReminderGrainTests : TestBase
{
    public ReminderGrainTests(OrleansApplicationFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Grain_Reminded()
    {
        var grainPrimaryKey = Guid.NewGuid();

        var reminderGrain = GrainFactory.GetGrain<IReminderGrain>(grainPrimaryKey);

        (await reminderGrain.RemindCalled()).Should().BeFalse();
        
        await Task.Delay(TimeSpan.FromSeconds(70));
        
        (await reminderGrain.RemindCalled()).Should().BeTrue();

        await reminderGrain.UnregisterReminder();
    }
}