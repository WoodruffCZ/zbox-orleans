namespace ZboxOrleans.Grains.Interfaces;

public interface IReminderGrain : IGrainWithGuidKey
{
    Task<bool> RemindCalled();
    Task UnregisterReminder();
}