using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;

namespace Apothecary;

[HarmonyPatch(typeof(CompRottable), nameof(CompRottable.TicksUntilRotAtTemp))]
public class CompRottable_TicksUntilRotAtTemp
{
    [HarmonyPriority(800)]
    public static void Postfix(ref CompRottable __instance, ref int __result, float temp)
    {
        if (__result is >= 72000000 or <= 0 || !__instance.parent.Spawned ||
            __instance.parent.Position.GetFirstThingWithComp<CompAYPreserve>(__instance.parent.Map) == null)
        {
            return;
        }

        var num = GenTemperature.RotRateAtTemperature(temp);
        var num2 = __instance.PropsRot.TicksToRotStart - __instance.RotProgress;
        __result = Mathf.RoundToInt(num2 / num) * 2;
    }
}