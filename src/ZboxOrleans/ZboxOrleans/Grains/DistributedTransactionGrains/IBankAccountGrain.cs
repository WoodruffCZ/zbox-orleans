namespace ZboxOrleans.Grains.DistributedTransactionGrains;

public interface IBankAccountGrain : IGrainWithGuidKey
{
    Task Deposit(Decimal money);
    Task WithdrawMoney(Decimal money);
    Task<Decimal> GetCurrentBalance();
}