using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;

namespace Apothecary
{
    // Token: 0x02000013 RID: 19
    [HarmonyPatch(typeof(CompRottable), "CompTickRare")]
    public class CR_CompTickRare_Patch
    {
        // Token: 0x06000037 RID: 55 RVA: 0x00003846 File Offset: 0x00001A46
        [HarmonyPrefix]
        [HarmonyPriority(800)]
        public static bool Prefix(ref CompRottable __instance)
        {
            if (!__instance.parent.Spawned ||
                __instance.parent.Position.GetFirstThingWithComp<CompRottable>(__instance.parent.Map) == null)
            {
                return true;
            }

            Ticky(__instance, 250);
            return false;
        }

        // Token: 0x06000038 RID: 56 RVA: 0x00003884 File Offset: 0x00001A84
        private static void Ticky(CompRottable CR, int interval)
        {
            if (!CR.Active)
            {
                return;
            }

            interval = (int) (interval / 2f);
            var rotProgress = CR.RotProgress;
            var num = GenTemperature.RotRateAtTemperature(CR.parent.AmbientTemperature);
            CR.RotProgress += num * interval;
            if (CR.Stage == RotStage.Rotting && CR.PropsRot.rotDestroys)
            {
                if (CR.parent.IsInAnyStorage() && CR.parent.SpawnedOrAnyParentSpawned)
                {
                    Messages.Message(
                        "MessageRottedAwayInStorage".Translate(CR.parent.Label, CR.parent).CapitalizeFirst(),
                        new TargetInfo(CR.parent.PositionHeld, CR.parent.MapHeld), MessageTypeDefOf.NegativeEvent);
                    LessonAutoActivator.TeachOpportunity(ConceptDefOf.SpoilageAndFreezers, OpportunityType.GoodToKnow);
                }

                CR.parent.Destroy();
                return;
            }

            if (Mathf.FloorToInt(rotProgress / 60000f) == Mathf.FloorToInt(CR.RotProgress / 60000f) ||
                !ShouldTakeRotDamage(CR))
            {
                return;
            }

            if (CR.Stage == RotStage.Rotting && CR.PropsRot.rotDamagePerDay > 0f)
            {
                CR.parent.TakeDamage(new DamageInfo(DamageDefOf.Rotting,
                    GenMath.RoundRandom(CR.PropsRot.rotDamagePerDay)));
                return;
            }

            if (CR.Stage == RotStage.Dessicated && CR.PropsRot.dessicatedDamagePerDay > 0f)
            {
                CR.parent.TakeDamage(new DamageInfo(DamageDefOf.Rotting,
                    GenMath.RoundRandom(CR.PropsRot.dessicatedDamagePerDay)));
            }
        }

        // Token: 0x06000039 RID: 57 RVA: 0x00003A4C File Offset: 0x00001C4C
        private static bool ShouldTakeRotDamage(CompRottable CR)
        {
            return !(CR.parent.ParentHolder is Thing thing) || thing.def.category != ThingCategory.Building ||
                   !thing.def.building.preventDeteriorationInside;
        }
    }
}