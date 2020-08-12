using System;
using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;

namespace Apothecary
{
	// Token: 0x02000029 RID: 41
	[HarmonyPatch(typeof(Mineable), "TrySpawnYield")]
	public class TrySpawnYield_PostPatch
	{
		// Token: 0x06000078 RID: 120 RVA: 0x000051AC File Offset: 0x000033AC
		[HarmonyPostfix]
		public static void PostFix(ref Mineable __instance, Map map, float yieldChance, bool moteOnWaste, Pawn pawn)
		{
			if (pawn != null)
			{
				bool isSource = false;
				if (__instance.def.defName == "CollapsedRocks" || __instance.def.defName == "rxCollapsedRoofRocks")
				{
					isSource = Controller.Settings.AllowCollapseRocks;
				}
				Mineable mineable = __instance;
				object obj;
				if (mineable == null)
				{
					obj = null;
				}
				else
				{
					ThingDef def = mineable.def;
					if (def == null)
					{
						obj = null;
					}
					else
					{
						BuildingProperties building = def.building;
						obj = (building?.mineableThing);
					}
				}
				if (obj != null || isSource)
				{
					int mining = 0;
					bool flag;
					if (pawn == null)
					{
						flag = (null != null);
					}
					else
					{
						Pawn_SkillTracker skills = pawn.skills;
						flag = ((skills?.GetSkill(SkillDefOf.Mining)) != null);
					}
					if (flag)
					{
						mining = pawn.skills.GetSkill(SkillDefOf.Mining).Level / 4;
					}
					if (TrySpawnYield_PostPatch.GenRnd100() <= 20 + mining)
					{
						Mineable mineable2 = __instance;
						ThingDef defSource;
						if (mineable2 == null)
						{
							defSource = null;
						}
						else
						{
							ThingDef def2 = mineable2.def;
							if (def2 == null)
							{
								defSource = null;
							}
							else
							{
								BuildingProperties building2 = def2.building;
								defSource = (building2?.mineableThing);
							}
						}
						ThingDef bitsdef;
						int bitsyield;
						ThingDef newbitsdef;
						if (AYBitsUtility.GetIsBitsSource(defSource, isSource, pawn, out bitsdef, out bitsyield, out newbitsdef) && bitsdef != null && bitsyield > 0)
						{
							int num = Mathf.Max(1, Mathf.RoundToInt(bitsyield * Find.Storyteller.difficulty.mineYieldFactor));
							Thing bitsthing;
							if (newbitsdef != null)
							{
								bitsthing = ThingMaker.MakeThing(newbitsdef, null);
							}
							else
							{
								bitsthing = ThingMaker.MakeThing(bitsdef, null);
							}
							bitsthing.stackCount = num;
							Thing newbitsthing;
							GenPlace.TryPlaceThing(bitsthing, pawn.Position, map, ThingPlaceMode.Near, out newbitsthing, null, null, default);
							if ((pawn == null || !pawn.IsColonist) && newbitsthing.def.EverHaulable && !newbitsthing.def.designateHaulable)
							{
								newbitsthing.SetForbidden(true, true);
							}
						}
					}
				}
			}
		}

		// Token: 0x06000079 RID: 121 RVA: 0x0000534F File Offset: 0x0000354F
		public static int GenRnd100()
		{
			return Rand.Range(1, 100);
		}
	}
}
