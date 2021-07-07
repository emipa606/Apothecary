using System.Collections.Generic;
using System.Linq;
using Verse;

namespace Apothecary
{
    // Token: 0x0200001F RID: 31
    [StaticConstructorOnStartup]
    public class InjRecipesCSmithy
    {
        // Token: 0x06000060 RID: 96 RVA: 0x000046E8 File Offset: 0x000028E8
        private static void InjectRecipesCSmithy()
        {
            var RCPadd = new List<RecipeDef>();
            var THGadd = new List<RecipeDef>();
            var source = DefDatabase<ThingDef>.GetNamed("FueledSmithy", false);
            var dest = DefDatabase<ThingDef>.GetNamed("AYCharcoalSmithy", false);
            if (source == null || dest == null)
            {
                return;
            }

            var allRecipes = source.AllRecipes;
            var destRCPs = dest.AllRecipes;
            foreach (var sourceRCP in allRecipes)
            {
                if (!destRCPs.Contains(sourceRCP))
                {
                    THGadd.AddDistinct(sourceRCP);
                }
            }

            var Recipes = DefDatabase<RecipeDef>.AllDefsListForReading;
            if (Recipes.Count > 0)
            {
                foreach (var recipe in Recipes)
                {
                    var benches = recipe.AllRecipeUsers.ToList();
                    if (benches.Count <= 0)
                    {
                        continue;
                    }

                    var sourceFound = false;
                    var destFound = false;
                    foreach (var thingDef in benches)
                    {
                        if (thingDef == source)
                        {
                            sourceFound = true;
                        }

                        if (thingDef == dest)
                        {
                            destFound = true;
                        }

                        if (sourceFound && destFound)
                        {
                            break;
                        }
                    }

                    if (sourceFound && !destFound)
                    {
                        RCPadd.AddDistinct(recipe);
                    }
                }
            }

            var rcpbenchadd = 0;
            if (THGadd.Count > 0)
            {
                foreach (var rcp in THGadd)
                {
                    dest.recipes.AddDistinct(rcp);
                    rcpbenchadd++;
                }

                DefDatabase<RecipeDef>.ResolveAllReferences();
            }

            var rcpattachadd = 0;
            if (RCPadd.Count > 0)
            {
                foreach (var recipeDef in RCPadd)
                {
                    recipeDef.recipeUsers.AddDistinct(dest);
                    rcpattachadd++;
                }

                DefDatabase<ThingDef>.ResolveAllReferences();
            }

            if (rcpbenchadd > 0)
            {
                Log.Message(rcpbenchadd + " recipes added to apothecary charcoal bench.");
            }

            if (rcpattachadd > 0)
            {
                Log.Message(rcpattachadd + "recipes have attached the charcoal bench for use.");
            }
        }
    }
}