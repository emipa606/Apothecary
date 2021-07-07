using RimWorld;
using Verse;

namespace Apothecary
{
    // Token: 0x0200001E RID: 30
    public class IngredientValueGetter_BarrelSalting : IngredientValueGetter
    {
        // Token: 0x0600005D RID: 93 RVA: 0x00004604 File Offset: 0x00002804
        public override float ValuePerUnitOf(ThingDef t)
        {
            if (t.IsNutritionGivingIngestible)
            {
                return t.GetStatValueAbstract(StatDefOf.Nutrition);
            }

            if (t.defName == "AYSalt" || t.defName == "WoodLog")
            {
                return 1f;
            }

            return 0f;
        }

        // Token: 0x0600005E RID: 94 RVA: 0x00004658 File Offset: 0x00002858
        public override string BillRequirementsDescription(RecipeDef r, IngredientCount ing)
        {
            string desc;
            if (!ing.IsFixedIngredient)
            {
                desc = "BillRequiresNutrition".Translate(ing.GetBaseCount()) + " (" + ing.filter.Summary + ")";
            }
            else
            {
                desc = "BillRequires".Translate(ing.GetBaseCount(), ing.filter.Summary);
            }

            return desc;
        }
    }
}