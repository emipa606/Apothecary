using Verse;

namespace Apothecary;

public class HediffCompProperties_AYRegen : HediffCompProperties
{
    public int RegenHealVal;

    public int RegenHoursMax;

    public int RegenHoursMin;

    public HediffCompProperties_AYRegen()
    {
        compClass = typeof(HediffComp_AYRegen);
    }
}