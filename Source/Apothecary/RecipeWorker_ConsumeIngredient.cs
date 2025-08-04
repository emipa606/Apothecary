using HarmonyLib;
using RimWorld;
using Verse;

namespace Apothecary;

[HarmonyPatch(typeof(RecipeWorker), nameof(RecipeWorker.ConsumeIngredient))]
public class RecipeWorker_ConsumeIngredient
{
    [HarmonyPriority(800)]
    public static bool Prefix(Thing ingredient, RecipeDef recipe, Map map)
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
        AYWashUtility.AyWashHaulJob(AYWashUtility.AyWashApparel(ref ingredient, map, loseQual));

        return false;
    }
}