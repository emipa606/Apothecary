using System;
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
			List<RecipeDef> RCPadd = new List<RecipeDef>();
			List<RecipeDef> THGadd = new List<RecipeDef>();
			ThingDef source = DefDatabase<ThingDef>.GetNamed("FueledSmithy", false);
			ThingDef dest = DefDatabase<ThingDef>.GetNamed("AYCharcoalSmithy", false);
			if (source != null && dest != null)
			{
				List<RecipeDef> allRecipes = source.AllRecipes;
				List<RecipeDef> destRCPs = dest.AllRecipes;
				foreach (RecipeDef sourceRCP in allRecipes)
				{
					if (!destRCPs.Contains(sourceRCP))
					{
						THGadd.AddDistinct(sourceRCP);
					}
				}
				List<RecipeDef> Recipes = DefDatabase<RecipeDef>.AllDefsListForReading;
				if (Recipes.Count > 0)
				{
					foreach (RecipeDef recipe in Recipes)
					{
						List<ThingDef> benches = recipe.AllRecipeUsers.ToList<ThingDef>();
						if (benches.Count > 0)
						{
							bool sourceFound = false;
							bool destFound = false;
							foreach (ThingDef thingDef in benches)
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
				}
				int rcpbenchadd = 0;
				if (THGadd.Count > 0)
				{
					foreach (RecipeDef rcp in THGadd)
					{
						dest.recipes.AddDistinct(rcp);
						rcpbenchadd++;
					}
					DefDatabase<RecipeDef>.ResolveAllReferences(true, false);
				}
				int rcpattachadd = 0;
				if (RCPadd.Count > 0)
				{
					foreach (RecipeDef recipeDef in RCPadd)
					{
						recipeDef.recipeUsers.AddDistinct(dest);
						rcpattachadd++;
					}
					DefDatabase<ThingDef>.ResolveAllReferences(true, false);
				}
				if (rcpbenchadd > 0)
				{
					Log.Message(rcpbenchadd.ToString() + " recipes added to apothecary charcoal bench.", false);
				}
				if (rcpattachadd > 0)
				{
					Log.Message(rcpattachadd.ToString() + "recipes have attached the charcoal bench for use.", false);
				}
			}
		}
	}
}
