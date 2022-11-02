using Verse;

namespace Apothecary;

public class HediffCompProperties_AYCure : HediffCompProperties
{
    public float CureHoursMax;

    public float CureHoursMin;

    public HediffCompProperties_AYCure()
    {
        compClass = typeof(HediffComp_AYCure);
    }
}