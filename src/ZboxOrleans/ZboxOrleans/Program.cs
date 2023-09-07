using Microsoft.Extensions.Hosting;

namespace ZboxOrleans;

//1. Orleans Silo: Nastavte prostředí pro Orleans Silo. Toto je základní krok, který je nezbytný pro další práci s Orleans.
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
                .UseDashboard(options => { options.HostSelf = true; }) //Orleans Dashboard: Integrujte svůj projekt s Orleans Dashboard pro lepší monitorování a ladění.
                .UseLocalhostClustering()
                .AddMemoryGrainStorage(Globals.InMemoryStorageProviderName)
                .AddAzureBlobGrainStorage(Globals.BlobStorageProviderName, options =>
                {
                    options.ContainerName = "zboxblob";
                    options.ConfigureBlobServiceClient("DefaultEndpointsProtocol=http;AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1;QueueEndpoint=http://127.0.0.1:10001/devstoreaccount1;TableEndpoint=http://127.0.0.1:10002/devstoreaccount1;");
                })
                .AddAzureTableTransactionalStateStorageAsDefault(options => {
                    options.TableName = "zboxtable";
                    options.ConfigureTableServiceClient("DefaultEndpointsProtocol=http;AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1;QueueEndpoint=http://127.0.0.1:10001/devstoreaccount1;TableEndpoint=http://127.0.0.1:10002/devstoreaccount1;");
                })
                .UseInMemoryReminderService()
                .UseTransactions()
                .Configure<HostOptions>(s => s.ShutdownTimeout = TimeSpan.FromSeconds(1));;
            });
    }
}