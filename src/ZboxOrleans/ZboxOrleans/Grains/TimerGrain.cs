using Orleans.Runtime;
using Orleans.Timers;
using ZboxOrleans.Grains.Interfaces;

namespace ZboxOrleans.Grains;

public class TimerGrain : IGrainBase, ITimerGrain, IDisposable
{
    private readonly ITimerRegistry _timerRegistry;
    public IGrainContext GrainContext { get; }

    private int _secondsFromLastCall;

    public Task OnActivateAsync(CancellationToken token)
    {
        _timerRegistry.RegisterTimer(
            GrainContext,
            asyncCallback: static async state =>
            {
                var qq = (TimerGrain)state;
                qq._secondsFromLastCall++;
                
                await Task.CompletedTask;
            },
            state: this,
            dueTime: TimeSpan.Zero,
            period: TimeSpan.FromSeconds(1));

        return Task.CompletedTask;
    }

    public TimerGrain(IGrainContext grainContext, ITimerRegistry timerRegistry)
    {
        _timerRegistry = timerRegistry;
        GrainContext = grainContext;
    }

    public Task<int> GetSecondsFromLastCall()
    {
        return Task.FromResult(_secondsFromLastCall);
    }

    public void Dispose()
    {
        
    }
}