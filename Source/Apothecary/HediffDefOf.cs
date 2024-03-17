using RimWorld;
using Verse;

namespace Apothecary;

[DefOf]
public static class HediffDefOf
{
    public static HediffDef Cut;
    public static HediffDef Burn;
    public static HediffDef Gunshot;
    public static HediffDef Scratch;
    public static HediffDef Stab;
    public static HediffDef Bruise;
    public static HediffDef Bite;
    public static HediffDef Shredded;
    public static HediffDef FoodPoisoning;
    public static HediffDef Anesthetic;

    static HediffDefOf()
    {
        DefOfHelper.EnsureInitializedInCtor(typeof(HediffDefOf));
    }
}