using System;
using System.Collections.Generic;
using RimWorld;
using Verse;

namespace Apothecary
{
	// Token: 0x02000024 RID: 36
	public class PlaceWorker_AYWaterGrower : PlaceWorker
	{
		// Token: 0x0600006C RID: 108 RVA: 0x00004C64 File Offset: 0x00002E64
		public override AcceptanceReport AllowsPlacing(BuildableDef checkingDef, IntVec3 loc, Rot4 rot, Map map, Thing thingToIgnore = null, Thing thing = null)
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
			if (map.terrainGrid.TerrainAt(loc).defName.Contains("Ocean") || map.terrainGrid.TerrainAt(loc).defName.Contains("ocean"))
			{
				return false;
			}
			List<Thing> list = map.thingGrid.ThingsListAt(loc);
			for (int i = 0; i < list.Count; i++)
			{
				Thing thingy = list[i];
				if (thingy.def.IsBlueprint || thingy.def.IsFrame)
				{
					return false;
				}
				if (thingy.def.category == ThingCategory.Plant)
				{
					return false;
				}
				if (thingy.def.category == ThingCategory.Building)
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

		// Token: 0x0600006D RID: 109 RVA: 0x00004D9B File Offset: 0x00002F9B
		public override IEnumerable<TerrainAffordanceDef> DisplayAffordances()
		{
			yield return TerrainAffordanceDefOf.Bridgeable;
			yield break;
		}
	}
}
