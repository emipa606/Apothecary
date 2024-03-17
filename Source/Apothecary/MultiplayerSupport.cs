using System.Reflection;
using HarmonyLib;
using Multiplayer.API;
using Verse;

namespace Apothecary;

[StaticConstructorOnStartup]
internal static class MultiplayerSupport
{
    private static readonly Harmony harmony = new Harmony("rimworld.pelador.apothecary.multiplayersupport");

    static MultiplayerSupport()
    {
        if (!MP.enabled)
        {
            return;
        }

        MethodInfo[] array =
        [
            AccessTools.Method(typeof(AYBitsUtility), "GetBitsYield"),
            AccessTools.Method(typeof(Building_AYCompostBin), "PlaceProduct"),
            AccessTools.Method(typeof(HediffComp_AYRegen), "ResetTicksToHeal"),
            AccessTools.Method(typeof(HediffComp_AYRegen), "TryHealRandomOldWound"),
            AccessTools.Method(typeof(JobDriver_PlantHarvest_PlantWorkDoneToil), "adjustYield"),
            AccessTools.Method(typeof(TrySpawnYield_PostPatch), "GenRnd100"),
            AccessTools.Method(typeof(AYFilth_Salt), "GetRndMelt"),
            AccessTools.Method(typeof(HediffComp_AYCure), "SetTicksToCure"),
            AccessTools.Method(typeof(Building_BirdBath), "IsInspired")
        ];
        foreach (var methodInfo in array)
        {
            FixRNG(methodInfo);
        }
    }

    private static void FixRNG(MethodInfo method)
    {
        harmony.Patch(method, new HarmonyMethod(typeof(MultiplayerSupport), "FixRNGPre"),
            new HarmonyMethod(typeof(MultiplayerSupport), "FixRNGPos"));
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