namespace ZboxOrleans.Grains;

public interface IPersistedStatefulGrain : IGrainWithGuidKey, IDisposable
{
    Task SetValue(string value);
    Task<string?> GetValue();
}