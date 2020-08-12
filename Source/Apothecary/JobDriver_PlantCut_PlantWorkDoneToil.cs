using System;
using HarmonyLib;
using RimWorld;
using Verse;
using Verse.AI;

namespace Apothecary
{
	// Token: 0x02000020 RID: 32
	[HarmonyPatch(typeof(JobDriver_PlantCut), "PlantWorkDoneToil", null)]
	public static class JobDriver_PlantCut_PlantWorkDoneToil
	{
		// Token: 0x06000062 RID: 98 RVA: 0x00004944 File Offset: 0x00002B44
		private static void Postfix(ref Toil __result, ref JobDriver_PlantCut __instance)
		{
			Map map = __instance.pawn.Map;
			Pawn actor = __instance.pawn;
			__result.initAction = (Action)Delegate.Combine(__result.initAction, new Action(delegate()
			{
				Plant plant = (Plant)actor.jobs.curJob.GetTarget(TargetIndex.A).Thing;
				AYPlantUtility.AddYield(actor, plant);
				if (plant.def.defName == "Plant_AYGinkgoBiloba")
				{
					float skillRatio = 0.5f;
					if ((actor?.skills) != null)
					{
						skillRatio = (actor.skills.GetSkill(SkillDefOf.Plants).levelInt + 5) / 20;
						if (skillRatio > 1f)
						{
							skillRatio = 1f;
						}
						if (skillRatio < 0.5f)
						{
							skillRatio = 0.5f;
						}
					}
					Thing thing = ThingMaker.MakeThing(ThingDef.Named("AYGinkgoLeaves"), null);
					float num = plant.def.plant.harvestYield * 2f * plant.Growth * skillRatio;
					num = JobDriver_PlantCut_PlantWorkDoneToil.AdjustYield(num);
					thing.stackCount = checked((int)num);
					if (thing.stackCount > 0)
					{
						GenPlace.TryPlaceThing(thing, actor.Position, actor.Map, ThingPlaceMode.Near, null, null, default);
						return;
					}
				}
				else if (plant.def.defName == "BurnedTree")
				{
					float skillRatio2 = 0.5f;
					Pawn actor2 = actor;
					if ((actor2?.skills) != null)
					{
						skillRatio2 = (actor.skills.GetSkill(SkillDefOf.Plants).levelInt + 5) / 20;
						if (skillRatio2 > 1f)
						{
							skillRatio2 = 1f;
						}
						if (skillRatio2 < 0.5f)
						{
							skillRatio2 = 0.5f;
						}
					}
					ThingDef def = ThingDef.Named("AYCharcoal");
					ThingDef defTwo = ThingDef.Named("AYWoodAshes");
					Thing thingOne = ThingMaker.MakeThing(def, null);
					Thing thingTwo = ThingMaker.MakeThing(defTwo, null);
					float numOne = 10f * skillRatio2;
					float numTwo = 20f * skillRatio2;
					numOne = JobDriver_PlantCut_PlantWorkDoneToil.AdjustYield(numOne);
					numTwo = JobDriver_PlantCut_PlantWorkDoneToil.AdjustYield(numTwo);
					checked
					{
						thingOne.stackCount = (int)numOne;
						thingTwo.stackCount = (int)numTwo;
						if (thingOne.stackCount > 0)
						{
							GenPlace.TryPlaceThing(thingOne, actor.Position, actor.Map, ThingPlaceMode.Near, null, null, default);
						}
						if (thingTwo.stackCount > 0)
						{
							GenPlace.TryPlaceThing(thingTwo, actor.Position, actor.Map, ThingPlaceMode.Near, null, null, default);
						}
					}
				}
			}));
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00004995 File Offset: 0x00002B95
		public static float AdjustYield(float num)
		{
			num *= Rand.Range(0.8f, 1.2f);
			return num;
		}
	}
}
