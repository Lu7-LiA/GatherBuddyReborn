namespace GatherBuddy.Structs;

public sealed class SpecialNodeDefinition
{
    public uint   ItemId               { get; init; }
    public uint   TerritoryId          { get; init; }
    public uint   GatheringPointBaseId { get; init; }
    public uint[] PointIds             { get; init; } = [];
}
