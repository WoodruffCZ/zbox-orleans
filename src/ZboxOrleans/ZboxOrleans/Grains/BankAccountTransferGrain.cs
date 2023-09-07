using Orleans.Concurrency;
using ZboxOrleans.Grains.Interfaces;

namespace ZboxOrleans.Grains;

[StatelessWorker]
public class BankAccountTransferGrain : Grain, IBankAccountTransferGrain
{
    public async Task Transfer(Guid fromAccountId, Guid toAccountId, decimal amountToTransfer)
    {
        var fromAccountGrain = GrainFactory.GetGrain<IBankAccountGrain>(fromAccountId);
        var toAccountGrain = GrainFactory.GetGrain<IBankAccountGrain>(toAccountId);
        
        await Task.WhenAll(
            fromAccountGrain.WithdrawMoney(amountToTransfer),
            toAccountGrain.Deposit(amountToTransfer)
            );
    }
}