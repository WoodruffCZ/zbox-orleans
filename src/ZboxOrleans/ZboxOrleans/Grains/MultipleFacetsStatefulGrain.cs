using Orleans.Runtime;
using ZboxOrleans.Grains.Interfaces;
using ZboxOrleans.GrainStates;

namespace ZboxOrleans.Grains;

public class MultipleFacetsStatefulGrain : IMultipleFacetsStatefulGrain
{
    private readonly IPersistentState<PrimaryGrainState> _primaryGrainState;
    private readonly IPersistentState<SecondaryGrainState> _secondaryGrainState;

    public MultipleFacetsStatefulGrain([PersistentState("primaryGrainState", Globals.InMemoryStorageProviderName)] IPersistentState<PrimaryGrainState> primaryGrainState,
        [PersistentState("secondaryGrainState", Globals.BlobStorageProviderName)] IPersistentState<SecondaryGrainState> secondaryGrainState)
    {
        _primaryGrainState = primaryGrainState;
        _secondaryGrainState = secondaryGrainState;
    }

    public async Task SetPrimaryValue(string value)
    {
        _primaryGrainState.State.Value = value;
        await _primaryGrainState.WriteStateAsync();
    }

    public async Task SetSecondaryValue(string value)
    {
        _secondaryGrainState.State.Value = value;
        await _secondaryGrainState.WriteStateAsync();
    }

    public Task<string?> GetPrimaryValue()
    {
        return Task.FromResult(_primaryGrainState.State.Value);
    }

    public Task<string?> GetSecondaryValue()
    {
        return Task.FromResult(_secondaryGrainState.State.Value);
    }
    
    public void Dispose()
    {   
        
    }
}