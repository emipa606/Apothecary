using HarmonyLib;
using RimWorld;
using Verse;

namespace Apothecary;

[HarmonyPatch(typeof(RecipeWorker), "ConsumeIngredient")]
public class RW_ConsumeIngredient_PrePatch
{
    [HarmonyPrefix]
    [HarmonyPriority(800)]
    public static bool Prefix(ref RecipeWorker __instance, Thing ingredient, RecipeDef recipe, Map map)
    {
        if (recipe.defName != "AY_WashApparel" || !ingredient.def.IsApparel)
        {
            return true;
        }

        if (!((Apparel)ingredient).WornByCorpse)
        {
            return false;
        }

        var loseQual = Controller.Settings.WashLowersQual;
        AYWashUtility.AYWashHaulJob(AYWashUtility.AYWashApparel(ref ingredient, map, loseQual));

        return false;
    }
}