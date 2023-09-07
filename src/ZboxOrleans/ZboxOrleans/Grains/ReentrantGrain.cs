using Orleans.Concurrency;
using ZboxOrleans.Grains.Interfaces;

namespace ZboxOrleans.Grains;

// 9. Reentrant Grains: Vytvořte grain, který demonstruje použití vlatnosti reentrant v Orleans.
[Reentrant]
public class ReentrantGrain : IReentrantGrain
{
    public async Task<(DateTime, long, DateTime)> ReturnSameValue(long id)
    {
        var startTime = DateTime.Now;
        await Task.Delay(1000);
        var endTime = DateTime.Now;

        return (startTime, id, endTime);
    }
}