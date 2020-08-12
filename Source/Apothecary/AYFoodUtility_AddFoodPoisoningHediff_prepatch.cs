using System;
using System.Collections.Generic;
using HarmonyLib;
using RimWorld;
using Verse;

namespace Apothecary
{
	// Token: 0x02000002 RID: 2
	[HarmonyPatch(typeof(FoodUtility), "AddFoodPoisoningHediff")]
	public class AYFoodUtility_AddFoodPoisoningHediff_prepatch
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		[HarmonyPrefix]
		[HarmonyPriority(800)]
		public static bool Prefix(Pawn pawn, Thing ingestible, FoodPoisonCause cause)
		{
			return !AYFoodUtility_AddFoodPoisoningHediff_prepatch.ImmuneToFP(pawn, HediffDefOf.FoodPoisoning);
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002064 File Offset: 0x00000264
		public static bool ImmuneToFP(Pawn pawn, HediffDef FPdef)
		{
			List<string> drugHDefs = AYFoodUtility_AddFoodPoisoningHediff_prepatch.AYFPImmDrug();
			List<Hediff> hediffs = pawn.health.hediffSet.hediffs;
			for (int i = 0; i < hediffs.Count; i++)
			{
				if (drugHDefs.Contains(hediffs[i].def.defName))
				{
					HediffStage curStage = hediffs[i].CurStage;
					if (curStage != null && curStage.makeImmuneTo != null)
					{
						for (int j = 0; j < curStage.makeImmuneTo.Count; j++)
						{
							if (curStage.makeImmuneTo[j] == FPdef)
							{
								return true;
							}
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020F7 File Offset: 0x000002F7
		public static List<string> AYFPImmDrug()
		{
			List<string> list = new List<string>();
			list.AddDistinct("AYKaleTeaHigh");
			return list;
		}
	}
}
