using Orleans.Providers;
using Orleans.Runtime;

namespace ZboxOrleans.Grains;

[StorageProvider(ProviderName = Globals.BlobStorageProviderName)]
public class PersistedStatefulGrain : Grain<PrimaryGrainState>, IPersistedStatefulGrain, IGrainBase
{
    public IGrainContext GrainContext { get; }
    
    public PersistedStatefulGrain(IGrainContext grainContext)
    {
        GrainContext = grainContext;
    }
    
    public async Task SetValue(string value)
    {
        State.Value = value;
        await WriteStateAsync();
    }

    public Task<string?> GetValue()
    {
        return Task.FromResult(State.Value);
    }

    public void Dispose()
    {
        
    }
}