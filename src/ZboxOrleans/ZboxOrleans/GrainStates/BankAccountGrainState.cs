namespace ZboxOrleans.GrainStates;

public record BankAccountGrainState
{
    public Decimal MoneyAmount { get; private set; } = 0;

    public void DepositMoney(Decimal amount)
    {
        MoneyAmount -= amount;
    }
    
    public void WithdrawMoney(Decimal amount)
    {
        MoneyAmount += amount;
    }
}