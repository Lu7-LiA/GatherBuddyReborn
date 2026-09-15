using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GatherBuddy.Classes;
using GatherBuddy.Structs;
using Lumina.Excel.Sheets;
using Newtonsoft.Json;

namespace GatherBuddy.Data;

public static class SpecialNodes
{
    public const string FileName = "special_nodes.json";

    public static void Apply(GameData data, IDictionary<uint, GatheringNode> gatheringNodes, string? filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
            return;

        if (!EnsureFileExists(data, filePath))
            return;

        List<SpecialNodeDefinition?> definitions;
        try
        {
            var json = File.ReadAllText(filePath);
            definitions = JsonConvert.DeserializeObject<List<SpecialNodeDefinition?>>(json) ?? [];
        }
        catch (Exception exception)
        {
            data.Log.Warning($"Failed to load special gathering nodes from {filePath}: {exception.Message}");
            return;
        }

        var loaded = 0;
        for (var index = 0; index < definitions.Count; ++index)
        {
            try
            {
                if (!TryCreateNode(data, gatheringNodes, definitions[index], out var node, out var error))
                {
                    data.Log.Warning($"Skipping special gathering node #{index + 1}: {error}");
                    continue;
                }

                gatheringNodes.Add(node.Id, node);
                ++loaded;
            }
            catch (Exception exception)
            {
                data.Log.Warning($"Skipping special gathering node #{index + 1}: {exception.Message}");
            }
        }

        data.Log.Information($"Loaded {loaded} special gathering node(s) from {filePath}.");
    }

    private static bool EnsureFileExists(GameData data, string filePath)
    {
        if (File.Exists(filePath))
            return true;

        try
        {
            var directory = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrEmpty(directory))
                Directory.CreateDirectory(directory);
            File.WriteAllText(filePath, $"[]{Environment.NewLine}");
            data.Log.Information($"Created empty special gathering node file at {filePath}.");
            return true;
        }
        catch (Exception exception)
        {
            data.Log.Warning($"Failed to create special gathering node file at {filePath}: {exception.Message}");
            return false;
        }
    }

    private static bool TryCreateNode(GameData data, IDictionary<uint, GatheringNode> gatheringNodes,
        SpecialNodeDefinition? definition, out GatheringNode node, out string error)
    {
        node = null!;
        if (definition == null)
            return Fail("The definition is null.", out error);
        if (gatheringNodes.ContainsKey(definition.GatheringPointBaseId))
            return Fail($"GatheringPointBase {definition.GatheringPointBaseId} is already registered.", out error);
        if (!data.Gatherables.TryGetValue(definition.ItemId, out var item))
            return Fail($"Item {definition.ItemId} was not found.", out error);

        var territoryRow = data.DataManager.GetExcelSheet<TerritoryType>().GetRowOrDefault(definition.TerritoryId);
        if (territoryRow == null)
            return Fail($"Territory {definition.TerritoryId} was not found.", out error);

        var baseNode = data.DataManager.GetExcelSheet<GatheringPointBase>().GetRowOrDefault(definition.GatheringPointBaseId);
        if (baseNode == null)
            return Fail($"GatheringPointBase {definition.GatheringPointBaseId} was not found.", out error);
        if (baseNode.Value.GatheringType.RowId >= (int)Enums.GatheringType.Spearfishing)
            return Fail($"GatheringPointBase {definition.GatheringPointBaseId} is not a mining or botany node.", out error);

        var pointIds = definition.PointIds?.Distinct().ToArray() ?? [];
        if (pointIds.Length == 0)
            return Fail("No GatheringPoint IDs were specified.", out error);

        var pointSheet     = data.DataManager.GetExcelSheet<GatheringPoint>();
        var worldPositions = new Dictionary<uint, List<System.Numerics.Vector3>>(pointIds.Length);
        foreach (var pointId in pointIds)
        {
            var point = pointSheet.GetRowOrDefault(pointId);
            if (point == null)
                return Fail($"GatheringPoint {pointId} was not found.", out error);
            if (point.Value.GatheringPointBase.RowId != definition.GatheringPointBaseId)
                return Fail($"GatheringPoint {pointId} does not belong to GatheringPointBase {definition.GatheringPointBaseId}.", out error);
            if (!data.WorldCoords.TryGetValue(pointId, out var positions) || positions.Count == 0)
                return Fail($"GatheringPoint {pointId} has no world location.", out error);

            worldPositions.Add(pointId, positions);
        }

        var territory = data.FindOrAddTerritory(territoryRow.Value);
        if (territory == null)
            return Fail($"Territory {definition.TerritoryId} is not usable.", out error);

        node  = GatheringNode.CreateSpecial(data, baseNode.Value, territory, item, worldPositions, definition.Name);
        error = string.Empty;
        return true;
    }

    private static bool Fail(string message, out string error)
    {
        error = message;
        return false;
    }
}
