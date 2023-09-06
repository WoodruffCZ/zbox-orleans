namespace ZboxOrleans.Grains;

public interface IStatefulGrain : IGrainWithGuidKey, IDisposable
{
    Task SetValue(string value);
    Task<string?> GetValue();
}