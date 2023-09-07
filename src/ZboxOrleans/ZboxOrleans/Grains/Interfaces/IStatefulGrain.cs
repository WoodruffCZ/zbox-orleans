namespace ZboxOrleans.Grains.Interfaces;

public interface IStatefulGrain : IGrainWithGuidKey, IDisposable
{
    Task SetValue(string value);
    Task<string?> GetValue();
}