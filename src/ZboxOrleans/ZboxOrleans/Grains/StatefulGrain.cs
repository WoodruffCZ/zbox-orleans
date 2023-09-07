using Orleans.Providers;
using Orleans.Runtime;
using ZboxOrleans.GrainStates;

namespace ZboxOrleans.Grains;

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
        return Task.CompletedTask;
    }

    public Task<string?> GetValue()
    {
        return Task.FromResult(State.Value);
    }

    public void Dispose()
    {
        
    }
}