using System.Collections.Generic;
using System.Linq;
using Verse;

namespace Apothecary;

[StaticConstructorOnStartup]
public class InjRecipesCSmithy
{
    private static void InjectRecipesCSmithy()
    {
        var rcPadd = new List<RecipeDef>();
        var thGadd = new List<RecipeDef>();
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
                thGadd.AddDistinct(sourceRCP);
            }
        }

        var recipes = DefDatabase<RecipeDef>.AllDefsListForReading;
        if (recipes.Count > 0)
        {
            foreach (var recipe in recipes)
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
                    rcPadd.AddDistinct(recipe);
                }
            }
        }

        var rcpbenchadd = 0;
        if (thGadd.Count > 0)
        {
            foreach (var rcp in thGadd)
            {
                dest.recipes.AddDistinct(rcp);
                rcpbenchadd++;
            }

            DefDatabase<RecipeDef>.ResolveAllReferences();
        }

        var rcpattachadd = 0;
        if (rcPadd.Count > 0)
        {
            foreach (var recipeDef in rcPadd)
            {
                recipeDef.recipeUsers.AddDistinct(dest);
                rcpattachadd++;
            }

            DefDatabase<ThingDef>.ResolveAllReferences();
        }

        if (rcpbenchadd > 0)
        {
            Log.Message($"{rcpbenchadd} recipes added to apothecary charcoal bench.");
        }

        if (rcpattachadd > 0)
        {
            Log.Message($"{rcpattachadd}recipes have attached the charcoal bench for use.");
        }
    }
}