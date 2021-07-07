using Verse;

namespace Apothecary
{
    // Token: 0x02000023 RID: 35
    public class PlaceWorker_AtItemLayer : PlaceWorker
    {
        // Token: 0x0600006A RID: 106 RVA: 0x00004B8C File Offset: 0x00002D8C
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

                if (thingy.def.category == ThingCategory.Building && thingy.def.surfaceType != SurfaceType.Item)
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