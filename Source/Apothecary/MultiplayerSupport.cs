using System.Reflection;
using HarmonyLib;
using Multiplayer.API;
using Verse;

namespace Apothecary;

[StaticConstructorOnStartup]
internal static class MultiplayerSupport
{
    private static readonly Harmony harmony = new("rimworld.pelador.apothecary.multiplayersupport");

    static MultiplayerSupport()
    {
        if (!MP.enabled)
        {
            return;
        }

        MethodInfo[] array =
        [
            AccessTools.Method(typeof(AYBitsUtility), nameof(AYBitsUtility.GetBitsYield)),
            AccessTools.Method(typeof(Building_AYCompostBin), nameof(Building_AYCompostBin.PlaceProduct)),
            AccessTools.Method(typeof(HediffComp_AYRegen), nameof(HediffComp_AYRegen.ResetTicksToHeal)),
            AccessTools.Method(typeof(HediffComp_AYRegen), nameof(HediffComp_AYRegen.TryHealRandomOldWound)),
            AccessTools.Method(typeof(JobDriver_PlantHarvest_PlantWorkDoneToil),
                nameof(JobDriver_PlantHarvest_PlantWorkDoneToil.AdjustYield)),
            AccessTools.Method(typeof(Mineable_TrySpawnYield), nameof(Mineable_TrySpawnYield.GenRnd100)),
            AccessTools.Method(typeof(AYFilth_Salt), nameof(AYFilth_Salt.GetRndMelt)),
            AccessTools.Method(typeof(HediffComp_AYCure), nameof(HediffComp_AYCure.SetTicksToCure)),
            AccessTools.Method(typeof(Building_BirdBath), nameof(Building_BirdBath.IsInspired))
        ];
        foreach (var methodInfo in array)
        {
            FixRNG(methodInfo);
        }
    }

    private static void FixRNG(MethodInfo method)
    {
        harmony.Patch(method, new HarmonyMethod(typeof(MultiplayerSupport), nameof(FixRNGPre)),
            new HarmonyMethod(typeof(MultiplayerSupport), nameof(FixRNGPos)));
    }

    private static void FixRNGPre()
    {
        Rand.PushState(Find.TickManager.TicksAbs);
    }

    private static void FixRNGPos()
    {
        Rand.PopState();
    }
}