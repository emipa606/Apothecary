using System;
using System.Collections.Generic;
using RimWorld;
using Verse;

namespace Apothecary
{
	// Token: 0x02000009 RID: 9
	public static class AYPlantUtility
	{
		// Token: 0x06000018 RID: 24 RVA: 0x00002EDC File Offset: 0x000010DC
		public static void AddYield(Pawn pawn, Plant plant)
		{
			if (AYResearch.AYHerbsYield.IsFinished && AYPlantUtility.isAYPlant(plant))
			{
				ThingDef def = plant.def;
				ThingDef thingDef;
				if (def == null)
				{
					thingDef = null;
				}
				else
				{
					PlantProperties plant2 = def.plant;
					thingDef = (plant2?.harvestedThingDef);
				}
				ThingDef HarvDef = thingDef;
				if (HarvDef != null)
				{
					float HarvNum = plant.def.plant.harvestYield;
					if (HarvNum > 0f)
					{
						float addyield = AYPlantUtility.GetAddYield(pawn, plant, HarvNum);
						if (addyield > 0f)
						{
							AYPlantUtility.GenAddYield(pawn, plant, HarvDef, addyield);
						}
					}
				}
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002F54 File Offset: 0x00001154
		public static bool isAYPlant(Plant plant)
		{
			if (plant != null)
			{
				ThingDef def = plant.def;
				bool flag;
				if (def == null)
				{
					flag = (null != null);
				}
				else
				{
					PlantProperties plant2 = def.plant;
					flag = ((plant2?.sowResearchPrerequisites) != null);
				}
				if (flag)
				{
					ThingDef def2 = plant.def;
					List<ResearchProjectDef> list;
					if (def2 == null)
					{
						list = null;
					}
					else
					{
						PlantProperties plant3 = def2.plant;
						list = (plant3?.sowResearchPrerequisites);
					}
					List<ResearchProjectDef> PlantResearch = list;
					if (PlantResearch.Count > 0)
					{
						for (int i = 0; i < PlantResearch.Count; i++)
						{
							if (PlantResearch[i].defName.StartsWith("AYHerbs"))
							{
								return true;
							}
						}
					}
				}
			}
			return false;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002FD8 File Offset: 0x000011D8
		public static float GetAddYield(Pawn pawn, Plant plant, float yield)
		{
			if (pawn != null && plant != null)
			{
				float skillRatio = 0.5f;
				if ((pawn?.skills) != null)
				{
					skillRatio = (pawn.skills.GetSkill(SkillDefOf.Plants).levelInt + 5) / 20;
					if (skillRatio > 1f)
					{
						skillRatio = 1f;
					}
					if (skillRatio < 0.5f)
					{
						skillRatio = 0.5f;
					}
				}
				return yield * plant.Growth * skillRatio * 0.4f;
			}
			return 0f;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00003050 File Offset: 0x00001250
		public static void GenAddYield(Pawn pawn, Plant plant, ThingDef harvDef, float yield)
		{
			Thing thing = ThingMaker.MakeThing(harvDef, null);
			thing.stackCount = checked((int)yield);
			if (thing.stackCount > 0)
			{
				GenPlace.TryPlaceThing(thing, pawn.Position, pawn.Map, ThingPlaceMode.Near, null, null, default);
			}
		}
	}
}
