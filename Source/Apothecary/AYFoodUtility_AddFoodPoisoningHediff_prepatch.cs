using System.Collections.Generic;
using HarmonyLib;
using RimWorld;
using Verse;

namespace Apothecary;

[HarmonyPatch(typeof(FoodUtility), "AddFoodPoisoningHediff")]
public class AYFoodUtility_AddFoodPoisoningHediff_prepatch
{
    [HarmonyPrefix]
    [HarmonyPriority(800)]
    public static bool Prefix(Pawn pawn)
    {
        return !ImmuneToFP(pawn, HediffDefOf.FoodPoisoning);
    }

    public static bool ImmuneToFP(Pawn pawn, HediffDef FPdef)
    {
        var drugHDefs = AYFPImmDrug();
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
                if (hediffDef == FPdef)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public static List<string> AYFPImmDrug()
    {
        var list = new List<string>();
        list.AddDistinct("AYKaleTeaHigh");
        return list;
    }
}