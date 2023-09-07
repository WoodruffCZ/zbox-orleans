namespace ZboxOrleans.Grains.Interfaces;

public interface ITimerGrain : IGrainWithGuidKey
{
    Task<int> GetSecondsFromLastCall();
}