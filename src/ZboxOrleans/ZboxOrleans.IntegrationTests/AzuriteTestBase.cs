using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ZboxOrleans.IntegrationTests.Fixtures;

namespace ZboxOrleans.IntegrationTests;

public class AzuriteTestBase : IAsyncLifetime
{
    private readonly AzuriteApplicationFactory _factory;
    public IHost Host { get; private set; }
    public IGrainFactory? GrainFactory { get; private set; }
    
    public AzuriteTestBase(AzuriteApplicationFactory factory)
    {
        _factory = factory;
        Host = _factory.Host;
    }

    public Task InitializeAsync()
    {
        GrainFactory = _factory.Host.Services.GetRequiredService<IGrainFactory>();
        
        return Task.CompletedTask;
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }
}