using System.Collections.Generic;
using HarmonyLib;
using RimWorld;
using Verse;

namespace Apothecary;

[HarmonyPatch(typeof(FoodUtility), nameof(FoodUtility.AddFoodPoisoningHediff))]
public class AYFoodUtility_AddFoodPoisoningHediff
{
    [HarmonyPriority(800)]
    public static bool Prefix(Pawn pawn)
    {
        return !immuneToFp(pawn, HediffDefOf.FoodPoisoning);
    }

    private static bool immuneToFp(Pawn pawn, HediffDef fPdef)
    {
        var drugHDefs = ayfpImmDrug();
        var hediffs = pawn.health.hediffSet.hediffs;
        foreach (var hediff in hediffs)
        {
            if (!drugHDefs.Contains(hediff.def.defName))
            {
                continue;
            }

            var curStage = hediff.CurStage;
            if (curStage?.makeImmuneTo == null)
            {
                continue;
            }

            foreach (var hediffDef in curStage.makeImmuneTo)
            {
                if (hediffDef == fPdef)
                {
                    return true;
                }
            }
        }

        return false;
    }

    private static List<string> ayfpImmDrug()
    {
        var list = new List<string>();
        list.AddDistinct("AYKaleTeaHigh");
        return list;
    }
}