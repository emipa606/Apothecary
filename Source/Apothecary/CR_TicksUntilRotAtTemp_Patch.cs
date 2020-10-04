using System;
using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;

namespace Apothecary
{
	// Token: 0x02000014 RID: 20
	[HarmonyPatch(typeof(CompRottable), "TicksUntilRotAtTemp")]
	public class CR_TicksUntilRotAtTemp_Patch
	{
		// Token: 0x0600003B RID: 59 RVA: 0x00003A98 File Offset: 0x00001C98
		[HarmonyPriority(800)]
		public static void Postfix(ref CompRottable __instance, ref int __result, float temp)
		{
			if (__result < 72000000 && __result > 0 && __instance.parent.Spawned && __instance.parent.Position.GetFirstThingWithComp<CompAYPreserve>(__instance.parent.Map) != null)
			{
				float num = GenTemperature.RotRateAtTemperature(temp);
				float num2 = __instance.PropsRot.TicksToRotStart - __instance.RotProgress;
				__result = Mathf.RoundToInt(num2 / num) * 2;
			}
		}
	}
}
