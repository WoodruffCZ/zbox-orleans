using Orleans.Providers;
using Orleans.Runtime;
using ZboxOrleans.Grains.Interfaces;
using ZboxOrleans.GrainStates;

namespace ZboxOrleans.Grains;

// 4. Stavový grain (s persistencí): Implementujte stavový grain, který svůj stav persistuje. Můžete využít Azure CosmosDB nebo Azure Blob Storage pro persistenci stavu grainu.
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