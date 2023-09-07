using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ZboxOrleans.IntegrationTests.Fixtures;

namespace ZboxOrleans.IntegrationTests;

public class TestBase 
{
    public IHost Host { get; private set; }
    public IGrainFactory GrainFactory { get; private set; }
    
    public TestBase(OrleansApplicationFactory factory)
    {
        Host = factory.Host;
        GrainFactory = factory.Host.Services.GetRequiredService<IGrainFactory>();
    }
}