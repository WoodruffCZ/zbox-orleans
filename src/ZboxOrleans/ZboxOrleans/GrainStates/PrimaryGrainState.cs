namespace ZboxOrleans.GrainStates;

[GenerateSerializer]
public record PrimaryGrainState
{
    [Id(0)]
    public string? Value { get; set; }
}