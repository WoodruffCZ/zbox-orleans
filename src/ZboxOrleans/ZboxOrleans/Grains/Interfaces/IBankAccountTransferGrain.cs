namespace ZboxOrleans.Grains.Interfaces;

public interface IBankAccountTransferGrain : IGrainWithGuidKey
{
    [Transaction(TransactionOption.Create)]
    Task Transfer(Guid fromAccountId, Guid toAccountId, decimal amountToTransfer);
}