namespace ZboxOrleans.Grains.Interfaces;

public interface IBankAccountGrain : IGrainWithGuidKey
{
    [Transaction(TransactionOption.Join)]
    Task Deposit(Decimal moneyAmount);
    [Transaction(TransactionOption.Join)]
    Task WithdrawMoney(Decimal moneyAmount);
    [Transaction(TransactionOption.CreateOrJoin)]
    Task<Decimal> GetCurrentBalance();
}