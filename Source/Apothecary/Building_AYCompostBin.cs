using System;
using System.Text;
using RimWorld;
using Verse;

namespace Apothecary
{
	// Token: 0x0200000F RID: 15
	public class Building_AYCompostBin : Building
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00003510 File Offset: 0x00001710
		private int TargetTicks
		{
			get
			{
				return this.compostBinComp.Props.fermentTicks;
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00003522 File Offset: 0x00001722
		public override void ExposeData()
		{
			base.ExposeData();
			Scribe_Values.Look<int>(ref this.fermentTicks, "fermentTicks", 0, false);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000353C File Offset: 0x0000173C
		public override void SpawnSetup(Map currentGame, bool respawningAfterLoad)
		{
			base.SpawnSetup(currentGame, respawningAfterLoad);
			this.compostBinComp = base.GetComp<CompAYCompostBin>();
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00003554 File Offset: 0x00001754
		public override void TickRare()
		{
			base.TickRare();
			if (this.fermentTicks < this.TargetTicks)
			{
				this.fermentTicks++;
				float heat = 4f;
				if (this.def.defName == Building_AYCompostBin.AYCharcoalKiln)
				{
					MoteMaker.ThrowSmoke(base.Position.ToVector3(), base.Map, 1f);
					heat = 12f;
				}
				GenTemperature.PushHeat(base.Position, base.Map, heat);
			}
			if (this.fermentTicks >= this.TargetTicks)
			{
				this.PlaceProduct();
			}
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000035EC File Offset: 0x000017EC
		public void PlaceProduct()
		{
			IntVec3 position = base.Position;
			Map map = base.Map;
			Thing thing = ThingMaker.MakeThing(ThingDef.Named(this.compostBinComp.Props.productOne), null);
			thing.stackCount = this.compostBinComp.Props.numProductOne;
			GenPlace.TryPlaceThing(thing, position, map, ThingPlaceMode.Near, null, null, default);
			Thing thing2 = ThingMaker.MakeThing(ThingDef.Named(this.compostBinComp.Props.productTwo), null);
			thing2.stackCount = this.compostBinComp.Props.numProductTwo;
			GenPlace.TryPlaceThing(thing2, position, map, ThingPlaceMode.Near, null, null, default);
			if (this.def.defName != Building_AYCompostBin.AYCharcoalKiln)
			{
				Random random = new Random();
				int Chance = random.Next(3);
				int num = random.Next(3);
				if (Chance < 2)
				{
					GenPlace.TryPlaceThing(ThingMaker.MakeThing(ThingDef.Named("WoodLog"), null), position, map, ThingPlaceMode.Near, null, null, default);
				}
				if (num < 1)
				{
					GenPlace.TryPlaceThing(ThingMaker.MakeThing(ThingDef.Named("WoodLog"), null), position, map, ThingPlaceMode.Near, null, null, default);
				}
			}
			ThingDef prevStuff = null;
			if (base.Stuff != null)
			{
				prevStuff = base.Stuff;
			}
			this.Destroy(DestroyMode.Vanish);
			GenConstruct.PlaceBlueprintForBuild(ThingDef.Named(this.def.defName), position, map, Rot4.North, Faction.OfPlayer, prevStuff);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00003748 File Offset: 0x00001948
		public override string GetInspectString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(base.GetInspectString());
			if (stringBuilder.Length != 0)
			{
				stringBuilder.AppendLine();
			}
			stringBuilder.AppendLine("AY.Progress".Translate() + " " + (fermentTicks / (float)this.TargetTicks * 100f).ToString("#0.00") + "%");
			return stringBuilder.ToString().TrimEndNewlines();
		}

		// Token: 0x04000037 RID: 55
		private CompAYCompostBin compostBinComp;

		// Token: 0x04000038 RID: 56
		private int fermentTicks;

		// Token: 0x04000039 RID: 57
		public static string AYCharcoalKiln = "AYCharcoalKiln";
	}
}
