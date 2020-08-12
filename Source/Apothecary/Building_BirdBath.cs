using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

namespace Apothecary
{
	// Token: 0x0200000C RID: 12
	public class Building_BirdBath : Building_Art
	{
		// Token: 0x06000022 RID: 34 RVA: 0x00003324 File Offset: 0x00001524
		public override void Tick()
		{
			base.Tick();
			if (base.Spawned && this.IsHashIntervalTick(this.inspirationTicks))
			{
				List<Thing> things = GenRadial.RadialDistinctThingsAround(this.TrueCenter().ToIntVec3(), base.Map, 10f, false).ToList<Thing>();
				if (things.Count > 0)
				{
					foreach (Thing thing in things)
					{
						Pawn p;
						if ((p = (thing as Pawn)) != null && this.IsValidForInspiration(thing, this) && this.IsInspired(25) && !p.mindState.inspirationHandler.Inspired)
						{
							InspirationDef IDef = (from x in DefDatabase<InspirationDef>.AllDefsListForReading
							where x.Worker.InspirationCanOccur(p)
							select x).RandomElementByWeightWithFallback((InspirationDef x) => x.Worker.CommonalityFor(p), null);
							p.mindState.inspirationHandler.TryStartInspiration_NewTemp(IDef, null);
						}
					}
				}
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00003448 File Offset: 0x00001648
		public bool IsValidForInspiration(Thing thing, Building BB)
		{
			Pawn p;
			if ((p = (thing as Pawn)) != null && thing.Spawned && p.Awake() && p.health.capacities.CapableOf(PawnCapacityDefOf.Sight))
			{
				bool flag;
				if (p == null)
				{
					flag = (null != null);
				}
				else
				{
					Pawn_NeedsTracker needs = p.needs;
					flag = ((needs?.beauty) != null);
				}
				if (flag && GenSight.LineOfSight(p.Position, BB.Position, BB.Map, true, null, 0, 0))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000034C0 File Offset: 0x000016C0
		public bool IsInspired(int chance)
		{
			return Rand.Range(1, 100) < chance;
		}

		// Token: 0x04000036 RID: 54
		public int inspirationTicks = 30000;
	}
}
