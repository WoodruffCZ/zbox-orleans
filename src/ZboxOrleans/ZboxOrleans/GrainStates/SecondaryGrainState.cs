namespace ZboxOrleans.GrainStates;

[GenerateSerializer]
public record SecondaryGrainState
{
    [Id(0)]
    public string? Value { get; set; }
}