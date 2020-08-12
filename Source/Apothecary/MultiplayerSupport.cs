using System;
using System.Reflection;
using HarmonyLib;
using Multiplayer.API;
using Verse;

namespace Apothecary
{
	// Token: 0x02000022 RID: 34
	[StaticConstructorOnStartup]
	internal static class MultiplayerSupport
	{
		// Token: 0x06000066 RID: 102 RVA: 0x00004A14 File Offset: 0x00002C14
		static MultiplayerSupport()
		{
			if (!MP.enabled)
			{
				return;
			}
			MethodInfo[] array = new MethodInfo[]
			{
				AccessTools.Method(typeof(AYBitsUtility), "GetBitsYield", null, null),
				AccessTools.Method(typeof(Building_AYCompostBin), "PlaceProduct", null, null),
				AccessTools.Method(typeof(HediffComp_AYRegen), "ResetTicksToHeal", null, null),
				AccessTools.Method(typeof(HediffComp_AYRegen), "TryHealRandomOldWound", null, null),
				AccessTools.Method(typeof(JobDriver_PlantHarvest_PlantWorkDoneToil), "adjustYield", null, null),
				AccessTools.Method(typeof(TrySpawnYield_PostPatch), "GenRnd100", null, null),
				AccessTools.Method(typeof(AYFilth_Salt), "GetRndMelt", null, null),
				AccessTools.Method(typeof(HediffComp_AYCure), "SetTicksToCure", null, null),
				AccessTools.Method(typeof(Building_BirdBath), "IsInspired", null, null)
			};
			for (int i = 0; i < array.Length; i++)
			{
				MultiplayerSupport.FixRNG(array[i]);
			}
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00004B37 File Offset: 0x00002D37
		private static void FixRNG(MethodInfo method)
		{
			MultiplayerSupport.harmony.Patch(method, new HarmonyMethod(typeof(MultiplayerSupport), "FixRNGPre", null), new HarmonyMethod(typeof(MultiplayerSupport), "FixRNGPos", null), null, null);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00004B71 File Offset: 0x00002D71
		private static void FixRNGPre()
		{
			Rand.PushState(Find.TickManager.TicksAbs);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00004B82 File Offset: 0x00002D82
		private static void FixRNGPos()
		{
			Rand.PopState();
		}

		// Token: 0x0400004B RID: 75
		private static Harmony harmony = new Harmony("rimworld.pelador.apothecary.multiplayersupport");
	}
}
