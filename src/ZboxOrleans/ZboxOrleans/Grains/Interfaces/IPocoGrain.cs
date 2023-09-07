namespace ZboxOrleans.Grains.Interfaces;

public interface IPocoGrain : IGrainWithIntegerKey
{
    Task<long> GetCalledId();
}