using Verse;

namespace Apothecary;

public class PlaceWorker_Candle : PlaceWorker
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
                case ThingCategory.Building when thingy.def.surfaceType != SurfaceType.Item &&
                                                 thingy.def.surfaceType != SurfaceType.Eat:
                case ThingCategory.Item:
                    return false;
            }
        }

        return true;
    }
}