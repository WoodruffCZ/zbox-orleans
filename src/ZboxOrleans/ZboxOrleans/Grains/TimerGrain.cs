using Orleans.Runtime;
using Orleans.Timers;
using ZboxOrleans.Grains.Interfaces;

namespace ZboxOrleans.Grains;

public class TimerGrain : IGrainBase, ITimerGrain
{
    private readonly ITimerRegistry _timerRegistry;
    public IGrainContext GrainContext { get; }

    private int _secondsFromLastCall;

    private IDisposable? _timer;

    public Task OnActivateAsync(CancellationToken token)
    {
        _timer = _timerRegistry.RegisterTimer(
            GrainContext,
            asyncCallback: static async state =>
            {
                var timerGrain = (TimerGrain)state;
                timerGrain._secondsFromLastCall++;
                
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

    public Task StopTimer()
    {
        _timer?.Dispose();
        return Task.CompletedTask;
    }
}