namespace ZboxOrleans.Grains;

public interface IStatelessGrain : IGrainWithGuidKey
{
    Task<DateTime> GetCurrentDateTime();
}