using Orleans.Providers;
using Orleans.Runtime;
using ZboxOrleans.Grains.Interfaces;
using ZboxOrleans.GrainStates;

namespace ZboxOrleans.Grains;

// 3. Stavový grain (bez persistence): Vytvořte stavový grain, který udržuje stav mezi voláními, ale není persistován.
[StorageProvider(ProviderName = Globals.InMemoryStorageProviderName)]
public class StatefulGrain : Grain<PrimaryGrainState>, IStatefulGrain, IGrainBase
{
    public IGrainContext GrainContext { get; }
    
    public StatefulGrain(IGrainContext grainContext)
    {
        GrainContext = grainContext;
    }
    
    public Task SetValue(string value)
    {
        State.Value = value;
        return WriteStateAsync();
    }

    public Task<string?> GetValue()
    {
        return Task.FromResult(State.Value);
    }

    public void Dispose()
    {
        
    }
}