using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using GatherBuddy.Time;
using GatherBuddy.Utility;
using Lumina.Excel.Sheets;

namespace GatherBuddy.Classes;

public partial class GatheringNode
{
    internal static GatheringNode CreateSpecial(GameData data, GatheringPointBase baseNode, Territory territory,
        Gatherable item, IReadOnlyDictionary<uint, List<Vector3>> worldPositions)
    {
        var positions = worldPositions.Values.SelectMany(list => list).ToArray();
        var x          = positions.Average(position => position.X);
        var z          = positions.Average(position => position.Z);
        var mapX       = Maps.NodeToMap(x, territory.SizeFactor);
        var mapY       = Maps.NodeToMap(z, territory.SizeFactor);
        var aetheryte = territory.Aetherytes.Count > 0
            ? territory.Aetherytes.ArgMin(candidate => candidate.WorldDistance(territory.Id, mapX, mapY))
            : null;
        var radius = data.DataManager.GetExcelSheet<ExportedGatheringPoint>()
            .GetRowOrDefault(baseNode.RowId)?.Radius ?? 10;

        var node = new GatheringNode(baseNode, territory, item, worldPositions, mapX, mapY, aetheryte, radius);
        node.AddNodeToItem(item);
        return node;
    }

    private GatheringNode(GatheringPointBase baseNode, Territory territory, Gatherable item,
        IReadOnlyDictionary<uint, List<Vector3>> worldPositions, int mapX, int mapY, Aetheryte? aetheryte, ushort radius)
    {
        BaseNodeData     = baseNode;
        Territory        = territory;
        Name             = territory.Name;
        Items            = [item];
        NodeType         = Enums.NodeType.Regular;
        Times            = BitfieldUptime.AllHours;
        IsLeveling       = true;
        Folklore         = string.Empty;
        IntegralXCoord   = mapX;
        IntegralYCoord   = mapY;
        ClosestAetheryte = aetheryte;
        Radius           = radius;
        WorldPositions   = worldPositions.ToDictionary(pair => pair.Key, pair => pair.Value);

        DefaultXCoord    = IntegralXCoord;
        DefaultYCoord    = IntegralYCoord;
        DefaultAetheryte = ClosestAetheryte;
        DefaultRadius    = Radius;
    }
}
