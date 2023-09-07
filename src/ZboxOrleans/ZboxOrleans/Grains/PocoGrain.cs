using Orleans.Runtime;
using ZboxOrleans.Grains.Interfaces;

namespace ZboxOrleans.Grains;

public class PocoGrain : IPocoGrain, IGrainBase
{
    public IGrainContext GrainContext { get; }
    
    public PocoGrain(IGrainContext grainContext)
    {
        GrainContext = grainContext;
    }

    public Task<long> GetCalledId()
    {
        return Task.FromResult(this.GetPrimaryKeyLong());
    }
}