using Orleans.Concurrency;
using Orleans.Providers;
using Orleans.Transactions.Abstractions;
using ZboxOrleans.Grains.Interfaces;
using ZboxOrleans.GrainStates;

namespace ZboxOrleans.Grains;

[Reentrant]
[StorageProvider(ProviderName = Globals.TransactionStorageName)]
public class BankAccountGrain : Grain, IBankAccountGrain
{
    private readonly ITransactionalState<BankAccountGrainState> _balance;

    public BankAccountGrain(
        [TransactionalState(nameof(balance))] ITransactionalState<BankAccountGrainState> balance) =>
        _balance = balance ?? throw new ArgumentNullException(nameof(balance));

    public async Task Deposit(decimal moneyAmount)
    {
        await _balance.PerformUpdate(balance => balance.MoneyAmount += moneyAmount);
    }

    public async Task WithdrawMoney(decimal moneyAmount)
    {
        await _balance.PerformUpdate(balance =>
        {
            if (balance.MoneyAmount - moneyAmount < 0)
            {
                throw new Exception("Cannot withdraw money - low account balance");
            }

            balance.MoneyAmount -= moneyAmount;
        });
    }

    public async Task<decimal> GetCurrentBalance()
    {
        return await _balance.PerformRead(balance => balance.MoneyAmount);
    }
}