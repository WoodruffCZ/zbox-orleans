namespace ZboxOrleans.Grains;

public interface IPocoGrain : IGrainWithIntegerKey
{
    Task<long> GetCalledId();
}