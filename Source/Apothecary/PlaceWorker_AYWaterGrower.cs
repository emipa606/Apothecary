using System.Collections.Generic;
using RimWorld;
using Verse;

namespace Apothecary;

public class PlaceWorker_AYWaterGrower : PlaceWorker
{
    public override AcceptanceReport AllowsPlacing(BuildableDef checkingDef, IntVec3 loc, Rot4 rot, Map map,
        Thing thingToIgnore = null, Thing thing = null)
    {
        if (!loc.InBounds(map))
        {
            return false;
        }

        if (loc.Filled(map))
        {
            return false;
        }

        if (!map.terrainGrid.TerrainAt(loc).IsWater)
        {
            return false;
        }

        if (map.terrainGrid.TerrainAt(loc).pathCost > 30)
        {
            return false;
        }

        if (map.terrainGrid.TerrainAt(loc).defName.Contains("Ocean") ||
            map.terrainGrid.TerrainAt(loc).defName.Contains("ocean"))
        {
            return false;
        }

        var list = map.thingGrid.ThingsListAt(loc);
        foreach (var thingy in list)
        {
            if (thingy.def.IsBlueprint || thingy.def.IsFrame)
            {
                return false;
            }

            switch (thingy.def.category)
            {
                case ThingCategory.Plant:
                    return false;
                case ThingCategory.Building:
                    return false;
                case ThingCategory.Item:
                    return false;
            }
        }

        return true;
    }

    public override IEnumerable<TerrainAffordanceDef> DisplayAffordances()
    {
        yield return TerrainAffordanceDefOf.Bridgeable;
    }
}