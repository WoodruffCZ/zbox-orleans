namespace ZboxOrleans.Grains;

public interface IMultipleFacetsStatefulGrain : IGrainWithGuidKey, IDisposable
{
    Task SetPrimaryValue(string value);
    Task SetSecondaryValue(string value);
    Task<string?> GetPrimaryValue();
    Task<string?> GetSecondaryValue();
}