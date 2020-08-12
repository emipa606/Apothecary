using System;
using System.Collections.Generic;
using RimWorld;
using Verse;

namespace Apothecary
{
	// Token: 0x0200001A RID: 26
	public class HediffComp_AYCure : HediffComp
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00003E3E File Offset: 0x0000203E
		public HediffCompProperties_AYCure MSProps
		{
			get
			{
				return (HediffCompProperties_AYCure)this.props;
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003E4C File Offset: 0x0000204C
		public void SetTicksToCure()
		{
			int period = 2500;
			int basehours;
			if (this.MSProps.CureHoursMin > 0f && this.MSProps.CureHoursMax > 0f && this.MSProps.CureHoursMax >= this.MSProps.CureHoursMin)
			{
				basehours = (int)(Rand.Range(this.MSProps.CureHoursMin, this.MSProps.CureHoursMax) * period);
			}
			else
			{
				basehours = Rand.Range(2, 5) * period;
			}
			if (basehours < period)
			{
				basehours = period;
			}
			if (basehours > 36 * period)
			{
				basehours = 36 * period;
			}
			this.ticksToCure = basehours;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00003EE0 File Offset: 0x000020E0
		public override void CompPostTick(ref float severityAdjustment)
		{
			if (this.curing && this.ticksToCure > 0)
			{
				this.ticksToCure--;
				return;
			}
			if (this.curing)
			{
				this.parent.Severity = 0f;
				Messages.Message("Apothecary.CureMsg".Translate(base.Pawn.LabelShort.CapitalizeFirst(), base.Def.label.CapitalizeFirst()), base.Pawn, MessageTypeDefOf.PositiveEvent, true);
				return;
			}
			List<string> Immunities = new List<string>();
			if (AYCureUtility.ImmuneTo(base.Pawn, base.Def, out Immunities))
			{
				int ImmunitiesAsCure = 0;
				for (int i = 0; i < Immunities.Count; i++)
				{
					if (Immunities[i] != "MSCondom_High")
					{
						ImmunitiesAsCure++;
					}
				}
				if (ImmunitiesAsCure > 0)
				{
					this.SetTicksToCure();
					this.curing = true;
				}
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003FCB File Offset: 0x000021CB
		public override void CompExposeData()
		{
			Scribe_Values.Look<int>(ref this.ticksToCure, "ticksToCure", 0, false);
			Scribe_Values.Look<bool>(ref this.curing, "curing", false, false);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00003FF1 File Offset: 0x000021F1
		public override string CompDebugString()
		{
			if (this.curing)
			{
				return "AYticksToCure: " + this.ticksToCure;
			}
			return "No active cure.";
		}

		// Token: 0x04000045 RID: 69
		private int ticksToCure;

		// Token: 0x04000046 RID: 70
		private bool curing;
	}
}
