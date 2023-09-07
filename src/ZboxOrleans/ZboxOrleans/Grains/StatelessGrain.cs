namespace ZboxOrleans.Grains;

public class StatelessGrain : IStatelessGrain
{
    public Task<DateTime> GetCurrentDateTime()
    {
        return Task.FromResult(DateTime.Now);
    }
}