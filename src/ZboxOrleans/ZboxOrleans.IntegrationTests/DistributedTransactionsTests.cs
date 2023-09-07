using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Orleans.Transactions;
using ZboxOrleans.Grains.Interfaces;

namespace ZboxOrleans.IntegrationTests;

public class DistributedTransactionsTests : TestBase
{
    [Fact]
    public async Task TransactionSuccessful()
    {
       var client1Key = Guid.NewGuid();
        var client2Key = Guid.NewGuid();
        
        var bankAccountTransferGrain = GrainFactory.GetGrain<IBankAccountTransferGrain>(Guid.NewGuid());

        await bankAccountTransferGrain.Transfer(client1Key, client2Key, 15);
        
        var client1BankAccountGrain = GrainFactory.GetGrain<IBankAccountGrain>(client1Key);
        var client2BankAccountGrain = GrainFactory.GetGrain<IBankAccountGrain>(client2Key);

        var client1Balance = await client1BankAccountGrain.GetCurrentBalance();
        var client2Balance = await client2BankAccountGrain.GetCurrentBalance();

        client1Balance.Should().Be(85);
        client2Balance.Should().Be(115);
    }
    
    [Fact]
    public async Task Transaction_Not_Successful()
    {
        var client = Host.Services.GetRequiredService<IGrainFactory>();

        var client1Key = Guid.NewGuid();
        var client2Key = Guid.NewGuid();
        
        var bankAccountTransferGrain = client.GetGrain<IBankAccountTransferGrain>(Guid.NewGuid());

        var transferFunc = async ()=> await bankAccountTransferGrain.Transfer(client1Key, client2Key, 9999);
        await transferFunc.Should().ThrowAsync<OrleansTransactionAbortedException>();
        
        var client1BankAccountGrain = client.GetGrain<IBankAccountGrain>(client1Key);
        var client2BankAccountGrain = client.GetGrain<IBankAccountGrain>(client2Key);

        var client1Balance = await client1BankAccountGrain.GetCurrentBalance();
        var client2Balance = await client2BankAccountGrain.GetCurrentBalance();

        client1Balance.Should().Be(100);
        client2Balance.Should().Be(100);
    }
}