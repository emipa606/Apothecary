using HarmonyLib;
using RimWorld;
using Verse;

namespace Apothecary
{
    // Token: 0x02000026 RID: 38
    [HarmonyPatch(typeof(RecipeWorker), "ConsumeIngredient")]
    public class RW_ConsumeIngredient_PrePatch
    {
        // Token: 0x06000071 RID: 113 RVA: 0x00004E94 File Offset: 0x00003094
        [HarmonyPrefix]
        [HarmonyPriority(800)]
        public static bool Prefix(ref RecipeWorker __instance, Thing ingredient, RecipeDef recipe, Map map)
        {
            if (recipe.defName != "AY_WashApparel" || !ingredient.def.IsApparel)
            {
                return true;
            }

            if (!((Apparel) ingredient).WornByCorpse)
            {
                return false;
            }

            var loseQual = Controller.Settings.WashLowersQual;
            AYWashUtility.AYWashHaulJob(AYWashUtility.AYWashApparel(ref ingredient, map, loseQual));

            return false;
        }
    }
}