using Verse;

namespace Apothecary
{
    // Token: 0x02000025 RID: 37
    public class PlaceWorker_Candle : PlaceWorker
    {
        // Token: 0x0600006F RID: 111 RVA: 0x00004DAC File Offset: 0x00002FAC
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

                if (thingy.def.category == ThingCategory.Plant)
                {
                    return false;
                }

                if (thingy.def.category == ThingCategory.Building && thingy.def.surfaceType != SurfaceType.Item &&
                    thingy.def.surfaceType != SurfaceType.Eat)
                {
                    return false;
                }

                if (thingy.def.category == ThingCategory.Item)
                {
                    return false;
                }
            }

            return true;
        }
    }
}