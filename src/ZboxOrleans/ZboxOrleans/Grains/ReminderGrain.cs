using Orleans.Runtime;
using Orleans.Timers;
using ZboxOrleans.Grains.Interfaces;

namespace ZboxOrleans.Grains;

public class ReminderGrain : IGrainBase, IReminderGrain, IRemindable
{
    private const string ReminderName = "MyReminder";
    
    public IGrainContext GrainContext { get; }
    private readonly IReminderRegistry _reminderRegistry;
    private IGrainReminder? _reminder;
    private bool _reminderCalled;
    
    public async Task OnActivateAsync(CancellationToken token)
    {
        _reminder = await _reminderRegistry.RegisterOrUpdateReminder(GrainContext.GrainId, ReminderName, TimeSpan.Zero, TimeSpan.FromMinutes(1));
    }

    public ReminderGrain(IGrainContext grainContext, IReminderRegistry reminderRegistry)
    {
        _reminderRegistry = reminderRegistry;
        GrainContext = grainContext;
    }

    public Task<bool> RemindCalled()
    {
        return Task.FromResult(_reminderCalled);
    }

    public async Task UnregisterReminder()
    {
        await _reminderRegistry.UnregisterReminder(GrainContext.GrainId, _reminder);
    }

    public Task ReceiveReminder(string reminderName, TickStatus status)
    {
        _reminderCalled = true;

        return Task.CompletedTask;
    }
}