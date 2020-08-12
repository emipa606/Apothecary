using System;
using System.Collections.Generic;
using RimWorld;
using Verse;

namespace Apothecary
{
	// Token: 0x02000006 RID: 6
	public class AYFilth_WoodAsh : Filth
	{
		// Token: 0x06000010 RID: 16 RVA: 0x00002C6C File Offset: 0x00000E6C
		public override void ExposeData()
		{
			base.ExposeData();
			Scribe_Values.Look<int>(ref this.AYspawnTick, "AYspawnTick", 0, false);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002C88 File Offset: 0x00000E88
		public override void Tick()
		{
			this.AYspawnTick++;
			int removeDelay = 300000;
			if (this.def.defName == "AYAshScrap_Filth")
			{
				removeDelay += removeDelay / 2;
			}
			if (this.AYspawnTick >= removeDelay)
			{
				this.Destroy(DestroyMode.Vanish);
				return;
			}
			if (Find.TickManager.TicksGame % 295 == 0)
			{
				Map TargetMap = base.Map;
				IntVec3 TargetCell = base.Position;
				List<Thing> Thinglist = TargetCell.GetThingList(TargetMap);
				if (Thinglist.Count > 0)
				{
					for (int i = 0; i < Thinglist.Count; i++)
					{
						if (Thinglist[i] is Blight && Thinglist[i].Position == TargetCell)
						{
							this.DoAYToxicDamage(Thinglist[i]);
						}
						if (Thinglist[i] is Plant && (Thinglist[i] as Plant).def.plant.IsTree && Thinglist[i].Position == TargetCell)
						{
							this.DoAYTreeBenefit(Thinglist[i], this.thickness);
						}
					}
				}
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002DB4 File Offset: 0x00000FB4
		private void DoAYTreeBenefit(Thing targ, int thick)
		{
			Plant tree = targ as Plant;
			if (tree != null && tree.Growth < 1f)
			{
				tree.Growth += this.AYGrowthPerTick(tree) * (1f - thick / 20f);
				if (tree.Growth > 1f)
				{
					tree.Growth = 1f;
				}
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002E14 File Offset: 0x00001014
		private float AYGrowthPerTick(Plant tree)
		{
			if (tree.LifeStage != PlantLifeStage.Growing || GenLocalDate.DayPercent(tree) < 0.25f || GenLocalDate.DayPercent(tree) > 0.8f)
			{
				return 0f;
			}
			return 1f / (60000f * tree.def.plant.growDays) * tree.GrowthRate * 400f;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002E74 File Offset: 0x00001074
		private void DoAYToxicDamage(Thing targ)
		{
			Blight blight = targ as Blight;
			if (blight != null)
			{
				float Dmg = 1f;
				blight.Severity -= Dmg;
				if (blight.Severity <= 0f)
				{
					blight.Destroy(DestroyMode.Vanish);
				}
			}
		}

		// Token: 0x04000031 RID: 49
		private int AYspawnTick;
	}
}
