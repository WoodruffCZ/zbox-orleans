using Orleans.Runtime;
using ZboxOrleans.Grains.Interfaces;

namespace ZboxOrleans.Grains;

//2. POCO Grain: Vytvořte jednoduchý POCO grain, který neuchovává stav mezi voláními. POCO grainy v Orleans 7 nevyžadují dědění od třídy Grain.
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