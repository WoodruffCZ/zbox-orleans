namespace ZboxOrleans.Grains.Interfaces;

public interface IStatelessGrain : IGrainWithGuidKey
{
    Task<DateTime> GetCurrentDateTime();
}