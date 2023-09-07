using ZboxOrleans.Grains.Interfaces;

namespace ZboxOrleans.Grains;

// 6. Bezstavový grain (stateless): Vytvořte bezstavový grain, který má maximální propustnost v možnostech volání metod grainu.
public class StatelessGrain : IStatelessGrain
{
    public Task<DateTime> GetCurrentDateTime()
    {
        return Task.FromResult(DateTime.Now);
    }
}