using System;
using RimWorld;
using Verse;

namespace Apothecary
{
	// Token: 0x0200001C RID: 28
	public class HediffComp_AYWakeAnasthetic : HediffComp
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000059 RID: 89 RVA: 0x000044CC File Offset: 0x000026CC
		public HediffCompProperties_AYWakeAnasthetic AYProps
		{
			get
			{
				return (HediffCompProperties_AYWakeAnasthetic)this.props;
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000044DC File Offset: 0x000026DC
		public override void CompPostTick(ref float severityAdjustment)
		{
			if (base.Pawn.Awake())
			{
				Pawn_HealthTracker health = base.Pawn.health;
				bool flag;
				if (health == null)
				{
					flag = false;
				}
				else
				{
					PawnCapacitiesHandler capacities = health.capacities;
					float? num = (capacities != null) ? new float?(capacities.GetLevel(PawnCapacityDefOf.Consciousness)) : null;
					float num2 = 0.05f;
					flag = (num.GetValueOrDefault() > num2 & num != null);
				}
				if (flag && base.Pawn.IsHashIntervalTick(2500))
				{
					Pawn pawn = base.Pawn;
					HediffSet hediffSet;
					if (pawn == null)
					{
						hediffSet = null;
					}
					else
					{
						Pawn_HealthTracker health2 = pawn.health;
						hediffSet = (health2?.hediffSet);
					}
					HediffSet set = hediffSet;
					if (set != null)
					{
						Hediff anasthetic = set.GetFirstHediffOfDef(HediffDefOf.Anesthetic, false);
						if (anasthetic != null)
						{
							float sev = anasthetic.Severity;
							if (sev > 0f)
							{
								float sevloss = sev * this.AYProps.sevReduce;
								if (sev - sevloss > 0f)
								{
									anasthetic.Severity = sev - sevloss;
									return;
								}
								anasthetic.Severity = 0f;
							}
						}
					}
				}
			}
		}
	}
}
