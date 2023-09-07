using Microsoft.Extensions.Hosting;

namespace ZboxOrleans;

public class Program
{
    public static async Task Main(string[] args)
    {
        await CreateHostBuilder(args).RunConsoleAsync();
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .UseOrleans(siloBuilder => { siloBuilder
                .UseDashboard(options => { options.HostSelf = true; })
                .UseLocalhostClustering()
                .AddMemoryGrainStorage(Globals.InMemoryStorageProviderName)
                .AddAzureBlobGrainStorage(Globals.BlobStorageProviderName, options =>
                {
                    options.ContainerName = "zboxblob";
                    options.ConfigureBlobServiceClient("DefaultEndpointsProtocol=http;AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1;QueueEndpoint=http://127.0.0.1:10001/devstoreaccount1;TableEndpoint=http://127.0.0.1:10002/devstoreaccount1;");
                });
            });
    }
}