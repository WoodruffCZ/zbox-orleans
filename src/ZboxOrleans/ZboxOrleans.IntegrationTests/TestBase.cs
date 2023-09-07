using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ZboxOrleans.IntegrationTests;

public class TestBase : IDisposable
{
    public IHost Host { get; }
    public IGrainFactory GrainFactory { get; }
    
    public TestBase()
    {
        Host = Program.CreateHostBuilder(Array.Empty<string>()).Build();
        
        Host.Start();
        
        GrainFactory = Host.Services.GetRequiredService<IGrainFactory>();
    }

    public void Dispose()
    {
        Host.Dispose();
    }
}