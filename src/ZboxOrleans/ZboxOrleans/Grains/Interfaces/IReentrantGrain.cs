namespace ZboxOrleans.Grains.Interfaces;

public interface IReentrantGrain : IGrainWithGuidKey
{
    Task<(DateTime startTime, long id, DateTime endTime)> ReturnSameValue(long id);
}