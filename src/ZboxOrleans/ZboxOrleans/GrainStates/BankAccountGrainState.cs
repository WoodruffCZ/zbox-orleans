namespace ZboxOrleans.GrainStates;

[GenerateSerializer]
public record BankAccountGrainState
{
    [Id(0)] public Decimal MoneyAmount { get; set; } = 100; //default amount :)
}