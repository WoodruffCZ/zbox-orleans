using Orleans.Providers;
using ZboxOrleans.GrainStates;

namespace ZboxOrleans.Grains.DistributedTransactionGrains;

[StorageProvider(ProviderName = Globals.InMemoryStorageProviderName)]
public class BankAccountGrain : Grain<BankAccountGrainState>, IBankAccountGrain
{
    public Task Deposit(decimal money)
    {
        State.DepositMoney(money);

        return Task.CompletedTask;
    }

    public Task WithdrawMoney(decimal money)
    {
        State.WithdrawMoney(money);
        
        return Task.CompletedTask;
    }

    public Task<decimal> GetCurrentBalance()
    {
        return Task.FromResult(State.MoneyAmount);
    }
}