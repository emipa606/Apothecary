using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;

namespace Apothecary;

[HarmonyPatch(typeof(CompRottable), nameof(CompRottable.CompTickRare))]
public class CompRottable_CompTickRare
{
    [HarmonyPriority(800)]
    public static bool Prefix(ref CompRottable __instance)
    {
        if (!__instance.parent.Spawned ||
            __instance.parent.Position.GetFirstThingWithComp<CompRottable>(__instance.parent.Map) == null)
        {
            return true;
        }

        ticky(__instance, 250);
        return false;
    }

    private static void ticky(CompRottable cr, int interval)
    {
        if (!cr.Active)
        {
            return;
        }

        interval = (int)(interval / 2f);
        var rotProgress = cr.RotProgress;
        var num = GenTemperature.RotRateAtTemperature(cr.parent.AmbientTemperature);
        cr.RotProgress += num * interval;
        if (cr.Stage == RotStage.Rotting && cr.PropsRot.rotDestroys)
        {
            if (cr.parent.IsInAnyStorage() && cr.parent.SpawnedOrAnyParentSpawned)
            {
                Messages.Message(
                    "MessageRottedAwayInStorage".Translate(cr.parent.Label, cr.parent).CapitalizeFirst(),
                    new TargetInfo(cr.parent.PositionHeld, cr.parent.MapHeld), MessageTypeDefOf.NegativeEvent);
                LessonAutoActivator.TeachOpportunity(ConceptDefOf.SpoilageAndFreezers, OpportunityType.GoodToKnow);
            }

            cr.parent.Destroy();
            return;
        }

        if (Mathf.FloorToInt(rotProgress / 60000f) == Mathf.FloorToInt(cr.RotProgress / 60000f) ||
            !shouldTakeRotDamage(cr))
        {
            return;
        }

        switch (cr.Stage)
        {
            case RotStage.Rotting when cr.PropsRot.rotDamagePerDay > 0f:
                cr.parent.TakeDamage(new DamageInfo(DamageDefOf.Rotting,
                    GenMath.RoundRandom(cr.PropsRot.rotDamagePerDay)));
                return;
            case RotStage.Dessicated when cr.PropsRot.dessicatedDamagePerDay > 0f:
                cr.parent.TakeDamage(new DamageInfo(DamageDefOf.Rotting,
                    GenMath.RoundRandom(cr.PropsRot.dessicatedDamagePerDay)));
                break;
        }
    }

    private static bool shouldTakeRotDamage(CompRottable CR)
    {
        return CR.parent.ParentHolder is not Thing thing || thing.def.category != ThingCategory.Building ||
               !thing.def.building.preventDeteriorationInside;
    }
}