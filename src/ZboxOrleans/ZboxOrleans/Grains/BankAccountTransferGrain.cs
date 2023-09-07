using Orleans.Concurrency;
using ZboxOrleans.Grains.Interfaces;

namespace ZboxOrleans.Grains;

// 7. Distributed Transactions: Vytvořte přiklad grainů, které ukazují, jak v Orleans provádět distribuované transakce.
// Mohlo by to být například jednoduché bankovní rozhraní, kde můžete převádět peníze mezi účty a musíte se ujistit, že transakce jsou konzistentní. 
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