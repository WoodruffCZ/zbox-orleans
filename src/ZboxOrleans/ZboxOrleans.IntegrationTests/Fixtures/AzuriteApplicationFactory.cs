using DotNet.Testcontainers.Builders;
using Microsoft.Extensions.Hosting;
using Testcontainers.Azurite;

namespace ZboxOrleans.IntegrationTests.Fixtures;

public class AzuriteApplicationFactory : IAsyncLifetime
{
    public IHost Host { get; }
    
    private AzuriteContainer? _azuriteContainer;
    
    public AzuriteApplicationFactory()
    {
        Host = Program.CreateHostBuilder(Array.Empty<string>()).Build();
        RunAzurite();
    }

    public async Task InitializeAsync()
    {
        await Host.StartAsync();
    }
    
    private async void RunAzurite()
    {
        // Remove if running or exists
        var azuriteContainersBuilder = new AzuriteBuilder()
            .WithImage("mcr.microsoft.com/azure-storage/azurite")
            .WithName("queueManager_azurite")
            .WithPortBinding(AzuriteBuilder.BlobPort,AzuriteBuilder.BlobPort)
            .WithPortBinding(AzuriteBuilder.TablePort,AzuriteBuilder.TablePort)
            .WithWaitStrategy(Wait.ForUnixContainer()
                .UntilPortIsAvailable(AzuriteBuilder.BlobPort)
                .UntilPortIsAvailable(AzuriteBuilder.TablePort))
            .WithAutoRemove(true)
            .WithCleanUp(true);
        
        _azuriteContainer = azuriteContainersBuilder.Build();
        await _azuriteContainer.StartAsync();
    }
    
    async Task IAsyncLifetime.DisposeAsync()
    {
        await Host.StopAsync();
        Host.Dispose();
        
        if (_azuriteContainer != null)
        {
            await _azuriteContainer.DisposeAsync();
        }
    }
}